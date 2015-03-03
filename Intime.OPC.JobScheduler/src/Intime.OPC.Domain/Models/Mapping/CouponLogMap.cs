using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CouponLogMapper : EntityTypeConfiguration<CouponLog>
    {
        public CouponLogMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ConsumeStoreNo)
                .HasMaxLength(20);

            this.Property(t => t.ReceiptNo)
                .HasMaxLength(50);

            this.Property(t => t.BrandNo)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CouponLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.ConsumeStoreNo).HasColumnName("ConsumeStoreNo");
            this.Property(t => t.ReceiptNo).HasColumnName("ReceiptNo");
            this.Property(t => t.BrandNo).HasColumnName("BrandNo");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
        }
    }
}
