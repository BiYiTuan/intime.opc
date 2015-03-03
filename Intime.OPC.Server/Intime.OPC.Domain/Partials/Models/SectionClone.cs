using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Partials.Models;

namespace Intime.OPC.Domain.Models
{
    public class SectionClone
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
        public IEnumerable<BrandClone> Brands { get; set; }

        public string SectionCode { get; set; }

        public Store Store { get; set; }

        public static SectionClone Convert2Section(dynamic obj)
        {
            if (obj == null)
            {
                return null;
            }
          
            return new SectionClone()
            {
                BrandId = obj.BrandId,
                ChannelSectionId = obj.ChannelSectionId,
                ContactPerson = obj.ContactPerson,
                ContactPhone = obj.ContactPhone,
                CreateDate = obj.CreateDate,
                CreateUser = obj.CreateUser,
                Location = obj.Location,
                Name = obj.Name,
                Id = obj.Id,
                Status = obj.Status,
                StoreCode = obj.StoreCode,
                StoreId = obj.StoreId,
                UpdateDate = obj.UpdateDate,
                UpdateUser = obj.CreateUser,
                SectionCode = obj.SectionCode

            };
        }
    }
}