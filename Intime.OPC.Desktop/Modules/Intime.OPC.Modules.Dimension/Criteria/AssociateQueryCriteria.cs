using System;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Criteria
{
    public class AssociateQueryCriteria : QueryCriteria
    {
        private int? _operatePermissions;
        private int _departmentId;

        public AssociateQueryCriteria()
        {
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
            DepartmentId = -1;
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
            set { SetProperty(ref _departmentId, value); }
        }

        [UriParameter("sectioncode")]
        public string SectionCode { get; set; }

        [UriParameter("operatorcode")]
        public string OperatorCode { get; set; }

        [UriParameter("operatorname")]
        public string OperatorName { get; set; }

        [UriParameter("operatepermissions")]
        public int? OperatePermissions
        {
            get
            {
                if (_operatePermissions != null && _operatePermissions.Value == (int)AssociatePermission.All)
                    _operatePermissions = null;

                return _operatePermissions;
            }
            set { _operatePermissions = value; }
        }
    }
}
