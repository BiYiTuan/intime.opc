using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_AuthRoleUserMapper : EntityTypeConfiguration<OPC_AuthRoleUser>
    {
        public OPC_AuthRoleUserMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("OPC_AuthRoleUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OPC_AuthUserId).HasColumnName("OPC_AuthUserId");
            this.Property(t => t.OPC_AuthRoleId).HasColumnName("OPC_AuthRoleId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUserId).HasColumnName("CreateUserId");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUserId).HasColumnName("UpdateUserId");
        }
    }
}
