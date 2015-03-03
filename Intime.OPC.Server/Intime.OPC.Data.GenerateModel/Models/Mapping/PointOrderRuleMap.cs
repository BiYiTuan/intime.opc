using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class PointOrderRuleMap : EntityTypeConfiguration<PointOrderRule>
    {
        public PointOrderRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PointOrderRule");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StorePromotionId).HasColumnName("StorePromotionId");
            this.Property(t => t.RangeFrom).HasColumnName("RangeFrom");
            this.Property(t => t.RangeTo).HasColumnName("RangeTo");
            this.Property(t => t.Ratio).HasColumnName("Ratio");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}
