using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RMA2ExMapper : EntityTypeConfiguration<RMA2Ex>
    {
        public RMA2ExMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RMANo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ExRMA)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("RMA2Ex");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.ExRMA).HasColumnName("ExRMA");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
