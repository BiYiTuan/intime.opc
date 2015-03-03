using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ShippingAddressMapper : EntityTypeConfiguration<ShippingAddress>
    {
        public ShippingAddressMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShippingZipCode)
                .HasMaxLength(20);

            this.Property(t => t.ShippingAddress1)
                .HasMaxLength(500);

            this.Property(t => t.ShippingContactPerson)
                .HasMaxLength(10);

            this.Property(t => t.ShippingContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.ShippingProvince)
                .HasMaxLength(50);

            this.Property(t => t.ShippingCity)
                .HasMaxLength(50);

            this.Property(t => t.ShippingDistrictName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShippingAddress");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ShippingZipCode).HasColumnName("ShippingZipCode");
            this.Property(t => t.ShippingAddress1).HasColumnName("ShippingAddress");
            this.Property(t => t.ShippingContactPerson).HasColumnName("ShippingContactPerson");
            this.Property(t => t.ShippingContactPhone).HasColumnName("ShippingContactPhone");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ShippingProvinceId).HasColumnName("ShippingProvinceId");
            this.Property(t => t.ShippingCityId).HasColumnName("ShippingCityId");
            this.Property(t => t.ShippingProvince).HasColumnName("ShippingProvince");
            this.Property(t => t.ShippingCity).HasColumnName("ShippingCity");
            this.Property(t => t.ShippingDistrictId).HasColumnName("ShippingDistrictId");
            this.Property(t => t.ShippingDistrictName).HasColumnName("ShippingDistrictName");
        }
    }
}
