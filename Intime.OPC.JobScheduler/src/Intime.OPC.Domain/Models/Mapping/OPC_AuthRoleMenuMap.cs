using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_AuthRoleMenuMapper : EntityTypeConfiguration<OPC_AuthRoleMenu>
    {
        public OPC_AuthRoleMenuMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("OPC_AuthRoleMenu");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OPC_AuthMenuId).HasColumnName("OPC_AuthMenuId");
            this.Property(t => t.OPC_AuthRoleId).HasColumnName("OPC_AuthRoleId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUserId).HasColumnName("CreateUserId");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUserId).HasColumnName("UpdateUserId");
        }
    }
}
