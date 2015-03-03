using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto
{
    //退货包裹管理 打应快递单
    public class RmaExpressRequest : BaseRequest
    {

        public string OrderNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void FormateDate()
        {
            StartDate = StartDate.Date;
            EndDate = EndDate.Date.AddDays(1);
        }

    }

    public class RmaExpressSaveDto
    {
        /*快递公司ID*/
        public int ShipViaID { get; set; }
        /*快递公司*/
        public string ShipViaName { get; set; }
        /*实际运费*/
        public double ShippingFee { get; set; }
        /*快递单号*/
        public string ShippingCode { get; set; }
        /*退货单*/
        public string RmaNo { get; set; }
    }

}