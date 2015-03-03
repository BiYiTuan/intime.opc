using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_Bank : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
