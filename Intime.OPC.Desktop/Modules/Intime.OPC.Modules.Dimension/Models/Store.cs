namespace Intime.OPC.Modules.Dimension.Models
{
    public class Store
    {
        public string Id { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        public string CreatedUser { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public string ExStoreId { get; set; }

        /// <summary>
        /// 退货地址
        /// </summary>
        public string RMAAddress { get; set; }

        /// <summary>
        /// //邮编
        /// </summary>
        public string RMAZipCode { get; set; }

        /// <summary>
        /// 退货联系人
        /// </summary>
        public string RMAPerson { get; set; }

        /// <summary>
        /// 退货电话
        /// </summary>
        public string RMAPhone { get; set; }
    }

    public class Store4Get
    {
        public string fieldName { get; set; }
        public string fieldValue { get; set; }
    }
}