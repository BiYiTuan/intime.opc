using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Favorite
    {
        public int Id { get; set; }
        public int FavoriteSourceId { get; set; }
        public int FavoriteSourceType { get; set; }
        public int User_Id { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Store_Id { get; set; }
    }
}
