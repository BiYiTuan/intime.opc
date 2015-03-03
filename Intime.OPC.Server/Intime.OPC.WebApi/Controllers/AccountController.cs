using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using Intime.OPC.WebApi.Core.Security;
using Intime.OPC.WebApi.Models;

namespace Intime.OPC.WebApi.Controllers
{
    /// <summary>
    ///     账户相关接口
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserProfileProvider _userProfileProvider;
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AccountController));

        public AccountController(IAccountService accountService, IUserProfileProvider userProfileProvider)
        {
            _accountService = accountService;
            _userProfileProvider = userProfileProvider;
        }

        [HttpPost]
        public IHttpActionResult AddUser([FromBody] OPC_AuthUser user)
        {
            if (_accountService.Add(user))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPut]
        public IHttpActionResult ResetPassword([FromBody] int userId)
        {
            return DoFunction(() =>
            {
                _accountService.ResetPassword(userId);
                return true;

            }, "");
        }


        [HttpPost]
        public IHttpActionResult ChangePassword([FromBody]ChangePasswordDto dto)
        {
            return DoAction(() => _accountService.ChangePassword(dto.UserID, dto.OldPassword, dto.NewPassword));
        }

        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody] OPC_AuthUser user)
        {
            //TODO:check params
            if (_accountService.Update(user))
            {
                return Ok();
            }

            return InternalServerError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetUsersByRoleID([FromUri]int roleId, [UserProfile] UserProfile userProfile)
        {
            //TODO:增加集团筛选
            //如果是管理员
            if (userProfile.IsSystem)
            {
                return Ok(_accountService.GetUsersByRoleID(roleId, 1, 20000).Result);
            }

            _log.Debug(String.Format("roleid:" + roleId.ToString()));

            return
                Ok(_accountService.GetPagedList(new PagerRequest(1, 20000, 20000), roleId, userProfile.OrgIds.ToList(),
                                                false));

        }

        [HttpPut]
        public IHttpActionResult DeleteUser([FromBody] int? userId)
        {
            if (userId != 0)
            {
                if (_accountService.DeleteById(userId.Value))
                {
                    return Ok();
                }
            }
            //TODO:check params

            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult SelectUser([UserProfile] UserProfile userProfile, [FromUri] string orgID, [FromUri] string SearchField, [FromUri] string SearchValue, [FromUri] int pageIndex, [FromUri] int pageSize = 20)
        {
            //TODO:增加集团筛选

            if (userProfile.IsSystem)
            {
                return DoFunction(() =>
                {
                    if (SearchField == "0")
                    {
                        return _accountService.SelectByLogName(orgID, SearchValue, pageIndex, pageSize);
                    }
                    if (SearchField == "1")
                    {
                        return _accountService.Select(orgID, SearchValue, pageIndex, pageSize);
                    }
                    return BadRequest("查询条件错误");
                }, "查询用户信息失败");
            }

            if (SearchField == "0")
            {
                return Ok(_accountService.GetPagedList(new PagerRequest(pageIndex, pageSize), null, userProfile.OrgIds.ToList(), false, null, SearchValue));
            }
            if (SearchField == "1")
            {
                return Ok(_accountService.GetPagedList(new PagerRequest(pageIndex, pageSize), null, userProfile.OrgIds.ToList(), false, SearchValue, null));
            }

            return BadRequest("参数错误，需要指定 SearchField");
        }


        [HttpPut]
        public IHttpActionResult Enable([FromBody] OPC_AuthUser user)
        {
            if (null == user)
            {
                return BadRequest("用户对象为空");
            }
            //TODO:check params
            if (_accountService.IsStop(user.Id, !(user.IsValid.Value)))
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult Token([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid || loginModel == null)
            {
                return BadRequest(ModelState);
            }

            // ===========================================================
            // 验证用户登录信息
            // ===========================================================

            var currentUser = _accountService.Get(loginModel.UserName, loginModel.Password);

            if (currentUser == null)
            {
                return BadRequest("用户验证失败，请检查您的密码是否正确");
            }

            // ===========================================================
            // 验证通过
            // ===========================================================

            var expires = DateTime.Now.AddSeconds(60 * 60 * 24);

            var profile = _userProfileProvider.Get(currentUser.Id);

            return Ok(new TokenModel
            {
                AccessToken = SecurityUtils.CreateToken(profile, expires),
                UserId = currentUser.Id,
                UserName = loginModel.UserName,
                Expires = expires
            });
        }
    }
}