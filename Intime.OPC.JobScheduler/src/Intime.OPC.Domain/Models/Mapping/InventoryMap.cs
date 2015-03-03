using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class InventoryMapper : EntityTypeConfiguration<Inventory>
    {
        public InventoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Inventory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.PColorId).HasColumnName("PColorId");
            this.Property(t => t.PSizeId).HasColumnName("PSizeId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.ChannelInventoryId).HasColumnName("ChannelInventoryId");
        }
    }
}
