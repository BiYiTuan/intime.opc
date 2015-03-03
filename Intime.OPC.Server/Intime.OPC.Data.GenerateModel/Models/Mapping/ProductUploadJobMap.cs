using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class ProductUploadJobMap : EntityTypeConfiguration<ProductUploadJob>
    {
        public ProductUploadJobMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProductUploadJob");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InDate).HasColumnName("InDate");
            this.Property(t => t.InUser).HasColumnName("InUser");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
