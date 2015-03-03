using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_ChannelMapMapper : EntityTypeConfiguration<OPC_ChannelMap>
    {
        public OPC_ChannelMapMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.InnerValue)
                .HasMaxLength(50);

            this.Property(t => t.ChannelValue)
                .HasMaxLength(50);

            this.Property(t => t.Channel)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_ChannelMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InnerValue).HasColumnName("InnerValue");
            this.Property(t => t.ChannelValue).HasColumnName("ChannelValue");
            this.Property(t => t.MapType).HasColumnName("MapType");
            this.Property(t => t.Channel).HasColumnName("Channel");
        }
    }
}
