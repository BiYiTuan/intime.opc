using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RMALogMapper : EntityTypeConfiguration<RMALog>
    {
        public RMALogMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RMANo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Operation)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("RMALog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.Operation).HasColumnName("Operation");
        }
    }
}
