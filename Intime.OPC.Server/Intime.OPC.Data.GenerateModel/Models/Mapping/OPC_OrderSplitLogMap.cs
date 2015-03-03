using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class OPC_OrderSplitLogMap : EntityTypeConfiguration<OPC_OrderSplitLog>
    {
        public OPC_OrderSplitLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Reason)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OPC_OrderSplitLog");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
