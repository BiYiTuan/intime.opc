using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class Order2ExMapper : EntityTypeConfiguration<Order2Ex>
    {
        public Order2ExMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ExOrderNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Order2Ex");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ExOrderNo).HasColumnName("ExOrderNo");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
