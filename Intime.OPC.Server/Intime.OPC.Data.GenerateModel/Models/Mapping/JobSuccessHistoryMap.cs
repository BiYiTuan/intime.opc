using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Intime.OPC.Data.GenerateModel.Models.Mapping
{
    public class JobSuccessHistoryMap : EntityTypeConfiguration<JobSuccessHistory>
    {
        public JobSuccessHistoryMap()
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
