using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_AssociateIncomeRuleFlattenMapper : EntityTypeConfiguration<IMS_AssociateIncomeRuleFlatten>
    {
        public IMS_AssociateIncomeRuleFlattenMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("IMS_AssociateIncomeRuleFlatten");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RuleId).HasColumnName("RuleId");
            this.Property(t => t.Percentage).HasColumnName("Percentage");
        }
    }
}
