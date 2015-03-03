using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class Supplier_BrandMap : EntityTypeConfiguration<Supplier_Brand>
    {
        public Supplier_BrandMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Supplier_Brand");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Supplier_Id).HasColumnName("Supplier_Id");
            this.Property(t => t.Brand_Id).HasColumnName("Brand_Id");
        }
    }
}
