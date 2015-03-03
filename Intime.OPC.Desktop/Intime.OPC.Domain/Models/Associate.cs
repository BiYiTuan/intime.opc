using System;

using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 导购信息
    /// </summary>
    [Uri("associates")]
    public class Associate : Dimension
    {
        private int departmentName;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public int? UserLevel { get; set; }

        /// <summary>
        /// 用户级别名称
        /// </summary>
        public string UserLevelName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Template ID
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        /// 操作权限
        /// </summary>
        public int? OperateRight { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 专柜ID
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 专柜名称
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 操作权限名字
        /// </summary>
        public string OperateRightName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 营业部ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 营业部名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
