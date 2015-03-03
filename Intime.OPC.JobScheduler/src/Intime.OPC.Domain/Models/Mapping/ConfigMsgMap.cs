using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ConfigMsgMapper : EntityTypeConfiguration<ConfigMsg>
    {
        public ConfigMsgMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MKey)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("ConfigMsg");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MKey).HasColumnName("MKey");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
