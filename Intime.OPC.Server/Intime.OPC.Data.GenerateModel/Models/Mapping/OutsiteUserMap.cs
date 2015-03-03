using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class OutsiteUserMap : EntityTypeConfiguration<OutsiteUser>
    {
        public OutsiteUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OutsiteUserId)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("OutsiteUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AssociateUserId).HasColumnName("AssociateUserId");
            this.Property(t => t.OutsiteUserId).HasColumnName("OutsiteUserId");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastLoginDate).HasColumnName("LastLoginDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.OutsiteType).HasColumnName("OutsiteType");
        }
    }
}
