using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class OPC_StockPropertyValueRawMap : EntityTypeConfiguration<OPC_StockPropertyValueRaw>
    {
        public OPC_StockPropertyValueRawMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SourceStockId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PropertyData)
                .IsRequired();

            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BrandSizeName)
                .HasMaxLength(64);

            this.Property(t => t.BrandSizeCode)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("OPC_StockPropertyValueRaw");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InventoryId).HasColumnName("InventoryId");
            this.Property(t => t.SourceStockId).HasColumnName("SourceStockId");
            this.Property(t => t.PropertyData).HasColumnName("PropertyData");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.BrandSizeName).HasColumnName("BrandSizeName");
            this.Property(t => t.BrandSizeCode).HasColumnName("BrandSizeCode");
        }
    }
}
