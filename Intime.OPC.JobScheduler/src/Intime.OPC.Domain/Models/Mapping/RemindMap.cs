using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class RemindMapper : EntityTypeConfiguration<Remind>
    {
        public RemindMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Remind");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.RemindUser).HasColumnName("RemindUser");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            this.Property(t => t.IsRemind).HasColumnName("IsRemind");
            this.Property(t => t.Stauts).HasColumnName("Stauts");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
        }
    }
}
