using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class DeviceTokenMapper : EntityTypeConfiguration<DeviceToken>
    {
        public DeviceTokenMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("DeviceToken");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
