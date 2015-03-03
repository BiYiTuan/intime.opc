using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ResourcesMap : EntityTypeConfiguration<Resources>
    {
        public ResourcesMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SourceId).IsRequired();
            this.Property(t => t.SourceType).IsRequired();
            this.Property(t => t.Name).IsRequired().HasMaxLength(1024);
            this.Property(t => t.Domain).IsRequired().HasMaxLength(1024);
            this.Property(t => t.CreatedUser).IsRequired();
            this.Property(t => t.CreatedDate).IsRequired();
            this.Property(t => t.IsDefault).IsRequired();
            this.Property(t => t.SortOrder).IsRequired();
            this.Property(t => t.Type).IsRequired();
            this.Property(t => t.Status).IsRequired();
            this.Property(t => t.Size).IsRequired().HasMaxLength(64);
            this.Property(t => t.Width).IsRequired();
            this.Property(t => t.Height).IsRequired();
            this.Property(t => t.ContentSize).IsRequired();
            this.Property(t => t.ExtName).IsRequired().HasMaxLength(16);
            this.Property(t => t.ValueId).HasMaxLength(10);

            this.ToTable("Resources");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Domain).HasColumnName("Domain");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.ContentSize).HasColumnName("ContentSize");
            this.Property(t => t.ExtName).HasColumnName("ExtName");
            this.Property(t => t.IsDimension).HasColumnName("IsDimension");
            this.Property(t => t.IsExternal).HasColumnName("IsExternal");
            this.Property(t => t.ColorId).HasColumnName("ColorId");
            this.Property(t => t.ValueId).HasColumnName("ValueId");
            this.Property(t => t.ChannelPicId).HasColumnName("ChannelPicId");
        }
    }
}
