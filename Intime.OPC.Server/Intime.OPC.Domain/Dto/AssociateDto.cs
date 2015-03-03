using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;

namespace Intime.OPC.Domain.Dto
{
    public class AssociateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Status { get; set; }
        public Nullable<int> TemplateId { get; set; }
        public Nullable<int> OperateRight { get; set; }
        public int StoreId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string OperatorCode { get; set; }

        public string SectionName { get; set; }
        public string SectionCode { get; set; }

        public string StoreName { get; set; }

        public string OperateRightName
        {
            get
            {
                if (OperateRight == null)
                {
                    return "";
                }

                return ((UserOperatorRight)OperateRight).GetDescription();
            }
            set { }
        }

        public string UserNickName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int? UserLevel { get; set; }

        public string UserLevelName
        {
            get
            {
                if (UserLevel == null)
                {
                    return "";
                }

                return ((UserLevel)UserLevel).GetDescription();
            }
            set { }
        }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
