using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class PointHistoryMapper : EntityTypeConfiguration<PointHistory>
    {
        public PointHistoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("PointHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PointSourceId).HasColumnName("PointSourceId");
            this.Property(t => t.PointSourceType).HasColumnName("PointSourceType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
