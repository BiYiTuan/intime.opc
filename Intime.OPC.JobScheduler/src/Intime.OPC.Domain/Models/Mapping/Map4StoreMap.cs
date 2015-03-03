using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class Map4StoreMapper : EntityTypeConfiguration<Map4Store>
    {
        public Map4StoreMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ChannelStoreId)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(20);


            this.Property(t => t.Province)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(10);


            // Table & Column Mappings
            this.ToTable("Map4Store");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.ChannelStoreId).HasColumnName("ChannelStoreId");
        }
    }
}
