using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeTransferMapper : EntityTypeConfiguration<IMS_AssociateIncomeTransfer>
    {
        public IMS_AssociateIncomeTransferMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TransferRetCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.TransferRetMsg)
                .HasMaxLength(200);

            this.Property(t => t.QueryRetCode)
                .HasMaxLength(10);

            this.Property(t => t.QueryRetMsg)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeTransfer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.TotalCount).HasColumnName("TotalCount");
            this.Property(t => t.TotalFee).HasColumnName("TotalFee");
            this.Property(t => t.IsSuccess).HasColumnName("IsSuccess");
            this.Property(t => t.TransferRetCode).HasColumnName("TransferRetCode");
            this.Property(t => t.TransferRetMsg).HasColumnName("TransferRetMsg");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.QueryRetCode).HasColumnName("QueryRetCode");
            this.Property(t => t.QueryRetMsg).HasColumnName("QueryRetMsg");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
        }
    }
}
