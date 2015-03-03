using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OrderTransactionMap : EntityTypeConfiguration<OrderTransaction>
    {
        public OrderTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PaymentCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.TransNo)
                .HasMaxLength(100);

            this.Property(t => t.OutsiteUId)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("OrderTransaction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.PaymentCode).HasColumnName("PaymentCode");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.TransNo).HasColumnName("TransNo");
            this.Property(t => t.IsSynced).HasColumnName("IsSynced");
            this.Property(t => t.SyncDate).HasColumnName("SyncDate");
            this.Property(t => t.OutsiteUId).HasColumnName("OutsiteUId");
            this.Property(t => t.OutsiteType).HasColumnName("OutsiteType");
            this.Property(t => t.OrderType).HasColumnName("OrderType");
            this.Property(t => t.CanSync).HasColumnName("CanSync");
        }
    }
}
