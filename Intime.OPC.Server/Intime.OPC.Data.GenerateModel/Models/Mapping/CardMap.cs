using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class CardMap : EntityTypeConfiguration<Card>
    {
        public CardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CardNo)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.CardProfile)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Card");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CardNo).HasColumnName("CardNo");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.CardProfile).HasColumnName("CardProfile");
            this.Property(t => t.IsLocked).HasColumnName("IsLocked");
        }
    }
}
