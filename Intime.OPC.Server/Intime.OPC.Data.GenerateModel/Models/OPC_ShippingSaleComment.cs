using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class OPC_ShippingSaleComment
    {
        public int Id { get; set; }
        public string ShippingCode { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public Nullable<int> ShippingSaleId { get; set; }
    }
}
