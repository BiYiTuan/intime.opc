using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CategoryMapMapper : EntityTypeConfiguration<CategoryMap>
    {
        public CategoryMapMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShowChannel)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CategoryMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChannelCatId).HasColumnName("ChannelCatId");
            this.Property(t => t.CatId).HasColumnName("CatId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ShowChannel).HasColumnName("ShowChannel");
        }
    }
}
