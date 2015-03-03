using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Dto.Request
{
    public class CreateShippingSaleOrderRequest
    {
        [Required(ErrorMessage = "必须提供销售单号")]
        public List<string> SalesOrderNos { get; set; }
    }

    public class GetOrderItemsRequest
    {
        /// <summary>
        /// 订单 no
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单 no
        /// </summary>
        public string SaleOrderNo { get; set; }

        /// <summary>
        /// 快递单OR出库单 ID
        /// </summary>
        public int ShippingOrderId { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortOrder { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品名前缀
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string ContactPhone { get; set; }
    }

    public class GetShippingSaleOrderRequest : DatePageRequest, IStoreDataRoleRequest
    {
        /// <summary>
        /// 订单 no
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单 no
        /// </summary>
        public string SaleOrderNo { get; set; }

        public int? SortOrder { get; set; }


        /// <summary>
        /// 销售单状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 销售单状态
        /// </summary>
        public EnumSaleOrderStatus? SaleOrderStatus
        {
            get { return Status == null ? default(EnumSaleOrderStatus?) : (EnumSaleOrderStatus?)Status; }
        }

        /// <summary>
        /// 是否生成发货单
        /// </summary>
        public bool HasDeliveryOrderGenerated { get; set; }

        /// <summary>
        /// 查询指定门店
        /// </summary>
        public List<int> StoreIds { get; set; }

        /// <summary>
        /// 是否查询所有门店
        /// </summary>
        public bool IsAllStoreIds { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNo { get; set; }

        /// <summary>
        /// 发货 开始时间
        /// </summary>
        public DateTime? StartGoodsOutDate { get; set; }

        /// <summary>
        /// 发货 结束时间
        /// </summary>
        public DateTime? EndGoodsOutDate { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string CustomerPhone { get; set; }

        ///// <summary>
        ///// 订单状态
        ///// </summary>
        //public int? ShippingStatus { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public int? StoreId { get; set; }

        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        public override void ArrangeParams()
        {
            Status = CheckIsNullOrAndSet(Status);
            //ShippingStatus = CheckIsNullOrAndSet(ShippingStatus);
            StoreId = CheckIsNullOrAndSet(StoreId);

            if (StartGoodsOutDate != null)
            {
                StartGoodsOutDate = StartGoodsOutDate.Value.Date;
            }

            if (EndGoodsOutDate != null)
            {
                EndGoodsOutDate = EndGoodsOutDate.Value.Date.AddDays(1);
            }

            base.ArrangeParams();
        }
    }

    public class PutShippingSaleOrderRequest
    {
        /// <summary>
        /// 发货单号
        /// </summary>
        public int? ShippingSaleOrderId { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public int? ShipViaId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200, ErrorMessage = "备注请不要超过200个字符", MinimumLength = 0)]
        public string ShippingRemark { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        [Required(ErrorMessage = "必须提供快递单号")]
        public string ShippingNo { get; set; }

        /// <summary>
        /// 快递费
        /// </summary>
        [Required(ErrorMessage = "必须提供快递费")]
        [RegularExpression(@"^[0-9]+(.[0-9]{1,2})?$", ErrorMessage = "请填写正确的快递费")]
        public decimal ShippingFee { get; set; }
    }
}
