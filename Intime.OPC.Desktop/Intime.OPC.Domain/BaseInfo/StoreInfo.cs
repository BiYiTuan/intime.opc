namespace Intime.OPC.Domain.BaseInfo
{
    public class StoreInfo
    {
        public string Id { get; set; }

        public string Name { get; set; } //门店名称

        public string Location { get; set; } //地址

        public string Tel { get; set; } //电话

        public string CreatedUser { get; set; }


        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public string Status { get; set; }

        public string ExStoreId { get; set; } //排序号

        public string RMAAddress { get; set; } //退货地址

        public string RMAZipCode { get; set; } //邮编

        public string RMAPerson { get; set; } //退货联系人

        public string RMAPhone { get; set; } //退货电话

        //public string 
    }

    public class Store4Get
    {
        public string fieldName { get; set; }
        public string fieldValue { get; set; }
    }
}