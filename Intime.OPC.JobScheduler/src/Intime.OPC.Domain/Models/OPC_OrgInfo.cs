using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_OrgInfo
    {
        public int ID { get; set; }
        public string OrgID { get; set; }
        public string OrgName { get; set; }
        public string ParentID { get; set; }
        public Nullable<int> StoreOrSectionID { get; set; }
        public string StoreOrSectionName { get; set; }
        public Nullable<int> OrgType { get; set; }
        public bool IsDel { get; set; }
    }
}
