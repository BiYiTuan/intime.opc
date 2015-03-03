using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SaleDetailMap : EntityTypeConfiguration<OPC_SaleDetail>
    {
        public OPC_SaleDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProdSaleCode)
                .HasMaxLength(50);
            this.Property(t => t.SectionCode)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("OPC_SaleDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SaleOrderNo).HasColumnName("SaleOrderNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StockId).HasColumnName("StockId");
            this.Property(t => t.SaleCount).HasColumnName("SaleCount");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.BackNumber).HasColumnName("BackNumber");
            this.Property(t => t.ProdSaleCode).HasColumnName("ProdSaleCode");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.RemarkDate).HasColumnName("RemarkDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.OrderItemId).HasColumnName("OrderItemId");

            this.Property(t => t.SectionCode).HasColumnName("SectionCode");
        }
    }
}
