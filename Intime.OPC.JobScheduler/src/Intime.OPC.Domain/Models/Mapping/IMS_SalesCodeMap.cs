using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_SalesCodeMapper : EntityTypeConfiguration<IMS_SalesCode>
    {
        public IMS_SalesCodeMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IMS_SalesCode");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
