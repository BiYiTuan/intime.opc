using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 迷你银开店申请信息
    /// </summary>
    [Uri("shopapplies")]
    public class ApplicationInfo : Dimension
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName{get;set;}

        /// <summary>
        /// 组织结构名称
        /// </summary>
	    public string DepartmentName{get;set;}

        /// <summary>
        /// 专柜码
        /// </summary>
	    public string SectionCode{get;set;}

        /// <summary>
        /// 导购姓名
        /// </summary>
	    public string OperatorName{get;set;}

        /// <summary>
        /// 导购编码
        /// </summary>
	    public string OperatorCode{get;set;}

        /// <summary>
        /// 手机号码
        /// </summary>
	    public string MobileNo{get;set;}

        /// <summary>
        /// 身份证号
        /// </summary>
	    public string IdCardNo {get;set;}

        /// <summary>
        /// 审核状态
        /// </summary>
        public ApprovalStatus ApproveStatus { get; set; }

        /// <summary>
        /// 通知状态(已发送/未发送)
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知次数
        /// </summary>
	    public int NotificationTimes{get;set;}
    }
}
