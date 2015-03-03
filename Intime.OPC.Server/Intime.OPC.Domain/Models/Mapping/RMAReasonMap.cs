using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RMAReasonMap : EntityTypeConfiguration<RMAReason>
    {
        public RMAReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("RMAReason");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
