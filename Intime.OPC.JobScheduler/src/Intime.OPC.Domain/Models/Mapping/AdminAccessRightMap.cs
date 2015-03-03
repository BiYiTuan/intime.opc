using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class AdminAccessRightMapper : EntityTypeConfiguration<AdminAccessRight>
    {
        public AdminAccessRightMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.ControllName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ActionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AdminAccessRight");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ControllName).HasColumnName("ControllName");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.InDate).HasColumnName("InDate");
            this.Property(t => t.InUser).HasColumnName("InUser");
        }
    }
}
