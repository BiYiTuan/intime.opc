using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class IMS_Associate
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
    }
}
