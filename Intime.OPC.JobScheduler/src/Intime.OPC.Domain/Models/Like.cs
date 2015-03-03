using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public int LikeUserId { get; set; }
        public int LikedUserId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
