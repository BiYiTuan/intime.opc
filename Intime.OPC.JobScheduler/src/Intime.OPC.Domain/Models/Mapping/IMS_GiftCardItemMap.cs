using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_GiftCardItemMapper : EntityTypeConfiguration<IMS_GiftCardItem>
    {
        public IMS_GiftCardItemMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_GiftCardItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GiftCardId).HasColumnName("GiftCardId");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.MaxLimit).HasColumnName("MaxLimit");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
