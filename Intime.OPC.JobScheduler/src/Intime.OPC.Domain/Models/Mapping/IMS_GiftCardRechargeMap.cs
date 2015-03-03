using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_GiftCardRechargeMapper : EntityTypeConfiguration<IMS_GiftCardRecharge>
    {
        public IMS_GiftCardRechargeMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ChargePhone)
                .IsRequired()
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("IMS_GiftCardRecharge");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ChargeUserId).HasColumnName("ChargeUserId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ChargePhone).HasColumnName("ChargePhone");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
        }
    }
}
