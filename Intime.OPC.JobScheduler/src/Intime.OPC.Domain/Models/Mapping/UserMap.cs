using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.Nickname)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.EMail)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Logo)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id");
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
            this.Property(t => t.IsCardBinded).HasColumnName("IsCardBinded");
        }
    }
}
