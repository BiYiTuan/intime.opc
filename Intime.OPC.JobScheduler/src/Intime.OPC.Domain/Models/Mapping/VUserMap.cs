using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class VUserMapper : EntityTypeConfiguration<VUser>
    {
        public VUserMapper()
        {
            // Primary Key
            this.HasKey(t => new { t.User_Id, t.Name, t.Password, t.Nickname, t.CreatedUser, t.CreatedDate, t.UpdatedUser, t.UpdatedDate, t.LastLoginDate, t.Mobile, t.EMail, t.Status, t.UserLevel, t.Store_Id, t.Region_Id, t.Logo, t.Description, t.Gender, t.Role_Id, t.Role_Name, t.Role_Description, t.Role_Val });

            // Properties
            this.Property(t => t.User_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.Nickname)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.CreatedUser)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UpdatedUser)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.EMail)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserLevel)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Store_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Region_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Logo)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Role_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Role_Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Role_Description)
                .IsRequired();

            this.Property(t => t.Role_Val)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VUser");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Nickname).HasColumnName("Nickname");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.LastLoginDate).HasColumnName("LastLoginDate");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.EMail).HasColumnName("EMail");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserLevel).HasColumnName("UserLevel");
            this.Property(t => t.Store_Id).HasColumnName("Store_Id");
            this.Property(t => t.Region_Id).HasColumnName("Region_Id");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Role_Id).HasColumnName("Role_Id");
            this.Property(t => t.Role_Name).HasColumnName("Role_Name");
            this.Property(t => t.Role_Description).HasColumnName("Role_Description");
            this.Property(t => t.Role_Val).HasColumnName("Role_Val");
        }
    }
}
