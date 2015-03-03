using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeRuleFixMapper : EntityTypeConfiguration<IMS_AssociateIncomeRuleFix>
    {
        public IMS_AssociateIncomeRuleFixMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeRuleFix");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RuleId).HasColumnName("RuleId");
            this.Property(t => t.FixAmount).HasColumnName("FixAmount");
        }
    }
}
