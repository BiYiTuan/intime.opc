using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IInviteCodeRequestRepository : IOPCRepository<int, IMS_InviteCodeRequest>//, IRepository<Section>
    {
        PagerInfo<ShopApplicationDto> GetPagedList(ApplyQueryCriteriaRequest request, Domain.PagerRequest pagerRequest);

        ShopApplicationDto GetDto(int id);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="request"></param>
        void SetApproved(ApplyApprovedRequest request);

        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="request"></param>
        void SetDemotion(ApplyApprovedRequest request);

        void SetApprovedNotificationTimes(int id, int times);

        void SetDemotionNotificationTimes(int id, int times);
    }
}
