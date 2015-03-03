using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class FavoriteMapper : EntityTypeConfiguration<Favorite>
    {
        public FavoriteMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Favorite");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FavoriteSourceId).HasColumnName("FavoriteSourceId");
            this.Property(t => t.FavoriteSourceType).HasColumnName("FavoriteSourceType");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Store_Id).HasColumnName("Store_Id");
        }
    }
}
