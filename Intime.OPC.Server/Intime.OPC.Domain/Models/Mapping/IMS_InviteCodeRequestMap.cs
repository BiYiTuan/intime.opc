using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class IMS_InviteCodeRequestMap : EntityTypeConfiguration<IMS_InviteCodeRequest>
    {
        public IMS_InviteCodeRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ContactMobile)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OperatorCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RejectReason)
                .HasMaxLength(50);

            this.Property(t => t.IdCard)
                .HasMaxLength(18);

            // Table & Column Mappings
            this.ToTable("IMS_InviteCodeRequest");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactMobile).HasColumnName("ContactMobile");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SectionCode).HasColumnName("SectionCode");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.OperatorCode).HasColumnName("OperatorCode");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.RequestType).HasColumnName("RequestType");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Approved).HasColumnName("Approved");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.RejectReason).HasColumnName("RejectReason");
            this.Property(t => t.IdCard).HasColumnName("IdCard");
            this.Property(t => t.ApprovedNotificationTimes).HasColumnName("ApprovedNotificationTimes");
            this.Property(t => t.DemotionNotificationTimes).HasColumnName("DemotionNotificationTimes");
        }
    }
}
