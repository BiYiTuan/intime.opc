using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class StoreCouponMapper : EntityTypeConfiguration<StoreCoupon>
    {
        public StoreCouponMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(20);

            this.Property(t => t.VipCard)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("StoreCoupons");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ValidStartDate).HasColumnName("ValidStartDate");
            this.Property(t => t.ValidEndDate).HasColumnName("ValidEndDate");
            this.Property(t => t.VipCard).HasColumnName("VipCard");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StorePromotionId).HasColumnName("StorePromotionId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
        }
    }
}
