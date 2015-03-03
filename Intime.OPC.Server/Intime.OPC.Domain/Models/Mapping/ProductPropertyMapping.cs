using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class ProductPropertyMapping : EntityTypeConfiguration<ProductProperty>
    {
        public ProductPropertyMapping()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProductId).IsRequired();
            this.Property(t => t.PropertyDesc).IsRequired().HasMaxLength(50);
            this.Property(t => t.Status).IsRequired();
            this.Property(t => t.UpdatedDate).IsRequired();
            this.Property(t => t.UpdatedUser).IsRequired();

            this.ToTable("ProductProperty");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.PropertyDesc).HasColumnName("PropertyDesc");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.IsColor).HasColumnName("IsColor");
            this.Property(t => t.IsSize).HasColumnName("IsSize");
            this.Property(t => t.ChannelPropertyId).HasColumnName("ChannelPropertyId");
        }
    }
}
