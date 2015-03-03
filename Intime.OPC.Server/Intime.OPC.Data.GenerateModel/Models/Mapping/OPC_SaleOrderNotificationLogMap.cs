using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class OPC_SaleOrderNotificationLogMap : EntityTypeConfiguration<OPC_SaleOrderNotificationLog>
    {
        public OPC_SaleOrderNotificationLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Message)
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
