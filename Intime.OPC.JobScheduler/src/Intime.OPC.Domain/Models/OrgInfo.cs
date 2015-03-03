using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OrgInfo
    {
        public int ID { get; set; }
        public string OrgName { get; set; }
        public string ParentID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> OrgType { get; set; }
        public Nullable<bool> IsDel { get; set; }
    }
}
