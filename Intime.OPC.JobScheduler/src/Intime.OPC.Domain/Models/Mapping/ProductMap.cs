using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.RecommendedReason)
                .IsRequired();

            this.Property(t => t.Favorable)
                .IsRequired();

            this.Property(t => t.SkuCode)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(100);

            this.Property(t => t.MoreDesc)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Brand_Id).HasColumnName("Brand_Id");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.RecommendedReason).HasColumnName("RecommendedReason");
            this.Property(t => t.Favorable).HasColumnName("Favorable");
            this.Property(t => t.RecommendUser).HasColumnName("RecommendUser");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Store_Id).HasColumnName("Store_Id");
            this.Property(t => t.Tag_Id).HasColumnName("Tag_Id");
            this.Property(t => t.FavoriteCount).HasColumnName("FavoriteCount");
            this.Property(t => t.ShareCount).HasColumnName("ShareCount");
            this.Property(t => t.InvolvedCount).HasColumnName("InvolvedCount");
            this.Property(t => t.RecommendSourceId).HasColumnName("RecommendSourceId");
            this.Property(t => t.RecommendSourceType).HasColumnName("RecommendSourceType");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.IsHasImage).HasColumnName("IsHasImage");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Is4Sale).HasColumnName("Is4Sale");
            this.Property(t => t.SkuCode).HasColumnName("SkuCode");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.MoreDesc).HasColumnName("MoreDesc");
            this.Property(t => t.ProductType).HasColumnName("ProductType");
        }
    }
}
