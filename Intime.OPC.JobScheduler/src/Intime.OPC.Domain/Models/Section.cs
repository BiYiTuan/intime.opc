using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Section
    {
        public string Location { get; set; }
        public string ContactPhone { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public string ContactPerson { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string StoreCode { get; set; }
        public Nullable<int> ChannelSectionId { get; set; }
        public string SectionCode { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    }
}
