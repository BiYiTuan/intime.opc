using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OrderLogMapper : EntityTypeConfiguration<OrderLog>
    {
        public OrderLogMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Operation)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OrderLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.Operation).HasColumnName("Operation");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
