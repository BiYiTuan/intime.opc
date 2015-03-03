using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class Map4OrderMapper : EntityTypeConfiguration<Map4Order>
    {
        public Map4OrderMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ChannelOrderCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Map4Order");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ChannelOrderCode).HasColumnName("ChannelOrderCode");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.SyncStatus).HasColumnName("SyncStatus");
        }
    }
}
