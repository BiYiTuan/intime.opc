using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductPropertyMapper : EntityTypeConfiguration<ProductProperty>
    {
        public ProductPropertyMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PropertyDesc)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductProperty");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.PropertyDesc).HasColumnName("PropertyDesc");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.IsColor).HasColumnName("IsColor");
            this.Property(t => t.IsSize).HasColumnName("IsSize");
            this.Property(t => t.ChannelPropertyId).HasColumnName("ChannelPropertyId");
        }
    }
}
