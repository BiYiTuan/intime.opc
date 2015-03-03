using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductUploadJob
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public Nullable<int> InUser { get; set; }
        public string FileName { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
