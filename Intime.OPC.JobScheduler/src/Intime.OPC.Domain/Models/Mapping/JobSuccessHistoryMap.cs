using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Domain.Models.Mapping
{
    public class JobSuccessHistoryMapper : EntityTypeConfiguration<JobSuccessHistory>
    {
        public JobSuccessHistoryMapper()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("JobSuccessHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.JobId).HasColumnName("JobId");
            this.Property(t => t.JobType).HasColumnName("JobType");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
