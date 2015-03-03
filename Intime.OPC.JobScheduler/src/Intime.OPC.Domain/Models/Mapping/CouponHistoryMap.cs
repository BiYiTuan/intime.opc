using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CouponHistoryMapper : EntityTypeConfiguration<CouponHistory>
    {
        public CouponHistoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CouponId)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CouponHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CouponId).HasColumnName("CouponId");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.FromStore).HasColumnName("FromStore");
            this.Property(t => t.FromUser).HasColumnName("FromUser");
            this.Property(t => t.FromProduct).HasColumnName("FromProduct");
            this.Property(t => t.FromPromotion).HasColumnName("FromPromotion");
            this.Property(t => t.ValidStartDate).HasColumnName("ValidStartDate");
            this.Property(t => t.ValidEndDate).HasColumnName("ValidEndDate");
            this.Property(t => t.IsLimitOnce).HasColumnName("IsLimitOnce");
        }
    }
}
