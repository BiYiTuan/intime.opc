using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;

namespace Intime.OPC.Domain.Dto
{
    public class ShopApplicationDto
    {
        /// <summary>
        /// 申请信息主键
        /// </summary>
        public int Id { get; set; }

        public int StoreId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 营业部名称
        /// </summary>
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }

        /// <summary>
        /// 专柜编码
        /// </summary>
        public string SectionCode { get; set; }

        public string SectionName { get; set; }

        /// <summary>
        /// 导购姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 导购编号
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// 身份证号 ，当前留空
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 枚举状态
        /// </summary>
        public int ApproveStatus { get; set; }

        /// <summary>
        /// 枚举状态
        /// </summary>
        public string ApproveStatusName
        {
            get { return ((InviteCodeRequestStatus)ApproveStatus).GetDescription(); }
            private set { }
        }

        /// <summary>
        /// 是否发送了通知
        /// </summary>
        public bool IsNotified
        {
            get { return NotificationTimes > 0; }
            private set { }
        }

        /// <summary>
        /// 通知次数
        /// </summary>
        public int NotificationTimes { get; set; }
    }
}
