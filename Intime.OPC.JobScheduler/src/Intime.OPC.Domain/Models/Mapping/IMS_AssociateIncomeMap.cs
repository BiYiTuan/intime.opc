using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeMapper : EntityTypeConfiguration<IMS_AssociateIncome>
    {
        public IMS_AssociateIncomeMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncome");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.AvailableAmount).HasColumnName("AvailableAmount");
            this.Property(t => t.RequestAmount).HasColumnName("RequestAmount");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
