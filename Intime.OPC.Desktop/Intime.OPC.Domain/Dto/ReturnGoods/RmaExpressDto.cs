using System;

namespace Intime.OPC.Domain.ReturnGoods
{
    //�˻��������� ��Ӧ��ݵ�
    public class RmaExpressDto
    {
        public RmaExpressDto()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public string OrderNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return string.Format("StartDate={0}&EndDate={1}&OrderNo={2}&pageIndex={3}&pageSize={4}", StartDate, EndDate,
                OrderNo, 1, 300);
        }
    }

    public class RmaExpressSaveDto
    {
        /*��ݹ�˾ID*/
        public int ShipViaID { get; set; }
        /*��ݹ�˾*/
        public string ShipViaName { get; set; }
        /*ʵ���˷�*/
        public double ShippingFee { get; set; }
        /*��ݵ���*/
        public string ShippingCode { get; set; }
        /*�˻���*/
        public string RmaNo { get; set; }
    }
}