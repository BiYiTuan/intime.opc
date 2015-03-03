using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class ExOrderMap : EntityTypeConfiguration<ExOrder>
    {
        public ExOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ExOrderNo)
                .HasMaxLength(100);

            this.Property(t => t.PaymentCode)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ExOrder");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExOrderNo).HasColumnName("ExOrderNo");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.PaymentCode).HasColumnName("PaymentCode");
            this.Property(t => t.PaidDate).HasColumnName("PaidDate");
            this.Property(t => t.IsShipped).HasColumnName("IsShipped");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.OrderType).HasColumnName("OrderType");
        }
    }
}
