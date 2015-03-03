using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductStageMapper : EntityTypeConfiguration<ProductStage>
    {
        public ProductStageMapper()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .HasMaxLength(64);

            this.Property(t => t.BrandName)
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.DescripOfPromotion)
                .HasMaxLength(200);

            this.Property(t => t.Tag)
                .HasMaxLength(64);

            this.Property(t => t.Store)
                .HasMaxLength(64);

            this.Property(t => t.Promotions)
                .HasMaxLength(200);

            this.Property(t => t.ItemCode)
                .HasMaxLength(100);

            this.Property(t => t.Subjects)
                .HasMaxLength(200);

            this.Property(t => t.UPCCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductStage");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DescripOfPromotion).HasColumnName("DescripOfPromotion");
            this.Property(t => t.DescripOfProBeginDate).HasColumnName("DescripOfProBeginDate");
            this.Property(t => t.DescripOfProEndDate).HasColumnName("DescripOfProEndDate");
            this.Property(t => t.InUserId).HasColumnName("InUserId");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Store).HasColumnName("Store");
            this.Property(t => t.Promotions).HasColumnName("Promotions");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Subjects).HasColumnName("Subjects");
            this.Property(t => t.UploadGroupId).HasColumnName("UploadGroupId");
            this.Property(t => t.InDate).HasColumnName("InDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Is4Sale).HasColumnName("Is4Sale");
            this.Property(t => t.UPCCode).HasColumnName("UPCCode");
        }
    }
}
