using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class PropertyValueMapper : EntityTypeConfiguration<PropertyValue>
    {
        public PropertyValueMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyValue");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PropertyId).HasColumnName("PropertyId");
            this.Property(t => t.ValueId).HasColumnName("ValueId");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.HasChild).HasColumnName("HasChild");
        }
    }
}
