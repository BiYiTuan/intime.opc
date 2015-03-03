using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class IMS_Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool Visible4Display { get; set; }
    }
}
