using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_AuthUserMapper : EntityTypeConfiguration<OPC_AuthUser>
    {
        public OPC_AuthUserMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.LogonName)
                .HasMaxLength(40);

            this.Property(t => t.Password)
                .HasMaxLength(40);

            this.Property(t => t.Phone)
                .HasMaxLength(40);

            this.Property(t => t.OrgId)
                .HasMaxLength(50);

            this.Property(t => t.SectionName)
                .HasMaxLength(50);

            this.Property(t => t.OrgName)
                .HasMaxLength(50);

            this.Property(t => t.DataAuthId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_AuthUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.LogonName).HasColumnName("LogonName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUserId).HasColumnName("CreateUserId");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUserId).HasColumnName("UpdateUserId");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.OrgName).HasColumnName("OrgName");
            this.Property(t => t.DataAuthId).HasColumnName("DataAuthId");
            this.Property(t => t.IsSystem).HasColumnName("IsSystem");
        }
    }
}
