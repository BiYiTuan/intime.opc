using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class IMS_SectionBrandMap : EntityTypeConfiguration<IMS_SectionBrand>
    {
        public IMS_SectionBrandMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_SectionBrand");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
        }
    }
}
