using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Request
{

    public class AssociateQueryRequest : DatePageRequest, IStoreDataRoleRequest
    {
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        public int? SectionId { get; set; }

        public int? Status { get; set; }

        /// <summary>
        /// 操作权限 向下兼容 1.仅有银泰卡权限 2.最高有系统商品权限的 3.最高有自拍商品权限的
        /// </summary>
        public int? OperatePermissions { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// 导购编号
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 导购姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int? Departmentid { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        public override void ArrangeParams()
        {
            base.ArrangeParams();

            OperatePermissions = CheckIsNullOrAndSet(OperatePermissions);
            SectionId = CheckIsNullOrAndSet(SectionId);
            Status = CheckIsNullOrAndSet(Status);
            Departmentid = CheckIsNullOrAndSet(Departmentid);
        }
    }

    public class ApplyQueryCriteriaRequest : DatePageRequest, IStoreDataRoleRequest
    {
        public string MobileNo { get; set; }
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
        public int? DepartmentId { get; set; }
        public string SectionCode { get; set; }
        public int? ApprovedStatus { get; set; }
        public string OperatorCode { get; set; }
        public string OperatorName { get; set; }
        int? IStoreDataRoleRequest.StoreId
        {
            get { return StoreId; }
            set { StoreId = value; }
        }

        public override void ArrangeParams()
        {
            base.ArrangeParams();

            ApprovedStatus = CheckIsNullOrAndSet(ApprovedStatus);

            DepartmentId = CheckIsNullOrAndSet(DepartmentId);
        }
    }

    public class ApplyApprovedRequest : IStoreDataRoleRequest
    {
        /// <summary>
        /// 审批 1表示审核通过 2:表示降权 //3：审核不通过
        /// </summary>
        public int? Approved { get; set; }

        public int ApplyId { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }

        public void ArrangeParams()
        {
            //if (Approved == -1)
            //{
            //    Approved = null;
            //}
        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// 降级
    /// </summary>
    public class ApplyDemotionRequest : IStoreDataRoleRequest
    {
        /// <summary>
        /// 审批 1表示审核通过 2:表示降权 //3：审核不通过
        /// </summary>
        public int? Approved { get; set; }

        public int ApplyId { get; set; }

        public void ArrangeParams()
        {
            //if (Approved == -1)
            //{
            //    Approved = null;
            //}
        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// 请求的通知
    /// </summary>
    public class ApplyNotifyRequest : IStoreDataRoleRequest
    {
        /// <summary>
        /// 可空，默认为1 ，
        /// </summary>
        public int? Times { get; set; }

        /// <summary>
        /// 通知
        /// </summary>
        public int? Notify { get; set; }

        public int ApplyId { get; set; }

        public void ArrangeParams()
        {
            //if (Approved == -1)
            //{
            //    Approved = null;
            //}

            Times = (Times == null || Times.Value < 1) ? 1 : Times.Value;
        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// 申请信息
    /// </summary>
    public class ApplyInfoRequest : IStoreDataRoleRequest
    {
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
        public int? DepartmentId { get; set; }
        public void ArrangeParams()
        {

        }

        int? IStoreDataRoleRequest.StoreId
        {
            get { return StoreId; }
            set { StoreId = value; }
        }
    }

}
