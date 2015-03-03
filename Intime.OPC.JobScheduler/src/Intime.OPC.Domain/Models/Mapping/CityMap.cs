using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CityMapper : EntityTypeConfiguration<City>
    {
        public CityMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ZipCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("City");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsProvince).HasColumnName("IsProvince");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.IsCity).HasColumnName("IsCity");
        }
    }
}
