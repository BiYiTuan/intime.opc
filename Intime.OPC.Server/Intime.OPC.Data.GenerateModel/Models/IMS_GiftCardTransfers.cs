using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class IMS_GiftCardTransfers
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int FromUserId { get; set; }
        public Nullable<int> ToUserId { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public int IsActive { get; set; }
        public int IsDecline { get; set; }
        public int PreTransferId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime OperateDate { get; set; }
        public int OperateUser { get; set; }
        public string FromNickName { get; set; }
        public string FromPhone { get; set; }
    }
}
