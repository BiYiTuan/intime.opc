using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class OPC_CategoryMap
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public string ChannelCategoryCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string Channel { get; set; }
    }
}
