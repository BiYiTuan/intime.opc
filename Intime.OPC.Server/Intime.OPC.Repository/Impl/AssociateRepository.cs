using System.Transactions;
using EntityFramework.Extensions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PredicateBuilder = LinqKit.PredicateBuilder;


namespace Intime.OPC.Repository.Impl
{
    public class AssociateRepository : OPCBaseRepository<int, IMS_Associate>, IAssociateRepository
    {
        static readonly int Giftcard = UserOperatorRight.GiftCard.AsId();
        static readonly int Sysproduct = UserOperatorRight.SystemProduct.AsId();
        static readonly int Seftproduct = UserOperatorRight.SelfProduct.AsId();

        #region methods

        /// <summary>
        /// 合伙人表筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<IMS_Associate, bool>> Filter(AssociateQueryRequest filter)
        {
            var query = PredicateBuilder.True<IMS_Associate>();

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

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }

                if (filter.SectionId != null)
                {
                    query = PredicateBuilder.And(query, v => v.SectionId == filter.SectionId.Value);
                }

                //操作权限 向下兼容 1.仅有银泰卡权限 2.最高有系统商品权限的 3.最高有自拍商品权限的
                if (filter.OperatePermissions != null)
                {
                    switch (filter.OperatePermissions)
                    {
                        case 1://
                            query = PredicateBuilder.And(query, v => v.OperateRight == Giftcard);
                            break;
                        case 2://
                            query = PredicateBuilder.And(query, v => v.OperateRight >= Sysproduct && v.OperateRight < Seftproduct);
                            break;
                        case 3:
                            query = PredicateBuilder.And(query, v => v.OperateRight >= Seftproduct);
                            break;
                        default:
                            query = PredicateBuilder.And(query, v => v.OperateRight >= filter.OperatePermissions);
                            break;

                    }
                }

                if (!String.IsNullOrEmpty(filter.OperatorCode))
                {
                    query = PredicateBuilder.And(query, v => v.OperatorCode == filter.OperatorCode);

                    return query;
                }

                if (!String.IsNullOrEmpty(filter.OperatorName) || !String.IsNullOrEmpty(filter.MobileNo) || !String.IsNullOrEmpty(filter.EMail))
                {

                }
                else
                {
                    if (filter.StartDate != null)
                    {
                        query = PredicateBuilder.And(query, v => v.CreateDate >= filter.StartDate);
                    }

                    if (filter.EndDate != null)
                    {
                        query = PredicateBuilder.And(query, v => v.CreateDate < filter.EndDate);
                    }

                }
            }

