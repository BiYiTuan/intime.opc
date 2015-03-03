using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SaleOrderNotificationLogMapper : EntityTypeConfiguration<OPC_SaleOrderNotificationLog>
    {
        public OPC_SaleOrderNotificationLogMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_SaleOrderNotificationLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SaleOrderNo).HasColumnName("SaleOrderNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
        }
    }
}
