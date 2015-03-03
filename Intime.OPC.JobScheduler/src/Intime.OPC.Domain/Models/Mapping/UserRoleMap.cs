using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class UserRoleMapper : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserRole");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Role_Id).HasColumnName("Role_Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
