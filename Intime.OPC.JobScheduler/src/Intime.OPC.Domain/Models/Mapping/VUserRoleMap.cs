using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class VUserRoleMapper : EntityTypeConfiguration<VUserRole>
    {
        public VUserRoleMapper()
        {
            // Primary Key
            this.HasKey(t => new { t.Role_Id, t.User_Id, t.Role_Name, t.Role_Description, t.Role_Val });

            // Properties
            this.Property(t => t.Role_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.User_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Role_Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Role_Description)
                .IsRequired();

            this.Property(t => t.Role_Val)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VUserRole");
            this.Property(t => t.Role_Id).HasColumnName("Role_Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Role_Name).HasColumnName("Role_Name");
            this.Property(t => t.Role_Description).HasColumnName("Role_Description");
            this.Property(t => t.Role_Val).HasColumnName("Role_Val");
        }
    }
}
