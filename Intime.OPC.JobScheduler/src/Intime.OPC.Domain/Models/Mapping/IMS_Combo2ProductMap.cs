using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_Combo2ProductMapper : EntityTypeConfiguration<IMS_Combo2Product>
    {
        public IMS_Combo2ProductMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_Combo2Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ComboId).HasColumnName("ComboId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
        }
    }
}
