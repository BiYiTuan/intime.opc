using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class SectionBrandImportStageMapper : EntityTypeConfiguration<SectionBrandImportStage>
    {
        public SectionBrandImportStageMapper()
        {
            // Primary Key
            this.HasKey(t => new { t.StoreCode, t.SupplyCode, t.ContractCode, t.CompanyName, t.CompanyContactPerson, t.SectionName, t.SectionCode });

            // Properties
            this.Property(t => t.StoreCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Department)
                .HasMaxLength(50);

            this.Property(t => t.SupplyCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContractCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyContactPerson)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyContactPhone)
                .HasMaxLength(50);

            this.Property(t => t.SectionName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionBrandName)
                .HasMaxLength(50);

            this.Property(t => t.SectonBrandEName)
                .HasMaxLength(50);

            this.Property(t => t.SectionCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OperatorCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SectionBrandImportStage");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.Department).HasColumnName("Department");
            this.Property(t => t.SupplyCode).HasColumnName("SupplyCode");
            this.Property(t => t.ContractCode).HasColumnName("ContractCode");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyContactPerson).HasColumnName("CompanyContactPerson");
            this.Property(t => t.CompanyContactPhone).HasColumnName("CompanyContactPhone");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.SectionBrandName).HasColumnName("SectionBrandName");
            this.Property(t => t.SectonBrandEName).HasColumnName("SectonBrandEName");
            this.Property(t => t.SectionCode).HasColumnName("SectionCode");
            this.Property(t => t.OperatorCode).HasColumnName("OperatorCode");
        }
    }
}
