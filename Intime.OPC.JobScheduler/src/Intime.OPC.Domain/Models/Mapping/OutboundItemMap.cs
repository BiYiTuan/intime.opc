using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OutboundItemMapper : EntityTypeConfiguration<OutboundItem>
    {
        public OutboundItemMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OutboundNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductDesc)
                .HasMaxLength(200);

            this.Property(t => t.StoreItemNo)
                .HasMaxLength(50);

            this.Property(t => t.StoreItemDesc)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OutboundItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OutboundNo).HasColumnName("OutboundNo");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductDesc).HasColumnName("ProductDesc");
            this.Property(t => t.StoreItemNo).HasColumnName("StoreItemNo");
            this.Property(t => t.StoreItemDesc).HasColumnName("StoreItemDesc");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ItemPrice).HasColumnName("ItemPrice");
            this.Property(t => t.ExtendPrice).HasColumnName("ExtendPrice");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.SizeId).HasColumnName("SizeId");
            this.Property(t => t.ColorId).HasColumnName("ColorId");
        }
    }
}
