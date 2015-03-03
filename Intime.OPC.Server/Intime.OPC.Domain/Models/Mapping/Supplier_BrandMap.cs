using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public partial class Supplier_BrandMap : EntityTypeConfiguration<Supplier_Brand>
    {
        public Supplier_BrandMap()
        {

            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Supplier_Id).IsRequired();

            this.Property(t => t.Brand_Id)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Supplier_Brand");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Supplier_Id).HasColumnName("Supplier_Id");
            this.Property(t => t.Brand_Id).HasColumnName("Brand_Id");
            //this.Property(t => t.Id).HasColumnName("Id");

            Init();

        }

        partial void Init();
    }
}