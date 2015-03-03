using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Dto.Request
{
    public class SetAssociateOperateRequest:IStoreDataRoleRequest
    {
        public void ArrangeParams()
        {
        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        public int AssociateId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public UserOperatorRight OperateRight { get; set; }
    }
}
