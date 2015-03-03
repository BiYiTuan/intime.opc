using System;

namespace Intime.OPC.Domain.Models
{
    public class Section
    {
        public string Location { get; set; }
        public string ContactPhone { get; set; }
        public int? Status { get; set; }
        public int? BrandId { get; set; }
        public int? StoreId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUser { get; set; }
        public string ContactPerson { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string StoreCode { get; set; }
        public int? ChannelSectionId { get; set; }
    }
}