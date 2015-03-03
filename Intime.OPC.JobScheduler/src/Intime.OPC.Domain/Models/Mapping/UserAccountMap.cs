using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class UserAccountMapper : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserAccount");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountType).HasColumnName("AccountType");
            this.Property(t => t.AccountId).HasColumnName("AccountId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
