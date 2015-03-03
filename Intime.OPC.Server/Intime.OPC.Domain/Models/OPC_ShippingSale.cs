using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_ShippingSale:IEntity
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingContactPerson { get; set; }
        public string ShippingContactPhone { get; set; }
        public Nullable<int> ShipViaId { get; set; }
        public string ShipViaName { get; set; }
        public string ShippingCode { get; set; }
        public Nullable<decimal> ShippingFee { get; set; }
        public Nullable<int> ShippingStatus { get; set; }
        public string ShippingRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }

        public string RmaNo { get; set; }

        public int PrintTimes { get; set; }
    }
}
