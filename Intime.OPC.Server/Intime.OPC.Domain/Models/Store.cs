using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class Store:IEntity
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
    }
}
