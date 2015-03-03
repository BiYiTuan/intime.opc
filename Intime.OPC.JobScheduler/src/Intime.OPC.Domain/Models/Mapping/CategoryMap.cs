using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CategoryMapper : EntityTypeConfiguration<Category>
    {
        public CategoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ExCatCode)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExCatId).HasColumnName("ExCatId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ExCatCode).HasColumnName("ExCatCode");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.TagId).HasColumnName("TagId");
        }
    }
}
