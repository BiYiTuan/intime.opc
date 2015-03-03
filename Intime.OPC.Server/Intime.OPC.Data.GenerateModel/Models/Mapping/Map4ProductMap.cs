using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class Map4ProductMap : EntityTypeConfiguration<Map4Product>
    {
        public Map4ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Channel)
                .HasMaxLength(50);

            this.Property(t => t.ChannelProductId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Map4Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ChannelProductId).HasColumnName("ChannelProductId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsImageUpload).HasColumnName("IsImageUpload");
        }
    }
}
