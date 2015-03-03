using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ResourceStageMapper : EntityTypeConfiguration<ResourceStage>
    {
        public ResourceStageMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.ExtName)
                .HasMaxLength(16);

            this.Property(t => t.Size)
                .HasMaxLength(64);

            this.Property(t => t.ItemCode)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("ResourceStage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ExtName).HasColumnName("ExtName");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.ContentSize).HasColumnName("ContentSize");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.InUser).HasColumnName("InUser");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.InDate).HasColumnName("InDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UploadGroupId).HasColumnName("UploadGroupId");
            this.Property(t => t.IsDimension).HasColumnName("IsDimension");
        }
    }
}
