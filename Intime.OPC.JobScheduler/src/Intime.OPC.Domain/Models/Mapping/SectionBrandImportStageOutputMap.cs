using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class SectionBrandImportStageOutputMapper : EntityTypeConfiguration<SectionBrandImportStageOutput>
    {
        public SectionBrandImportStageOutputMapper()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.Name });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SectionBrandImportStageOutput");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
