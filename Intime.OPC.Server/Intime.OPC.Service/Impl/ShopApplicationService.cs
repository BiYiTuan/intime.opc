using System;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Repository;
using System.Collections.Generic;

namespace Intime.OPC.Service.Impl
{
    public class ShopApplicationService : IShopApplicationService
    {
        private readonly IInviteCodeRequestRepository _inviteCodeRequestRepository;
        public ShopApplicationService(IInviteCodeRequestRepository inviteCodeRequestRepository)
        {
            _inviteCodeRequestRepository = inviteCodeRequestRepository;
        }

        public ExectueResult<PagerInfo<ShopApplicationDto>> GetPagedList(ApplyQueryCriteriaRequest request)
        {
            var pageInfo = _inviteCodeRequestRepository.GetPagedList(request, request.PagerRequest);

            return new OkExectueResult<PagerInfo<ShopApplicationDto>>(pageInfo);
        }

        public ExectueResult<ShopApplicationDto> GetItem(int id)
        {
            var dto = _inviteCodeRequestRepository.GetDto(id);

            return new OkExectueResult<ShopApplicationDto>(dto);
        }

        public ExectueResult<ShopApplicationDto> SetApproved(ApplyApprovedRequest request)
        {
            //1 ok 2 reject
            var approved = new[] { 1, 3 };

            if (approved.Contains(v => v == request.Approved))
            {
                _inviteCodeRequestRepository.SetApproved(request);
            }
            else
            {
                return SetDemotion(request);
            }

            return GetItem(request.ApplyId);
        }

        public ExectueResult<ShopApplicationDto> Notification(ApplyNotifyRequest request)
        {
            //需要调用 微信通知

            var dto = _inviteCodeRequestRepository.GetDto(request.ApplyId);
            if (dto == null)
            {
                return new FailureExectueResult<ShopApplicationDto>(String.Format("申请单{0}未找到", request.ApplyId));
            }

            var status = (InviteCodeRequestStatus)dto.ApproveStatus;

            switch (status)
            {
                case InviteCodeRequestStatus.Requesting:
                    //正在审核
                    break;
                case InviteCodeRequestStatus.Approved:
                    //已经通过
                    _inviteCodeRequestRepository.SetApprovedNotificationTimes(request.ApplyId, request.Times ?? 1);
                    break;
                case InviteCodeRequestStatus.Reject:
                    //拒绝
                    _inviteCodeRequestRepository.SetDemotionNotificationTimes(request.ApplyId, request.Times ?? 1);
                    break;
                default:
                    throw new OpcException(String.Format("申请单{0}状态({1})未知", request.ApplyId, dto.ApproveStatus));
                    break;
            }




            return GetItem(request.ApplyId);
        }

        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExectueResult<ShopApplicationDto> SetDemotion(ApplyApprovedRequest request)
        {
            _inviteCodeRequestRepository.SetDemotion(request);

            return GetItem(request.ApplyId);
        }
    }
}
