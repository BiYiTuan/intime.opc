using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class ProductCode2StoreCodeMap : EntityTypeConfiguration<ProductCode2StoreCode>
    {
        public ProductCode2StoreCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StoreProductCode)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductCode2StoreCode");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.StoreProductCode).HasColumnName("StoreProductCode");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
        }
    }
}
