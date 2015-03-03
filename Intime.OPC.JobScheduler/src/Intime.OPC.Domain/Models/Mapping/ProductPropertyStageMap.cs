using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductPropertyStageMapper : EntityTypeConfiguration<ProductPropertyStage>
    {
        public ProductPropertyStageMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ItemCode)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PropertyDesc)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ValueDesc)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductPropertyStage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.PropertyDesc).HasColumnName("PropertyDesc");
            this.Property(t => t.ValueDesc).HasColumnName("ValueDesc");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.UploadGroupId).HasColumnName("UploadGroupId");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
