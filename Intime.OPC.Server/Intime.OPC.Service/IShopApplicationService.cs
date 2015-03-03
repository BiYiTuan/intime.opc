using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;

namespace Intime.OPC.Service
{
    public interface IShopApplicationService
    {
        /// <summary>
        /// 获取申请表列表数据
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        ExectueResult<PagerInfo<ShopApplicationDto>> GetPagedList(ApplyQueryCriteriaRequest request);

        /// <summary>
        /// 获取指定id的申请表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExectueResult<ShopApplicationDto> GetItem(int id);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExectueResult<ShopApplicationDto> SetApproved(ApplyApprovedRequest request);

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExectueResult<ShopApplicationDto> Notification(ApplyNotifyRequest request);

        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExectueResult<ShopApplicationDto> SetDemotion(ApplyApprovedRequest request);
    }
}
