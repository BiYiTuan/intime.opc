using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class IMS_AssociateIncomeTran2ReqMap : EntityTypeConfiguration<IMS_AssociateIncomeTran2Req>
    {
        public IMS_AssociateIncomeTran2ReqMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeTran2Req");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FullPackageId).HasColumnName("FullPackageId");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
        }
    }
}
