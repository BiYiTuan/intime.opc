using System;

namespace Intime.OPC.Domain.Models
{
    public class OPC_SaleComment
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
    }
}