using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class Map4CategoryMap : EntityTypeConfiguration<Map4Category>
    {
        public Map4CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Channel)
                .HasMaxLength(50);

            this.Property(t => t.CategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Map4Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelId).HasColumnName("ChannelId");
            this.Property(t => t.Channel).HasColumnName("Channel");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.ChannelCategoryId).HasColumnName("ChannelCategoryId");
        }
    }
}
