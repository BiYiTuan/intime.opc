using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class Promotion2ProductMapper : EntityTypeConfiguration<Promotion2Product>
    {
        public Promotion2ProductMapper()
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
