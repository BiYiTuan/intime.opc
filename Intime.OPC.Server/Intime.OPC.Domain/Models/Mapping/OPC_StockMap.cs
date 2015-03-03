using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_StockMap : EntityTypeConfiguration<OPC_Stock>
    {
        public OPC_StockMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProdSaleCode)
                .HasMaxLength(50);
            this.Property(t => t.ProductName)
                .HasMaxLength(120);
            this.Property(t => t.SectionCode)
                .HasMaxLength(50);
            this.Property(t => t.ProductName)
                .HasMaxLength(50);
            this.Property(t => t.ProductCode)
               .HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("OPC_Stock");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SkuId).HasColumnName("SkuId");
            this.Property(t => t.SourceStockId).HasColumnName("SourceStockId");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.Count).HasColumnName("Count");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsDel).HasColumnName("IsDel");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.ProdSaleCode).HasColumnName("ProdSaleCode");

            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.SectionCode).HasColumnName("SectionCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
        }
    }
}
