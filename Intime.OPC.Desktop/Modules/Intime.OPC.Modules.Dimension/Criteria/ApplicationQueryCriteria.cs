using Intime.OPC.Domain.Attributes;
using Intime.OPC.Infrastructure.Service;
using System;

namespace Intime.OPC.Modules.Dimension.Criteria
{
    public class ApplicationQueryCriteria : QueryCriteria
    {
        private int _departmentId;

        public ApplicationQueryCriteria()
        {
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
            DepartmentId = -1;
            ApprovedStatus = -1;
            StoreId = -1;
        }

        [UriParameter("begindate")]
        public DateTime BeginDate { get; set; }

        [UriParameter("enddate")]
        public DateTime EndDate { get; set; }

        [UriParameter("mobileno")]
        public string MobileNo { get; set; }

        [UriParameter("storeid")]
        public int StoreId { get; set; }

        [UriParameter("departmentid")]
        public int DepartmentId
        {
            get
            {
                return _departmentId;
            }
            set
            {
                SetProperty(ref _departmentId, value);
            }
        }

        [UriParameter("sectioncode")]
        public string SectionCode { get; set; }

        [UriParameter("approvedstatus")]
        public int ApprovedStatus { get; set; }

        [UriParameter("operatorcode")]
        public string OperatorCode { get; set; }

        [UriParameter("operatorname")]
        public string OperatorName { get; set; }
    }
}
