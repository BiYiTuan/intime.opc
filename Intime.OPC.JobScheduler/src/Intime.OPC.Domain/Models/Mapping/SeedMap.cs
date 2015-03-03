using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class SeedMapper : EntityTypeConfiguration<Seed>
    {
        public SeedMapper()
        {
            // Primary Key
            this.HasKey(t => t.Name);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Seed");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
