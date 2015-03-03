using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeHistoryMapper : EntityTypeConfiguration<IMS_AssociateIncomeHistory>
    {
        public IMS_AssociateIncomeHistoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SourceNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.SourceNo).HasColumnName("SourceNo");
            this.Property(t => t.AssociateUserId).HasColumnName("AssociateUserId");
            this.Property(t => t.AssociateIncome).HasColumnName("AssociateIncome");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
