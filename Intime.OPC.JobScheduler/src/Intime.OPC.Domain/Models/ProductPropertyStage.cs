using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductPropertyStage
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string PropertyDesc { get; set; }
        public string ValueDesc { get; set; }
        public int SortOrder { get; set; }
        public int UploadGroupId { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
