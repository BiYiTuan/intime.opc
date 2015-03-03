using Intime.OPC.Common.Logger;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Exception;
using Intime.OPC.WebApi.Core.Filters;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System;
using System.Linq;
using System.Web.Http;

namespace Intime.OPC.WebApi.Core
{
    [ApiExceptionFilter]
    public class BaseController : ApiController
    {
        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <returns>ILog.</returns>
        protected ILog GetLog()
        {
            return LoggerManager.Current();
        }

        public TextResult Error(string message)
        {
            return new TextResult(message, this.Request);
        }

        public int UserId
        {
            get
            {
                int uid;
                if (int.TryParse(this.Request.Properties[AccessTokenConst.UseridPropertiesName].ToString(), out uid))
                {
                    return uid;
                }
                throw new UnauthorizedAccessException();
            }
            set
            {
                throw new InvalidOperationException("不允许的操作");
            }
        }

        protected IHttpActionResult DoFunction(Func<dynamic> action, string errorMessage = "")
        {
            try
            {
                return Ok(action());
            }
            catch (Exception ex)
            {
                GetLog().Error(ex);
                return BadRequest(ex.Message);
            }
        }

        protected IHttpActionResult DoAction(Action action, string errorMessage = "")
        {
            try
            {
                action();
                return Ok();
            }
            catch (OpcException oex)
            {
                return BadRequest(oex.Message);
            }
            catch (Exception ex)
            {
                GetLog().Error(ex);
                return InternalServerError();
            }
        }

        protected IHttpActionResult RetrunHttpActionResult4ExectueResult<T>(ExectueResult<T> exectueResult)
        {
            if (exectueResult == null)
            {
                throw new ArgumentNullException("exectueResult");
            }

            return exectueResult.IsSuccess ? RetrunHttpActionResult(exectueResult.Data) : BadRequest(exectueResult.Message);
        }

        protected IHttpActionResult RetrunHttpActionResult<T>(T dto)
        {
            if (null == dto)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        public IHttpActionResult ResultNotFound()
        {
            return NotFound();
        }

        #region methods

        /// <summary>
        /// 检查 用户权限
        /// </summary>
        /// <param name="userProfile"></param>
        /// <param name="storeId">目标的 store 当=null 是 不验证</param>
        /// <returns></returns>
        protected static dynamic CheckRole4Store(UserProfile userProfile, int? storeId)
        {
            //
            if (userProfile == null)
            {
                return new
                {
                    Result = false,
                    Error = "获取用户信息失败"
                };
            }

            if (userProfile.StoreIds != null && !userProfile.StoreIds.Any())
            {
                return new
                {
                    Result = false,
                    Error = "获取用户门店信息失败"
                };
            }

            //
            if (storeId != null && storeId > 0 && userProfile.StoreIds != null)
            {
                if (!userProfile.StoreIds.Contains(storeId.Value))
                {
                    return new
                    {
                        Result = false,
                        Error = String.Format("您没有授权其他门店（id:{0}）的信息", storeId.Value)
                    };
                }
            }

            return new
            {
                Result = true,
                Error = String.Empty
            };
        }

        /// <summary>
        /// 检查 STOREID 如果-1 那么设为null
        /// -1 or null 代表不限定 storeId
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        protected static int? CheckStoreId(int? storeId)
        {
            if (storeId != null && storeId == -1)
            {
                storeId = null;
            }

            return storeId;
        }

        /// <summary>
        /// 检查数据权限，如果返回不为NULL，则BADREQUEST
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public bool CheckDataRoleAndArrangeParams(IStoreDataRoleRequest request, UserProfile userProfile, out IHttpActionResult httpActionResult)
        {
            if (request == null)
            {
                httpActionResult = BadRequest("参数错误");

                return false;
            }

            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                httpActionResult = BadRequest(result.Error);

                return false;
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            request.CurrentUserId = userProfile.Id;
            request.ArrangeParams();

            httpActionResult = null;

            return true;
        }

        #endregion
    }
}