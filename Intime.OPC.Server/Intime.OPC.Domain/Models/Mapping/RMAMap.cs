using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RMAMap : EntityTypeConfiguration<RMA>
    {
        public RMAMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RMANo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Reason)
                .HasMaxLength(500);

            this.Property(t => t.BankName)
                .HasMaxLength(200);

            this.Property(t => t.BankAccount)
                .HasMaxLength(20);

            this.Property(t => t.BankCard)
                .HasMaxLength(50);

            this.Property(t => t.RejectReason)
                .HasMaxLength(500);

            this.Property(t => t.GiftReason)
                .HasMaxLength(100);

            this.Property(t => t.InvoiceReason)
                .HasMaxLength(100);

            this.Property(t => t.RebatePointReason)
                .HasMaxLength(100);

            this.Property(t => t.PostalFeeReason)
                .HasMaxLength(100);

            this.Property(t => t.ContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.ShipNo)
                .HasMaxLength(50);

            this.Property(t => t.MailAddress)
                .HasMaxLength(1000);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("RMA");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.RMAType).HasColumnName("RMAType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RMAAmount).HasColumnName("RMAAmount");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.BankCard).HasColumnName("BankCard");
            this.Property(t => t.RejectReason).HasColumnName("RejectReason");
            this.Property(t => t.rebatepostfee).HasColumnName("rebatepostfee");
            this.Property(t => t.chargepostfee).HasColumnName("chargepostfee");
            this.Property(t => t.ActualAmount).HasColumnName("ActualAmount");
            this.Property(t => t.GiftReason).HasColumnName("GiftReason");
            this.Property(t => t.InvoiceReason).HasColumnName("InvoiceReason");
            this.Property(t => t.RebatePointReason).HasColumnName("RebatePointReason");
            this.Property(t => t.PostalFeeReason).HasColumnName("PostalFeeReason");
            this.Property(t => t.ChargeGiftFee).HasColumnName("ChargeGiftFee");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.ShipviaId).HasColumnName("ShipviaId");
            this.Property(t => t.ShipNo).HasColumnName("ShipNo");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.MailAddress).HasColumnName("MailAddress");
            this.Property(t => t.RMAReason).HasColumnName("RMAReason");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
        }
    }
}
