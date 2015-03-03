using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class SectionMap : EntityTypeConfiguration<Section>
    {
        public SectionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Location)
                .HasMaxLength(200);

            this.Property(t => t.ContactPhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StoreCode)
                .HasMaxLength(50);

            this.Property(t => t.SectionCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Section");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.ChannelSectionId).HasColumnName("ChannelSectionId");
            this.Property(t => t.SectionCode).HasColumnName("SectionCode");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
        }
    }
}
