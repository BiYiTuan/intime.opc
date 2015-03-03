using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class StoreMap : EntityTypeConfiguration<Store>
    {
        public StoreMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(2048);

            this.Property(t => t.Tel)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.RMAAddress)
                .HasMaxLength(200);

            this.Property(t => t.RMAZipCode)
                .HasMaxLength(50);

            this.Property(t => t.RMAPerson)
                .HasMaxLength(10);

            this.Property(t => t.RMAPhone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Store");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Tel).HasColumnName("Tel");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Group_Id).HasColumnName("Group_Id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Region_Id).HasColumnName("Region_Id");
            this.Property(t => t.StoreLevel).HasColumnName("StoreLevel");
            this.Property(t => t.GpsLat).HasColumnName("GpsLat");
            this.Property(t => t.GpsLng).HasColumnName("GpsLng");
            this.Property(t => t.GpsAlt).HasColumnName("GpsAlt");
            this.Property(t => t.ExStoreId).HasColumnName("ExStoreId");
            this.Property(t => t.RMAAddress).HasColumnName("RMAAddress");
            this.Property(t => t.RMAZipCode).HasColumnName("RMAZipCode");
            this.Property(t => t.RMAPerson).HasColumnName("RMAPerson");
            this.Property(t => t.RMAPhone).HasColumnName("RMAPhone");

        }
    }
}
