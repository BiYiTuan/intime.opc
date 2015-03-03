using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class IMS_TagMap : EntityTypeConfiguration<IMS_Tag>
    {
        public IMS_TagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IMS_Tag");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Visible4Display).HasColumnName("Visible4Display");
        }
    }
}
