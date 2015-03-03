using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class BrandMapper : EntityTypeConfiguration<Brand>
    {
        public BrandMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.EnglishName)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.Logo)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.WebSite)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.Group)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Brand");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EnglishName).HasColumnName("EnglishName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Group).HasColumnName("Group");
            this.Property(t => t.ChannelBrandId).HasColumnName("ChannelBrandId");
        }
    }
}
