using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class IMS_AssociateIncomeRuleFlexMap : EntityTypeConfiguration<IMS_AssociateIncomeRuleFlex>
    {
        public IMS_AssociateIncomeRuleFlexMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeRuleFlex");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RuleId).HasColumnName("RuleId");
            this.Property(t => t.BenchFrom).HasColumnName("BenchFrom");
            this.Property(t => t.BenchTo).HasColumnName("BenchTo");
            this.Property(t => t.Percentage).HasColumnName("Percentage");
        }
    }
}
