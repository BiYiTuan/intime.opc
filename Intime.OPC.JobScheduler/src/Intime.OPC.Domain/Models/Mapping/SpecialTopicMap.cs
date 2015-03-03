using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class SpecialTopicMapper : EntityTypeConfiguration<SpecialTopic>
    {
        public SpecialTopicMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.TargetValue)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SpecialTopic");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TargetValue).HasColumnName("TargetValue");
        }
    }
}
