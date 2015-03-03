using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class PaymentMethodMap : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Prefix)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("PaymentMethod");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.IsCOD).HasColumnName("IsCOD");
            this.Property(t => t.AvailChannels).HasColumnName("AvailChannels");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.AvoidInvoice).HasColumnName("AvoidInvoice");
        }
    }
}
