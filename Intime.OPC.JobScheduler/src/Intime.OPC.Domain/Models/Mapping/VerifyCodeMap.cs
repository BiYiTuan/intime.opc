using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class VerifyCodeMapper : EntityTypeConfiguration<VerifyCode>
    {
        public VerifyCodeMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.VerifySource)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("VerifyCode");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.TryCount).HasColumnName("TryCount");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.VerifyMode).HasColumnName("VerifyMode");
            this.Property(t => t.VerifySource).HasColumnName("VerifySource");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
