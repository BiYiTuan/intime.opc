using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_OrgInfoMap : EntityTypeConfiguration<OPC_OrgInfo>
    {
        public OPC_OrgInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrgID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OrgName)
                .HasMaxLength(50);

            this.Property(t => t.ParentID)
                .HasMaxLength(50);

            this.Property(t => t.StoreOrSectionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_OrgInfo");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.OrgName).HasColumnName("OrgName");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.StoreOrSectionID).HasColumnName("StoreOrSectionID");
            this.Property(t => t.StoreOrSectionName).HasColumnName("StoreOrSectionName");
            this.Property(t => t.OrgType).HasColumnName("OrgType");
            this.Property(t => t.IsDel).HasColumnName("IsDel");
        }
    }
}
