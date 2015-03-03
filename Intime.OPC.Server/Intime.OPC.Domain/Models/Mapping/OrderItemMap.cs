using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductDesc)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StoreItemNo)
                .HasMaxLength(20);

            this.Property(t => t.StoreItemDesc)
                .HasMaxLength(200);

            this.Property(t => t.ProductName)
                .HasMaxLength(200);

            this.Property(t => t.SalesPerson)
                .HasMaxLength(20);

            this.Property(t => t.ColorValueName)
                .HasMaxLength(50);

            this.Property(t => t.SizeValueName)
                .HasMaxLength(50);

            this.Property(t => t.StoreSalesCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductDesc).HasColumnName("ProductDesc");
            this.Property(t => t.StoreItemNo).HasColumnName("StoreItemNo");
            this.Property(t => t.StoreItemDesc).HasColumnName("StoreItemDesc");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.ItemPrice).HasColumnName("ItemPrice");
            this.Property(t => t.ExtendPrice).HasColumnName("ExtendPrice");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.SalesPerson).HasColumnName("SalesPerson");
            this.Property(t => t.SizeId).HasColumnName("SizeId");
            this.Property(t => t.ColorId).HasColumnName("ColorId");
            this.Property(t => t.SizeValueId).HasColumnName("SizeValueId");
            this.Property(t => t.ColorValueId).HasColumnName("ColorValueId");
            this.Property(t => t.ColorValueName).HasColumnName("ColorValueName");
            this.Property(t => t.SizeValueName).HasColumnName("SizeValueName");
            this.Property(t => t.ProductType).HasColumnName("ProductType");
            this.Property(t => t.StoreSalesCode).HasColumnName("StoreSalesCode");
        }
    }
}
