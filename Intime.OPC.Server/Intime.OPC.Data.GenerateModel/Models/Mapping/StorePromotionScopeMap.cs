using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class StorePromotionScopeMap : EntityTypeConfiguration<StorePromotionScope>
    {
        public StorePromotionScopeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StoreName)
                .HasMaxLength(50);

            this.Property(t => t.Excludes)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("StorePromotionScope");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StorePromotionId).HasColumnName("StorePromotionId");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.StoreName).HasColumnName("StoreName");
            this.Property(t => t.Excludes).HasColumnName("Excludes");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}
