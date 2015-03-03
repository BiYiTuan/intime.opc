using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Brand_Id { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public decimal Price { get; set; }
        public string RecommendedReason { get; set; }
        public string Favorable { get; set; }
        public int RecommendUser { get; set; }
        public int Status { get; set; }
        public int Store_Id { get; set; }
        public int Tag_Id { get; set; }
        public int FavoriteCount { get; set; }
        public int ShareCount { get; set; }
        public int InvolvedCount { get; set; }
        public int RecommendSourceId { get; set; }
        public int RecommendSourceType { get; set; }
        public int SortOrder { get; set; }
        public bool IsHasImage { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<bool> Is4Sale { get; set; }
        public string SkuCode { get; set; }
        public string BarCode { get; set; }
        public string MoreDesc { get; set; }
        public Nullable<int> ProductType { get; set; }
    }
}
