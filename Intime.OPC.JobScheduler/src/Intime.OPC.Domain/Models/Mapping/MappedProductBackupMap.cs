using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class MappedProductBackupMapper : EntityTypeConfiguration<MappedProductBackup>
    {
        public MappedProductBackupMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Channel)
                .HasMaxLength(50);

            this.Property(t => t.ChannelProductId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MappedProductBackup");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ChannelProductId).HasColumnName("ChannelProductId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
        }
    }
}
