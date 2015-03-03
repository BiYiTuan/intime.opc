using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class WXReplyMapper : EntityTypeConfiguration<WXReply>
    {
        public WXReplyMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MatchKey)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ReplyMsg)
                .IsRequired()
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("WXReply");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MatchKey).HasColumnName("MatchKey");
            this.Property(t => t.ReplyMsg).HasColumnName("ReplyMsg");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
