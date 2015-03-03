using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class ProductProperty : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PropertyDesc { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedUser { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsColor { get; set; }
        public Nullable<bool> IsSize { get; set; }
        public Nullable<int> ChannelPropertyId { get; set; }
    }
}
