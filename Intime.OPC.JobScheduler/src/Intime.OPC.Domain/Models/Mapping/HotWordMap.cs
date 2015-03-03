using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class HotWordMapper : EntityTypeConfiguration<HotWord>
    {
        public HotWordMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Word)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("HotWord");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Word).HasColumnName("Word");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
