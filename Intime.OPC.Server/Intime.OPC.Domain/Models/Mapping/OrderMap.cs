using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PaymentMethodCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.PaymentMethodName)
                .HasMaxLength(20);

            this.Property(t => t.ShippingZipCode)
                .HasMaxLength(20);

            this.Property(t => t.ShippingAddress)
                .HasMaxLength(500);

            this.Property(t => t.ShippingContactPerson)
                .HasMaxLength(10);

            this.Property(t => t.ShippingContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.InvoiceSubject)
                .HasMaxLength(200);

            this.Property(t => t.InvoiceDetail)
                .HasMaxLength(200);

            this.Property(t => t.ShippingNo)
                .HasMaxLength(50);

            this.Property(t => t.Memo)
                .HasMaxLength(200);

            this.Property(t => t.OrderSource)
                .HasMaxLength(10);

            this.Property(t => t.PromotionRules).HasMaxLength(1000);
            this.Property(t => t.PromotionDesc).HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Order");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.RecAmount).HasColumnName("RecAmount");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PaymentMethodCode).HasColumnName("PaymentMethodCode");
            this.Property(t => t.PaymentMethodName).HasColumnName("PaymentMethodName");
            this.Property(t => t.ShippingZipCode).HasColumnName("ShippingZipCode");
            this.Property(t => t.ShippingAddress).HasColumnName("ShippingAddress");
            this.Property(t => t.ShippingContactPerson).HasColumnName("ShippingContactPerson");
            this.Property(t => t.ShippingContactPhone).HasColumnName("ShippingContactPhone");
            this.Property(t => t.NeedInvoice).HasColumnName("NeedInvoice");
            this.Property(t => t.InvoiceSubject).HasColumnName("InvoiceSubject");
            this.Property(t => t.InvoiceDetail).HasColumnName("InvoiceDetail");
            this.Property(t => t.ShippingFee).HasColumnName("ShippingFee");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.ShippingNo).HasColumnName("ShippingNo");
            this.Property(t => t.ShippingVia).HasColumnName("ShippingVia");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.Memo).HasColumnName("Memo");
            this.Property(t => t.InvoiceAmount).HasColumnName("InvoiceAmount");
            this.Property(t => t.TotalPoints).HasColumnName("TotalPoints");
            this.Property(t => t.OrderSource).HasColumnName("OrderSource");
            this.Property(t => t.OrderProductType).HasColumnName("OrderProductType");
            this.Property(t => t.PromotionRules).HasColumnName("PromotionRules");
            this.Property(t => t.PromotionDesc).HasColumnName("PromotionDesc");
        }
    }
}
