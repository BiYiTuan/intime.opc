using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OutboundMapper : EntityTypeConfiguration<Outbound>
    {
        public OutboundMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OutboundNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SourceNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ShippingAddress)
                .HasMaxLength(500);

            this.Property(t => t.ShippingContactPerson)
                .HasMaxLength(10);

            this.Property(t => t.ShippingContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.ShippingZipCode)
                .HasMaxLength(20);

            this.Property(t => t.ShippingNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Outbound");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OutboundNo).HasColumnName("OutboundNo");
            this.Property(t => t.SourceNo).HasColumnName("SourceNo");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.ShippingVia).HasColumnName("ShippingVia");
            this.Property(t => t.ShippingAddress).HasColumnName("ShippingAddress");
            this.Property(t => t.ShippingContactPerson).HasColumnName("ShippingContactPerson");
            this.Property(t => t.ShippingContactPhone).HasColumnName("ShippingContactPhone");
            this.Property(t => t.ShippingZipCode).HasColumnName("ShippingZipCode");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ShippingNo).HasColumnName("ShippingNo");
        }
    }
}
