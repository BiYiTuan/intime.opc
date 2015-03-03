using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class InventoryBackupMapper : EntityTypeConfiguration<InventoryBackup>
    {
        public InventoryBackupMapper()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.ProductId, t.PColorId, t.PSizeId, t.Amount, t.UpdateDate, t.UpdateUser, t.ChannelInventoryId });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PColorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PSizeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Amount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UpdateUser)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChannelInventoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InventoryBackup");
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
