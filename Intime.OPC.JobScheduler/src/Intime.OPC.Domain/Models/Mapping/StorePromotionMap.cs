using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class StorePromotionMapper : EntityTypeConfiguration<StorePromotion>
    {
        public StorePromotionMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Notice)
                .HasMaxLength(1000);

            this.Property(t => t.UsageNotice)
                .HasMaxLength(500);

            this.Property(t => t.InScopeNotice)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("StorePromotion");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ActiveStartDate).HasColumnName("ActiveStartDate");
            this.Property(t => t.ActiveEndDate).HasColumnName("ActiveEndDate");
            this.Property(t => t.PromotionType).HasColumnName("PromotionType");
            this.Property(t => t.AcceptPointType).HasColumnName("AcceptPointType");
            this.Property(t => t.Notice).HasColumnName("Notice");
            this.Property(t => t.CouponStartDate).HasColumnName("CouponStartDate");
            this.Property(t => t.CouponEndDate).HasColumnName("CouponEndDate");
            this.Property(t => t.MinPoints).HasColumnName("MinPoints");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UsageNotice).HasColumnName("UsageNotice");
            this.Property(t => t.InScopeNotice).HasColumnName("InScopeNotice");
            this.Property(t => t.UnitPerPoints).HasColumnName("UnitPerPoints");
        }
    }
}
