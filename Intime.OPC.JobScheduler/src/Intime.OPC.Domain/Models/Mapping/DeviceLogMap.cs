using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class DeviceLogMapper : EntityTypeConfiguration<DeviceLog>
    {
        public DeviceLogMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DeviceToken)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.DeviceUid)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("DeviceLogs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.DeviceToken).HasColumnName("DeviceToken");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.DeviceUid).HasColumnName("DeviceUid");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
