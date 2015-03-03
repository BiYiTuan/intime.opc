using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class LikeMapper : EntityTypeConfiguration<Like>
    {
        public LikeMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Like");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LikeUserId).HasColumnName("LikeUserId");
            this.Property(t => t.LikedUserId).HasColumnName("LikedUserId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
