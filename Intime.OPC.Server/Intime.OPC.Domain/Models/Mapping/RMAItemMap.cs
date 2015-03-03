using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RMAItemMap : EntityTypeConfiguration<RMAItem>
    {
        public RMAItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RMANo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductDesc)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StoreItem)
                .HasMaxLength(50);

            this.Property(t => t.StoreDesc)
                .HasMaxLength(200);

            this.Property(t => t.ColorValueName)
                .HasMaxLength(50);

            this.Property(t => t.SizeValueName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RMAItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductDesc).HasColumnName("ProductDesc");
            this.Property(t => t.StoreItem).HasColumnName("StoreItem");
            this.Property(t => t.StoreDesc).HasColumnName("StoreDesc");
            this.Property(t => t.ItemPrice).HasColumnName("ItemPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ExtendPrice).HasColumnName("ExtendPrice");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.SizeId).HasColumnName("SizeId");
            this.Property(t => t.ColorId).HasColumnName("ColorId");
            this.Property(t => t.SizeValueId).HasColumnName("SizeValueId");
            this.Property(t => t.ColorValueId).HasColumnName("ColorValueId");
            this.Property(t => t.ColorValueName).HasColumnName("ColorValueName");
            this.Property(t => t.SizeValueName).HasColumnName("SizeValueName");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
        }
    }
}
