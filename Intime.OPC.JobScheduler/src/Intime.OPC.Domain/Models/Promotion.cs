using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int LikeCount { get; set; }
        public int FavoriteCount { get; set; }
        public int ShareCount { get; set; }
        public int InvolvedCount { get; set; }
        public int Store_Id { get; set; }
        public int RecommendUser { get; set; }
        public int RecommendSourceId { get; set; }
        public int RecommendSourceType { get; set; }
        public int Tag_Id { get; set; }
        public bool IsTop { get; set; }
        public Nullable<bool> IsProdBindable { get; set; }
        public Nullable<int> PublicationLimit { get; set; }
        public Nullable<bool> IsMain { get; set; }
        public Nullable<bool> IsLimitPerUser { get; set; }
        public string PublicProCode { get; set; }
        public Nullable<bool> IsCodeUseLimit { get; set; }
    }
}
