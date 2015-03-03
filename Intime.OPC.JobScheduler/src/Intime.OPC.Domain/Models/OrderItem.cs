using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int ProductId { get; set; }
        public string ProductDesc { get; set; }
        public string StoreItemNo { get; set; }
        public string StoreItemDesc { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ExtendPrice { get; set; }
        public int BrandId { get; set; }
        public int StoreId { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int UpdateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Points { get; set; }
        public string SalesPerson { get; set; }
        public Nullable<int> SizeId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> SizeValueId { get; set; }
        public Nullable<int> ColorValueId { get; set; }
        public string ColorValueName { get; set; }
        public string SizeValueName { get; set; }
        public string StoreSalesCode { get; set; }
        public Nullable<int> ProductType { get; set; }
    }
}
