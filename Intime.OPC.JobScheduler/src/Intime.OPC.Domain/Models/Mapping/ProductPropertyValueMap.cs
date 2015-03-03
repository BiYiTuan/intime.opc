using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductPropertyValueMapper : EntityTypeConfiguration<ProductPropertyValue>
    {
        public ProductPropertyValueMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ValueDesc)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductPropertyValue");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PropertyId).HasColumnName("PropertyId");
            this.Property(t => t.ValueDesc).HasColumnName("ValueDesc");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ChannelValueId).HasColumnName("ChannelValueId");
        }
    }
}
