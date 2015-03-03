using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class BannerMapper : EntityTypeConfiguration<Banner>
    {
        public BannerMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Banner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
