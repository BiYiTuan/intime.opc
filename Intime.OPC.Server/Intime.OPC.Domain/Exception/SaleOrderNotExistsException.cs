using System;

namespace Intime.OPC.Domain.Exception
{
    public class SaleOrderNotExistsException : SalesOrderException
    {
        public SaleOrderNotExistsException(string saleOrderNo)
            : base(String.Format("销售单不存在,销售单编号：{0}", saleOrderNo), saleOrderNo)
        {
        }
    }

    public class SaleOrderNotFoundException : SalesOrderException
    {
        public SaleOrderNotFoundException(string msg)
            : base(msg)
        {
        }
    }

    public class SalesOrderException : OpcApiException
    {
        public SalesOrderException(string msg)
            : base(msg)
        {
        }

        public SalesOrderException(string msg, string salesorderNo)
            : base(msg)
        {
            SalesOrderNo = salesorderNo;
        }

        public string SalesOrderNo { get; private set; }
    }
}