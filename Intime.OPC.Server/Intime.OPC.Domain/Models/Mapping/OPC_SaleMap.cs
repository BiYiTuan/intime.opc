using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SaleMap : EntityTypeConfiguration<OPC_Sale>
    {
        public OPC_SaleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShippingCode)
                .HasMaxLength(50);

            this.Property(t => t.ShippingRemark)
                .HasMaxLength(500);

            this.Property(t => t.CashNum)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("OPC_Sale");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShippingSaleId).HasColumnName("ShippingSaleId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.SaleOrderNo).HasColumnName("SaleOrderNo");
            this.Property(t => t.SalesType).HasColumnName("SalesType");
            this.Property(t => t.ShipViaId).HasColumnName("ShipViaId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ShippingCode).HasColumnName("ShippingCode");
            this.Property(t => t.ShippingFee).HasColumnName("ShippingFee");
            this.Property(t => t.ShippingStatus).HasColumnName("ShippingStatus");
            this.Property(t => t.ShippingRemark).HasColumnName("ShippingRemark");
            this.Property(t => t.SellDate).HasColumnName("SellDate");
            this.Property(t => t.IfTrans).HasColumnName("IfTrans");
            this.Property(t => t.TransStatus).HasColumnName("TransStatus");
            this.Property(t => t.SalesAmount).HasColumnName("SalesAmount");
            this.Property(t => t.SalesCount).HasColumnName("SalesCount");
            this.Property(t => t.CashStatus).HasColumnName("CashStatus");
            this.Property(t => t.CashNum).HasColumnName("CashNum");
            this.Property(t => t.CashDate).HasColumnName("CashDate");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.PrintTimes).HasColumnName("PrintTimes");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.RemarkDate).HasColumnName("RemarkDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
