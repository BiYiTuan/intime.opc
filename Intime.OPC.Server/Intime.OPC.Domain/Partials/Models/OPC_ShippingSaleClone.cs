using System;

namespace Intime.OPC.Domain.Partials.Models
{
    public partial class OPC_ShippingSaleClone
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


        public static OPC_ShippingSaleClone Convert2ShippingSale(dynamic obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OPC_ShippingSaleClone
            {
                Id = obj.Id,
                BrandId = obj.BrandId,
                CreateDate = obj.CreateDate,
                CreateUser = obj.CreateUser,
                OrderNo = obj.OrderNo,
                PrintTimes = obj.PrintTimes,
                RmaNo = obj.RmaNo,
                ShippingAddress = obj.ShippingAddress,
                ShippingCode =  obj.ShippingCode,
                ShippingStatus = obj.ShippingStatus,
                ShippingContactPerson = obj.ShippingContactPerson,
                ShippingContactPhone = obj.ShippingContactPhone,
                ShipViaId = obj.ShipViaId,
                ShipViaName = obj.ShipViaName,
                ShippingFee = obj.ShippingFee,
                ShippingRemark = obj.ShippingRemark,
                ShippingZipCode = obj.ShippingZipCode,
                StoreId = obj.StoreId,
                UpdateDate = obj.UpdateDate,
                UpdateUser = obj.UpdateUser
            };
        }
    }
}