using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class PromotionBrandRelationMap : EntityTypeConfiguration<PromotionBrandRelation>
    {
        public PromotionBrandRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PromotionBrandRelations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Promotion_Id).HasColumnName("Promotion_Id");
            this.Property(t => t.Brand_Id).HasColumnName("Brand_Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
