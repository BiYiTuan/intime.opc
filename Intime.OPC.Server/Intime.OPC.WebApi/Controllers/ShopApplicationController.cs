using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/shopapplies")]
    public class ShopApplicationController : BaseController
    {
        private readonly IShopApplicationService _shopApplicationService;

        public ShopApplicationController(IShopApplicationService shopApplicationService)
        {
            _shopApplicationService = shopApplicationService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id, [UserProfile] UserProfile userProfile)
        {
            var result = _shopApplicationService.GetItem(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            if (result.Data == null)
            {
                return NotFound();
            }
            var r = CheckRole4Store(userProfile, result.Data.StoreId);

            return !r.Result ? BadRequest(r.Error) : RetrunHttpActionResult(result.Data);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetList([FromUri]ApplyQueryCriteriaRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var exectueResult = _shopApplicationService.GetPagedList(request);

            return RetrunHttpActionResult4ExectueResult(exectueResult);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/approved")]
        public IHttpActionResult PutApproved(int id,[FromBody]ApplyApprovedRequest request, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            request.ApplyId = id;

            var exectueResult = _shopApplicationService.SetApproved(request);

            return RetrunHttpActionResult4ExectueResult(exectueResult);
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/notify")]
        public IHttpActionResult PutNotification(int id, [FromBody] ApplyNotifyRequest request,
            [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            request.ApplyId = id;

            var exectueResult = _shopApplicationService.Notification(request);

            return RetrunHttpActionResult4ExectueResult(exectueResult);
        }


        ///// <summary>
        ///// 修改申请单特定可修改内容
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="userProfile"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("{id:int}/apply")]
        //public IHttpActionResult PutApply([FromBody] ApplyInfoRequest request, [UserProfile] UserProfile userProfile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    IHttpActionResult httpActionResult;
        //    var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
        //    if (!result)
        //    {
        //        return httpActionResult;
        //    }

        //    var exectueResult = _shopApplicationService.Update4Apply(request);

        //    return RetrunHttpActionResult(exectueResult);
        //}

    }
}
