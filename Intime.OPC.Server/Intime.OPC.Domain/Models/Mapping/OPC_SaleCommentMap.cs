using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SaleCommentMap : EntityTypeConfiguration<OPC_SaleComment>
    {
        public OPC_SaleCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OPC_SaleComment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SaleOrderNo).HasColumnName("SaleOrderNo");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}
