using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    [Uri("deliveryorder")]
    public class OPC_ShippingSale : Model
    {
        private string _expressCode;
        private string _shipCompanyName;
        private double _expressFee;
        private double _shipViaExpressFee;

        /// <summary>
        /// 退货单号
        /// </summary>
        public string RmaNo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        public string GoodsOutCode { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressCode
        {
            get { return _expressCode; }
            set { SetProperty(ref _expressCode, value); }
        }

        /// <summary>
        /// 发货状态
        /// </summary>
        public string ShippingStatus { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        ///  发货时间
        /// </summary>
        public DateTime GoodsOutDate { get; set; }

        /// <summary>
        /// 发货方式
        /// </summary>
        public string GoodsOutType { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string ShipCompanyName
        {
            get { return _shipCompanyName; }
            set { SetProperty(ref _shipCompanyName, value); }
        }

        /// <summary>
        /// 快递员
        /// </summary>
        public string ShipManName { get; set; }

        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintStatus { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ShippingZipCode { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        /// <value>The shipping method.</value>
        public string ShippingMethod { get; set; }

        /// <summary>
        /// 支付快递公司快递费
        /// </summary>
        /// <value>The express fee.</value>
        public double ShipViaExpressFee
        {
            get { return _shipViaExpressFee; }
            set { SetProperty(ref _shipViaExpressFee, value); }
        }

        /// <summary>
        /// 快递费
        /// </summary>
        /// <value>The express fee.</value>
        public double ExpressFee
        {
            get { return _expressFee; }
            set { SetProperty(ref _expressFee, value); }
        }


        #region Added by Frank Gao

        /// <summary>
        /// 快递公司ID
        /// </summary>
        public int? ShipViaId { get; set; }

        /// <summary>
        /// 包含的销售单
        /// </summary>
        public IList<OPC_Sale> SalesOrders { get; set; }

        #endregion
    }
}