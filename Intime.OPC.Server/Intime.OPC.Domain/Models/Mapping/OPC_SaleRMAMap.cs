using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SaleRMAMap : EntityTypeConfiguration<OPC_SaleRMA>
    {
        public OPC_SaleRMAMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RMAMemo)
                .HasMaxLength(150);

            this.Property(t => t.OrderNo)
                .HasMaxLength(50);

         

            this.Property(t => t.SaleRMASource)
                .IsRequired()
                .HasMaxLength(20);
            

            this.Property(t => t.RMANo)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("OPC_SaleRMA");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SaleOrderNo).HasColumnName("SaleOrderNo");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.BackDate).HasColumnName("BackDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.StoreFee).HasColumnName("StoreFee");
            this.Property(t => t.CustomFee).HasColumnName("CustomFee");
            this.Property(t => t.RMAMemo).HasColumnName("RMAMemo");
            this.Property(t => t.CompensationFee).HasColumnName("CompensationFee");
            this.Property(t => t.RMACount).HasColumnName("RMACount");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.SaleRMASource).HasColumnName("SaleRMASource");
            this.Property(t => t.RMAStatus).HasColumnName("RMAStatus");
            this.Property(t => t.RMACashStatus).HasColumnName("RMACashStatus");
            this.Property(t => t.RMANo).HasColumnName("RMANo");
            this.Property(t => t.RealRMASumMoney).HasColumnName("RealRMASumMoney");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.ServiceAgreeTime).HasColumnName("CustomerAuthDate");

            this.Property(t => t.RecoverableSumMoney).HasColumnName("RecoverableSumMoney");
        }
    }
}
