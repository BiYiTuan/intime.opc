using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class OPC_CategoryMapMapper : EntityTypeConfiguration<OPC_CategoryMap>
    {
        public OPC_CategoryMapMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ChannelCategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Channel)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OPC_CategoryMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TagId).HasColumnName("TagId");
            this.Property(t => t.ChannelCategoryCode).HasColumnName("ChannelCategoryCode");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.Channel).HasColumnName("Channel");
        }
    }
}
