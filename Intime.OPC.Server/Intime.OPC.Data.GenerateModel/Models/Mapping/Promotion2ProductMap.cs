using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class Promotion2ProductMap : EntityTypeConfiguration<Promotion2Product>
    {
        public Promotion2ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Promotion2Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProdId).HasColumnName("ProdId");
            this.Property(t => t.ProId).HasColumnName("ProId");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
