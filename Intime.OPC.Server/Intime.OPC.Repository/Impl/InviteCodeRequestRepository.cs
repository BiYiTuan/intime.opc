using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{
    public class InviteCodeRequestRepository : OPCBaseRepository<int, IMS_InviteCodeRequest>, IInviteCodeRequestRepository
    {
        /// <summary>
        /// 已审核状态
        /// </summary>
        private static readonly List<int> ApprovedStatus = new List<int>
            {
                InviteCodeRequestStatus.Approved.AsId(),
                InviteCodeRequestStatus.Reject.AsId()
            };

        private static readonly int NotApproved = InviteCodeRequestStatus.Requesting.AsId();

        #region methods

        private static Expression<Func<IMS_InviteCodeRequest, bool>> Filter(ApplyQueryCriteriaRequest filter)
        {
            var query = PredicateBuilder.True<IMS_InviteCodeRequest>();

            if (filter != null)
            {
                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.StoreId == filter.StoreId.Value);
                }

                if (filter.StoreId == null && filter.DataRoleStores != null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.StoreId));
                }

                if (filter.ApprovedStatus != null)
                {
                    ////未审核状态  白名单方式
                    //if (filter.ApprovedStatus.Value == 1)
                    //{
                    //    query = PredicateBuilder.And(query, v => v.Status == NotApproved);
                    //}
                    //else if (filter.ApprovedStatus.Value == 0)
                    //{
                    //    query = PredicateBuilder.And(query, v => ApprovedStatus.Contains(v.Status));
                    //}

                    query = PredicateBuilder.And(query, v => v.Status == filter.ApprovedStatus.Value);
                }

                if (!String.IsNullOrWhiteSpace(filter.MobileNo))
                {
                    query = PredicateBuilder.And(query, v => v.ContactMobile == filter.MobileNo);
                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.OperatorCode))
                {
                    query = PredicateBuilder.And(query, v => v.OperatorCode == filter.OperatorCode);
                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.OperatorName))
                {
                    query = PredicateBuilder.And(query, v => v.Name == filter.OperatorName);
                }

                if (filter.DepartmentId != null)
                {
                    query = PredicateBuilder.And(query, v => v.DepartmentId == filter.DepartmentId.Value);
                }

                if (filter.StartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate >= filter.StartDate);
                }

                if (filter.EndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate < filter.EndDate);
                }

            }

            return query;
        }

        private static Func<IQueryable<IMS_InviteCodeRequest>, IOrderedQueryable<IMS_InviteCodeRequest>> OrderBy(ApplySortOrder sortOrder)
        {
            Func<IQueryable<IMS_InviteCodeRequest>, IOrderedQueryable<IMS_InviteCodeRequest>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    //orderBy = v => v.OrderByDescending(s => s);
                    break;
            }

            return orderBy;
        }


        #endregion

        public override IEnumerable<IMS_InviteCodeRequest> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PagerInfo<ShopApplicationDto> GetPagedList(ApplyQueryCriteriaRequest request, PagerRequest pagerRequest)
        {
            var invitecodefilter = Filter(request);

            using (var db = GetYintaiHZhouContext())
            {
                var inviteCodeRequest = db.IMS_InviteCodeRequest;
                var stores = db.Stores;
                var departments = db.Departments;

                var q = from icr in inviteCodeRequest.AsExpandable().Where(invitecodefilter)
                        join store in stores on icr.StoreId equals store.Id
                        join department in departments on icr.DepartmentId equals department.Id
                        select new ShopApplicationDto
                        {
                            ApproveStatus = icr.Status,
                            DepartmentName = department.Name,
                            DepartmentId = icr.DepartmentId,
                            Id = icr.Id,
                            IdCardNo = icr.IdCard,
                            //IsNotified = true,//<---------
                            MobileNo = icr.ContactMobile,
                            NotificationTimes = icr.ApprovedNotificationTimes ?? 0 + icr.DemotionNotificationTimes ?? 0,
                            OperatorCode = icr.OperatorCode,
                            OperatorName = icr.Name,
                            SectionCode = icr.SectionCode,
                            SectionName = icr.SectionName,
                            StoreId = icr.StoreId,
                            StoreName = store.Name,

                        };
                var totalCount = q.Count();
                var datas = q.OrderByDescending(v => v.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();

                return new PagerInfo<ShopApplicationDto>(pagerRequest, totalCount) { Datas = datas };
            }
        }

        public ShopApplicationDto GetDto(int id)
        {
            using (var db = GetYintaiHZhouContext())
            {
                var inviteCodeRequest = db.IMS_InviteCodeRequest;
                var stores = db.Stores;
                var departments = db.Departments;

                var q = from icr in inviteCodeRequest.Where(v => v.Id == id)
                        join store in stores on icr.StoreId equals store.Id
                        join department in departments on icr.DepartmentId equals department.Id
                        select new ShopApplicationDto
                        {
                            ApproveStatus = icr.Status,
                            DepartmentName = department.Name,
                            DepartmentId = icr.DepartmentId,
                            Id = icr.Id,
                            IdCardNo = icr.IdCard,
                            //IsNotified = true,//<---------
                            MobileNo = icr.ContactMobile,
                            NotificationTimes = icr.ApprovedNotificationTimes ?? 0 + icr.DemotionNotificationTimes ?? 0,
                            OperatorCode = icr.OperatorCode,
                            OperatorName = icr.Name,
                            SectionCode = icr.SectionCode,
                            SectionName = icr.SectionName,
                            StoreId = icr.StoreId,
                            StoreName = store.Name,

                        };

                var data = q.FirstOrDefault();

                return data;
            }
        }

        public void SetApproved(ApplyApprovedRequest request)
        {
            var item = GetItem(request.ApplyId);
            if (item == null)
            {
                throw new OpcException(String.Format("申请单{0}未找到", request.ApplyId));
            }

            using (var ts = new TransactionScope())
            {
                using (var db = GetYintaiHZhouContext())
                {
                    var approved = request.Approved == 1;

                    var stdField = new List<string>
                    {
                        "Approved",
                        "ApprovedDate",
                        "ApprovedBy",
                        "UpdateDate",
                        "UpdateUser",
                        "Status"
                    };

                    item.Approved = approved;
                    item.ApprovedDate = DateTime.Now;
                    item.ApprovedBy = request.CurrentUserId;
                    item.UpdateDate = DateTime.Now;
                    item.UpdateUser = request.CurrentUserId ?? 0;
                    item.Status = approved ? InviteCodeRequestStatus.Approved.AsId() : InviteCodeRequestStatus.Reject.AsId();

                    if (!approved)
                    {
                        item.RejectReason = request.Reason;
                        stdField.Add("RejectReason");
                    }

                    EFHelper.UpdateEntityFields(db, item, stdField, false);

                    if (approved)
                    {
                        //init
                        InitializeAssociateTables(db, item, request.CurrentUserId ?? 0);
                    }

                    db.SaveChanges();
                }

                ts.Complete();
            }
        }

        /// <summary>
        /// 降级
        /// </summary>
        /// <param name="request"></param>
        public void SetDemotion(ApplyApprovedRequest request)
        {

            throw new NotImplementedException();

            var item = GetItem(request.ApplyId);
            if (item == null)
            {
                throw new OpcException(String.Format("申请单{0}未找到", request.ApplyId));
            }

            if (!item.Approved.HasValue || !item.Approved.Value)
            {
                throw new OpcException(String.Format("申请单{0}还未通过审核", request.ApplyId));
            }

            using (var ts = new TransactionScope())
            {

                using (var db = GetYintaiHZhouContext())
                {
                    var imsAssociate = db.Set<IMS_Associate>();
                    var sections = db.Set<Section>();

                    var section =
                        sections.FirstOrDefault(v => v.StoreId == item.StoreId && v.SectionCode == item.SectionCode);

                    if (section == null)
                    {
                        throw new OpcException(String.Format("申请单{0}门店{1}专柜码{2}专柜未找到", item.Id, item.StoreId,
                                                             item.SectionCode));
                    }

                    var associate =
                        imsAssociate.FirstOrDefault(
                            v =>
                            v.UserId == item.UserId && v.Status == 1 && v.SectionId == section.Id &&
                            v.StoreId == section.StoreId && v.OperatorCode == item.OperatorCode);

                    if (associate == null)
                    {
                        throw new OpcException(String.Format("申请单{0}未找到关联表", item.Id));
                    }

                    if (associate.OperateRight == null)
                    {
                        throw new OpcException(String.Format("申请单{0}未找到关联表{1}OperateRight=null", item.Id,
                                                             associate.OperateRight));
                    }

                    var uor = (UserOperatorRight)associate.OperateRight;

                    var b = (uor & UserOperatorRight.SelfProduct) != 0;
                    if (!b)
                    {
                        throw new OpcException(String.Format("申请单{0}已经是{1}({2})", item.Id, uor.GetDescription(), uor));
                    }

                    associate.OperateRight = (uor & (~UserOperatorRight.SelfProduct)).AsId();

                    // item.Status = InviteCodeRequestStatus.Demotion.AsID();

                    EFHelper.UpdateEntityFields(db, item, new[] { "Status" });

                    EFHelper.UpdateEntityFields(db, associate, new[] { "OperateRight" });
                }
                ts.Complete();
            }
        }

        /// <summary>
        /// 开店初始化
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <param name="currentUserId"></param>
        private void InitializeAssociateTables(DbContext context, IMS_InviteCodeRequest entity, int currentUserId)
        {
            //steps:
            // 1. read initial info from invite code tables
            // 2. initialize associate tables

            //var initialBrands = Context.Set<IMS_SectionBrand>().Where(isb => isb.SectionId == inviteEntity.Sec.SectionId);
            //var initialSaleCodes = Context.Set<IMS_SalesCode>().Where(isc => isc.SectionId == inviteEntity.Sec.SectionId);
            var users = context.Set<User>();
            var sections = context.Set<Section>();
            var imsAssociate = context.Set<IMS_Associate>();
            var sectionbrands = context.Set<IMS_SectionBrand>();
            var salescodes = context.Set<IMS_SalesCode>();
            var giftcards = context.Set<IMS_GiftCard>();

            //1验证
            var exitUserEntity = users.FirstOrDefault(v => v.Id == entity.UserId);
            if (exitUserEntity == null)
            {
                throw new OpcException(String.Format("申请单{0}用户{1}未找到", entity.Id, entity.UserId));
            }

            var section =
                sections.FirstOrDefault(v => v.StoreId == entity.StoreId && v.SectionCode == entity.SectionCode);
            if (section == null)
            {
                throw new OpcException(String.Format("申请单{0}门店{1}专柜码{2}专柜未找到", entity.Id, entity.StoreId, entity.SectionCode));
            }

            var requestType = (InviteCodeRequestType)entity.RequestType;
            var operateRight = requestType == InviteCodeRequestType.Daogou
                ? (UserOperatorRight.GiftCard | UserOperatorRight.SelfProduct | UserOperatorRight.SystemProduct)
                : (UserOperatorRight.GiftCard | UserOperatorRight.SystemProduct);


            var associateEntity = imsAssociate.FirstOrDefault(ia => ia.UserId == exitUserEntity.Id);
            //新业务中 当前用户是导购 ,那么需要更新 associateEntity表即可，不用初始化其他表了 
            if (associateEntity != null)
            {
                associateEntity.OperateRight = operateRight.AsId();
                associateEntity.SectionId = section.Id;
                associateEntity.StoreId = entity.StoreId;
                associateEntity.OperatorCode = entity.OperatorCode;

                return;
            }

            var initialBrands = sectionbrands.Where(v => v.SectionId == section.Id);
            var initialSaleCodes = salescodes.Where(v => v.SectionId == section.Id);

            //2.1 update user level to daogou
            exitUserEntity.UserLevel = (int)UserLevel.DaoGou;
            exitUserEntity.UpdatedDate = DateTime.Now;
            exitUserEntity.UpdatedUser = currentUserId;
            if (String.IsNullOrEmpty(exitUserEntity.Logo))
            {
                exitUserEntity.Logo = ConfigManager.IMS_DEFAULT_LOGO;
            }
            //EFHelper.Update(context, exitUserEntity);

            //2.2 create daogou's associate store


            var associate = new IMS_Associate
            {
                CreateDate = DateTime.Now,
                CreateUser = currentUserId,
                OperateRight = operateRight.AsId(),//inviteEntity.Inv.AuthRight.Value,<----------------------------
                Status = 1,
                TemplateId = ConfigManager.IMS_DEFAULT_TEMPLATE,
                UserId = exitUserEntity.Id,
                StoreId = entity.StoreId,
                SectionId = section.Id,
                OperatorCode = entity.OperatorCode
            };

            var assocateEntity = context.Set<IMS_Associate>().Add(associate);
            context.SaveChanges();

            //2.3 create daogou's brands
            foreach (var brand in initialBrands)
            {
                context.Set<IMS_AssociateBrand>().Add(new IMS_AssociateBrand
                {
                    AssociateId = assocateEntity.Id,
                    BrandId = brand.BrandId,
                    CreateDate = DateTime.Now,
                    CreateUser = currentUserId,
                    Status = 1,
                    UserId = exitUserEntity.Id
                });
            }
            //2.4 create daogou's sales code
            foreach (var saleCode in initialSaleCodes)
            {
                context.Set<IMS_AssociateSaleCode>().Add(new IMS_AssociateSaleCode
                {
                    AssociateId = assocateEntity.Id,
                    Code = saleCode.Code,
                    CreateDate = DateTime.Now,
                    CreateUser = currentUserId,
                    Status = 1,
                    UserId = exitUserEntity.Id

                });
            }
            //2.5 create daogou's giftcard
            var giftCardEntity = giftcards.FirstOrDefault(igc => igc.Status == 1);

            if (giftCardEntity != null)
            {
                context.Set<IMS_AssociateItems>().Add(new IMS_AssociateItems
                {
                    AssociateId = assocateEntity.Id,
                    CreateDate = DateTime.Now,
                    CreateUser = currentUserId,
                    ItemId = giftCardEntity.Id,
                    ItemType = (int)ComboType.GiftCard,
                    Status = 1,
                    UpdateDate = DateTime.Now,
                    UpdateUser = currentUserId
                });
            }
        }

        public void SetApprovedNotificationTimes(int id, int times)
        {
            using (var db = GetYintaiHZhouContext())
            {
                var item = GetItem(id);
                if (item == null)
                {
                    throw new OpcException(String.Format("申请单{0}未找到", id));
                }

                item.ApprovedNotificationTimes += times;

                EFHelper.UpdateEntityFields(db, item, new List<string>
                    {
                        "ApprovedNotificationTimes"
                    });
            }
        }

        public void SetDemotionNotificationTimes(int id, int times)
        {

            throw new NotImplementedException();

            using (var db = GetYintaiHZhouContext())
            {
                var item = GetItem(id);
                if (item == null)
                {
                    throw new OpcException(String.Format("申请单{0}未找到", id));
                }

                //var status = (InviteCodeRequestStatus)item.Status;
                //if (status == InviteCodeRequestStatus.Reject)
                //{
                item.DemotionNotificationTimes += times;

                EFHelper.UpdateEntityFields(db, item, new List<string>
                    {
                        "DemotionNotificationTimes"
                    });
                //}
                //else
                //{
                //    throw new OpcException(String.Format("申请单{0}状态为{1}({2})", id, status.GetDescription(), status.AsID()));
                //}
            }
        }
    }
}
