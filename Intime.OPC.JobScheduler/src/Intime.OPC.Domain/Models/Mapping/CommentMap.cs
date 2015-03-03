using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CommentMapper : EntityTypeConfiguration<Comment>
    {
        public CommentMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.ReplyUser).HasColumnName("ReplyUser");
            this.Property(t => t.ReplyId).HasColumnName("ReplyId");
        }
    }
}
