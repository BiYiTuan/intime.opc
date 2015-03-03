using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class WX_MenuMapper : EntityTypeConfiguration<WX_Menu>
    {
        public WX_MenuMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Url)
                .HasMaxLength(100);

            this.Property(t => t.AppId)
                .HasMaxLength(50);

            this.Property(t => t.WKey)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WX_Menu");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AppId).HasColumnName("AppId");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.Pos).HasColumnName("Pos");
            this.Property(t => t.WKey).HasColumnName("WKey");
        }
    }
}
