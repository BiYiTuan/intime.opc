namespace Intime.OPC.Domain.Models
{
    public partial class Store
    {
        public static Store Convert2Store(dynamic obj)
        {
            if (obj == null)
            {
                return null;
            }

            //return AutoMapper.Mapper.DynamicMap<Brand>(obj);


            return new Store()
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