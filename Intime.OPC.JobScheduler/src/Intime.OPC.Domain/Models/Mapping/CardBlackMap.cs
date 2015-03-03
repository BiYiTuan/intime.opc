using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class CardBlackMapper : EntityTypeConfiguration<CardBlack>
    {
        public CardBlackMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CardNo)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CardBlack");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CardNo).HasColumnName("CardNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
