using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_SupplierInfoMapper : EntityTypeConfiguration<OPC_SupplierInfo>
    {
        public OPC_SupplierInfoMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SupplierNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SupplierName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Corporate)
                .HasMaxLength(50);

            this.Property(t => t.Contact)
                .HasMaxLength(20);

            this.Property(t => t.Contract)
                .HasMaxLength(20);

            this.Property(t => t.Telephone)
                .HasMaxLength(20);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.FaxNo)
                .HasMaxLength(20);

            this.Property(t => t.TaxNo)
                .HasMaxLength(20);

            this.Property(t => t.Memo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OPC_SupplierInfo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SupplierNo).HasColumnName("SupplierNo");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.Corporate).HasColumnName("Corporate");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.Contract).HasColumnName("Contract");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.TaxNo).HasColumnName("TaxNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Memo).HasColumnName("Memo");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
