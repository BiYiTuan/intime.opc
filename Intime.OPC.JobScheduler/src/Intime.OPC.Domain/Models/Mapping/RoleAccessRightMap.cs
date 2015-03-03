using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RoleAccessRightMapper : EntityTypeConfiguration<RoleAccessRight>
    {
        public RoleAccessRightMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RoleAccessRight");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.AccessRightId).HasColumnName("AccessRightId");

            // Relationships
            this.HasRequired(t => t.AdminAccessRight)
                .WithMany(t => t.RoleAccessRights)
                .HasForeignKey(d => d.AccessRightId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RoleAccessRights)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
