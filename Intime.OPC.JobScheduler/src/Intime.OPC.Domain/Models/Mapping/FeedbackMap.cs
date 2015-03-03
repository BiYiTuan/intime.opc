using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class FeedbackMapper : EntityTypeConfiguration<Feedback>
    {
        public FeedbackMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.Contact)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Feedback");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
