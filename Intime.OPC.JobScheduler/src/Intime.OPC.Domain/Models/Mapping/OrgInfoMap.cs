using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OrgInfoMap : EntityTypeConfiguration<OrgInfo>
    {
        public OrgInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrgName)
                .HasMaxLength(50);

            this.Property(t => t.ParentID)
                .HasMaxLength(50);

            this.Property(t => t.StoreName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrgInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.OrgName).HasColumnName("OrgName");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.StoreID).HasColumnName("StoreID");
            this.Property(t => t.StoreName).HasColumnName("StoreName");
            this.Property(t => t.OrgType).HasColumnName("OrgType");
            this.Property(t => t.IsDel).HasColumnName("IsDel");
        }
    }
}
