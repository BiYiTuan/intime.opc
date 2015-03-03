using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_ShippingSaleMapper : EntityTypeConfiguration<OPC_ShippingSale>
    {
        public OPC_ShippingSaleMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ShippingZipCode)
                .HasMaxLength(20);

            this.Property(t => t.ShippingAddress)
                .HasMaxLength(500);

            this.Property(t => t.ShippingContactPerson)
                .HasMaxLength(10);

            this.Property(t => t.ShippingContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.ShipViaName)
                .HasMaxLength(50);

            this.Property(t => t.ShippingCode)
                .HasMaxLength(50);

            this.Property(t => t.ShippingRemark)
                .HasMaxLength(500);

            this.Property(t => t.RMANo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_ShippingSale");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.ShippingZipCode).HasColumnName("ShippingZipCode");
            this.Property(t => t.ShippingAddress).HasColumnName("ShippingAddress");
            this.Property(t => t.ShippingContactPerson).HasColumnName("ShippingContactPerson");
            this.Property(t => t.ShippingContactPhone).HasColumnName("ShippingContactPhone");
            this.Property(t => t.ShipViaId).HasColumnName("ShipViaId");
            this.Property(t => t.ShipViaName).HasColumnName("ShipViaName");
            this.Property(t => t.ShippingCode).HasColumnName("ShippingCode");
            this.Property(t => t.ShippingFee).HasColumnName("ShippingFee");
            this.Property(t => t.ShippingStatus).HasColumnName("ShippingStatus");
            this.Property(t => t.ShippingRemark).HasColumnName("ShippingRemark");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.PrintTimes).HasColumnName("PrintTimes");
        }
    }
}
