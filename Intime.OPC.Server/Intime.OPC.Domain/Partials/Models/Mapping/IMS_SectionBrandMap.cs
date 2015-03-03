using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models.Mapping
{

    public partial class IMS_SectionBrandMap : EntityTypeConfiguration<IMS_SectionBrand>
    {
        public IMS_SectionBrandMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.BrandId).IsRequired();
            this.Property(t => t.SectionId).IsRequired();


            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
        }
    }
}