            return query;
        }

        private static Func<IQueryable<IMS_Associate>, IOrderedQueryable<IMS_Associate>> OrderBy()
        {
            Func<IQueryable<IMS_Associate>, IOrderedQueryable<IMS_Associate>> orderBy = null;


            return orderBy;
        }

        /// <summary>
        /// 专柜表 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Section, bool>> SectionFilter(AssociateQueryRequest filter)
        {
            var query = PredicateBuilder.True<Section>();

            if (!String.IsNullOrEmpty(filter.SectionCode))
            {
                query = PredicateBuilder.And(query, v => v.SectionCode == filter.SectionCode);
            }

            if (filter.Departmentid != null)
            {
                query = PredicateBuilder.And(query, v => v.DepartmentId == filter.Departmentid.Value);
            }

            return query;
        }

        /// <summary>
        /// 用户表筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<User, bool>> UserFilter(AssociateQueryRequest filter)
        {
            var query = PredicateBuilder.True<User>();

            if (!String.IsNullOrEmpty(filter.OperatorName))
            {
                query = PredicateBuilder.And(query, v => v.Nickname == filter.OperatorName);
            }

            if (!String.IsNullOrEmpty(filter.MobileNo))
            {
                query = PredicateBuilder.And(query, v => v.Mobile == filter.MobileNo);
            }

            if (!String.IsNullOrEmpty(filter.EMail))
            {
                query = PredicateBuilder.And(query, v => v.EMail == filter.EMail);
            }

            return query;
        }


        #endregion

        public override IEnumerable<IMS_Associate> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PagerInfo<AssociateDto> GetPagedList(AssociateQueryRequest request, PagerRequest pagerRequest)
        {
            var associateFilter = Filter(request);
            var sectionFilter = SectionFilter(request);
            var userFilter = UserFilter(request);
            using (var db = GetYintaiHZhouContext())
            {
                var associates = db.IMS_Associate;
                var sections = db.Sections;
                var stores = db.Stores;
                var users = db.Users;
                var deptments = db.Departments;

                var q = from associate in associates.AsExpandable().Where(associateFilter)
                        join section in sections.AsExpandable().Where(sectionFilter) on associate.SectionId equals section.Id
                        join store in stores on associate.StoreId equals store.Id
                        join user in users.AsExpandable().Where(userFilter) on associate.UserId equals user.Id
                        join deptment in deptments on section.DepartmentId equals deptment.Id into tmp1
                        from deptment in tmp1.DefaultIfEmpty()
                        select new AssociateDto
                        {
                            StoreId = store.Id,
                            StoreName = store.Name,
                            SectionCode = section.SectionCode,
                            SectionId = section.Id,
                            SectionName = section.Name,
                            OperateRight = associate.OperateRight,
                            OperatorCode = associate.OperatorCode,
                            CreateDate = associate.CreateDate,
                            CreateUser = associate.CreateUser,
                            Id = associate.Id,
                            Status = associate.Status,
                            TemplateId = associate.TemplateId,
                            UserId = associate.UserId,
                            UserNickName = user.Nickname,
                            Mobile = user.Mobile,
                            Email = user.EMail,
                            UserName = user.Name,
                            UserLevel = user.UserLevel,
                            DepartmentId = deptment == null ? 0 : deptment.Id,
                            DepartmentName = deptment == null ? null : deptment.Name
                        };
                var totalCount = q.Count();

                var datas = q.OrderByDescending(v => v.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();

                return new PagerInfo<AssociateDto>(pagerRequest, totalCount) { Datas = datas };
            }
        }

        public AssociateDto GetDto(int id)
        {
            using (var db = GetYintaiHZhouContext())
            {
                var associates = db.IMS_Associate;
                var sections = db.Sections;
                var stores = db.Stores;
                var users = db.Users;
                var deptments = db.Departments;

                var q = from associate in associates.Where(v => v.Id == id)
                        join section in sections on associate.SectionId equals section.Id
                        join store in stores on associate.StoreId equals store.Id
                        join user in users on associate.UserId equals user.Id
                        join deptment in deptments on section.DepartmentId equals deptment.Id into tmp1
                        from deptment in tmp1.DefaultIfEmpty()
                        select new AssociateDto
                        {
                            StoreId = store.Id,
                            StoreName = store.Name,
                            SectionCode = section.SectionCode,
                            SectionId = section.Id,
                            SectionName = section.Name,
                            OperateRight = associate.OperateRight,
                            OperatorCode = associate.OperatorCode,
                            CreateDate = associate.CreateDate,
                            CreateUser = associate.CreateUser,
                            Id = associate.Id,
                            Status = associate.Status,
                            TemplateId = associate.TemplateId,
                            UserId = associate.UserId,
                            UserNickName = user.Nickname,
                            Mobile = user.Mobile,
                            Email = user.EMail,
                            UserName = user.Name,
                            UserLevel = user.UserLevel,
                            DepartmentId = deptment == null ? 0 : deptment.Id,
                            DepartmentName = deptment == null ? null : deptment.Name

                        };
                return q.FirstOrDefault();
            }
        }

        /// <summary>
        /// 降权 目前对于降权会清空表，所以要注意权限
        /// </summary>
        public void SetOperate(SetAssociateOperateRequest request)
        {
            //var userid = 0;
            //var associateId = 0;
            //var authUserId = 0;

            using (var ts = new TransactionScope())
            {
                using (var db = GetYintaiHZhouContext())
                {
                    var associate = db.IMS_Associate;
                    var entity = associate.FirstOrDefault(v => v.Id == request.AssociateId);
                    var associatesalecodes = db.IMS_AssociateSaleCode;

                    if (entity == null)
                    {
                        throw new OpcException(String.Format("合伙人ID:({0})未找到。", request.AssociateId));
                    }

                    //if (entity.UserId != userid)
                    //{
                    //    throw new OpcException(String.Format("合伙人ID:({0})的UserId{1}与提供的UserId{2}不一致。", associateId, entity.UserId, userid));
                    //}

                    entity.OperateRight = request.OperateRight.AsId();

                    associatesalecodes.Where(v => v.AssociateId == request.AssociateId).Delete();

                    db.SaveChanges();
                }

                ts.Complete();
            }
        }
    }
}
