using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class IMS_ComboMap : EntityTypeConfiguration<IMS_Combo>
    {
        public IMS_ComboMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Desc)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Private2Name)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("IMS_Combo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Private2Name).HasColumnName("Private2Name");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.OnlineDate).HasColumnName("OnlineDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.ProductType).HasColumnName("ProductType");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            this.Property(t => t.IsInPromotion).HasColumnName("IsInPromotion");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
        }
    }
}
