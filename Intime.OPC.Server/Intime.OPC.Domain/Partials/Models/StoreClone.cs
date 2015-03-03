using System;

namespace Intime.OPC.Domain.Partials.Models
{
    public class StoreClone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tel { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Group_Id { get; set; }
        public int Status { get; set; }
        public int Region_Id { get; set; }
        public int StoreLevel { get; set; }
        public Nullable<decimal> GpsLat { get; set; }
        public Nullable<decimal> GpsLng { get; set; }
        public Nullable<decimal> GpsAlt { get; set; }
        public Nullable<int> ExStoreId { get; set; }
        public string RMAAddress { get; set; }
        public string RMAZipCode { get; set; }
        public string RMAPerson { get; set; }
        public string RMAPhone { get; set; }

        public static StoreClone Convert2Store(dynamic obj)
        {
            if (obj == null)
            {
                return null;
            }

            //return AutoMapper.Mapper.DynamicMap<Brand>(obj);


            return new StoreClone()
            {
                Id = obj.Id,
                Name = obj.Name,
                ExStoreId = obj.ExStoreId,
                GpsAlt = obj.GpsAlt,
                GpsLng = obj.GpsLng,
                GpsLat = obj.GpsLat,
                Group_Id = obj.Group_Id,
                Latitude = obj.Latitude,
                Location = obj.Location,
                Longitude = obj.Longitude,
                Region_Id = obj.Region_Id,
                RMAAddress = obj.RMAAddress,
                RMAZipCode = obj.RMAZipCode,
                RMAPerson = obj.RMAPerson,
                RMAPhone = obj.RMAPhone,
                Tel = obj.Tel,
                StoreLevel = obj.StoreLevel,

                CreatedDate = obj.CreatedDate,
                CreatedUser = obj.CreatedUser,
                Description = obj.Description,
                Status = obj.Status,
                UpdatedDate = obj.UpdatedDate,
                UpdatedUser = obj.CreatedUser
            };
        }
    }
}