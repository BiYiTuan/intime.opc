using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class Map4InventoryMapper : EntityTypeConfiguration<Map4Inventory>
    {
        public Map4InventoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.attr)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.desc)
                .HasMaxLength(500);

            this.Property(t => t.stockId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.saleAttr)
                .HasMaxLength(500);

            this.Property(t => t.pic)
                .HasMaxLength(200);

            this.Property(t => t.specAttr)
                .HasMaxLength(500);

            this.Property(t => t.itemId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Map4Inventory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.attr).HasColumnName("attr");
            this.Property(t => t.desc).HasColumnName("desc");
            this.Property(t => t.stockId).HasColumnName("stockId");
            this.Property(t => t.sellerUin).HasColumnName("sellerUin");
            this.Property(t => t.soldNum).HasColumnName("soldNum");
            this.Property(t => t.skuId).HasColumnName("skuId");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.num).HasColumnName("num");
            this.Property(t => t.saleAttr).HasColumnName("saleAttr");
            this.Property(t => t.pic).HasColumnName("pic");
            this.Property(t => t.specAttr).HasColumnName("specAttr");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.InventoryId).HasColumnName("InventoryId");
            this.Property(t => t.price).HasColumnName("price");
            this.Property(t => t.itemId).HasColumnName("itemId");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
