using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class StoreRealMapper : EntityTypeConfiguration<StoreReal>
    {
        public StoreRealMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StoreNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("StoreReal");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StoreNo).HasColumnName("StoreNo");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}
