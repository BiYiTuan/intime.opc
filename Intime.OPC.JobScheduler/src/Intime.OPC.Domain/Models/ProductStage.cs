using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductStage
    {
        public int id { get; set; }
        public string name { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string DescripOfPromotion { get; set; }
        public Nullable<System.DateTime> DescripOfProBeginDate { get; set; }
        public Nullable<System.DateTime> DescripOfProEndDate { get; set; }
        public Nullable<int> InUserId { get; set; }
        public string Tag { get; set; }
        public string Store { get; set; }
        public string Promotions { get; set; }
        public string ItemCode { get; set; }
        public string Subjects { get; set; }
        public Nullable<int> UploadGroupId { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<bool> Is4Sale { get; set; }
        public string UPCCode { get; set; }
    }
}
