using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeRuleMapper : EntityTypeConfiguration<IMS_AssociateIncomeRule>
    {
        public IMS_AssociateIncomeRuleMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeRule");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.RuleType).HasColumnName("RuleType");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
