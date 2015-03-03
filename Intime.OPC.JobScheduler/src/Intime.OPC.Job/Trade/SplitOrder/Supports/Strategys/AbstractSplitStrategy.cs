using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports.Strategys
{
    public abstract class AbstractSplitStrategy : ISplitStrategy
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public virtual SpliltResultModel Split(OrderModel order,
            SectionStocksModel sectionStocks)
        {
            /**
            * sectionId和SaleDetail的对照表
            */
            var sectionId2SaleDetails = new Dictionary<int, List<OPC_SaleDetail>>();

            foreach (var item in order.Items)
            {
                /**
                 * 进行销售单详情处理
                 */
                //var saleDetails = ProcessOrderItem2SectionSaleDetail(item, sectionStocks[item.SkuId]);
                var saleDetails = ProcessOrderItem2SectionSaleDetail(item, sectionStocks[item.SkuId].Where(x => x.Price == item.ItemPrice));

                          if (saleDetails == null)
                {
                    throw new NullReferenceException("detailInfo");
                }

                //if (saleDetails.Count == 0)
                //{
                //    throw new ApplicationException("saleDetails length is 0");
                //}

                /**
                 * 遍历添加拆分后的销售清单项
                */
                foreach (var saleDetail in saleDetails)
                {
                    if (sectionId2SaleDetails.ContainsKey(saleDetail.SectionId))
                    {
                        sectionId2SaleDetails[saleDetail.SectionId].Add(saleDetail.SaleDetail);
                    }
                    else
                    {
                        sectionId2SaleDetails[saleDetail.SectionId] = new List<OPC_SaleDetail> { saleDetail.SaleDetail };
                    }
                }
            }

            // 处理订单的结果
            var result = new List<SaleOrderModel>();

            var index = 1;
            foreach (var sectionId in sectionId2SaleDetails.Keys)
            {
                var saleOrderInfo = ProcessSaleOrder(index, order, sectionId, sectionId2SaleDetails[sectionId]);

                if (saleOrderInfo == null)
                {
                    throw new NullReferenceException("saleOrderInfo");
                }

                result.Add(saleOrderInfo);

                index += 1;
            }

            return new SpliltResultModel(result);
        }

        /// <summary>
        /// 处理销售清单到到专柜
        /// </summary>
        /// <param name="orderItem">原始订单中的订单详单项</param>
        /// <param name="stocks">专柜库存列表</param>
        /// <returns>专柜销售清单项</returns>
        protected abstract IList<SectionSaleDetailInfo> ProcessOrderItem2SectionSaleDetail(
            OrderItemModel orderItem, IEnumerable<OPC_Stock> stocks);

        /// <summary>
        /// 处理销售单
        /// </summary>
        /// <param name="index">销售单的当前索引Id,用于生产SaleId</param>
        /// <param name="order">原始订单</param>
        /// <param name="sectionId">专柜Id</param>
        /// <param name="details">销售单清单</param>
        /// <returns>销售单</returns>
        protected virtual SaleOrderModel ProcessSaleOrder(int index, OrderModel order, int sectionId, IList<OPC_SaleDetail> details)
        {
            var saleOrderNo = GenerateSaleId(order.OrderNo, index);

            // 设置销售详情各项的SaleOrderNo 
            //增加计算销售单总金额
            foreach (var detail in details)
            {
                detail.SaleOrderNo = saleOrderNo;                
            }

            var result = new SaleOrderModel(details)
            {
                OrderNo = order.OrderNo,
                SaleOrderNo = saleOrderNo,
                Status = SystemDefine.SaleOrderDefaultStatus,
                SellDate = order.CreateDate,
                SalesAmount = details.Sum(c => c.Price * c.SaleCount), 
                SalesCount = details.Sum(c => c.SaleCount),
                SectionId = sectionId,
                CreatedDate = DateTime.Now,
                CreatedUser = SystemDefine.SysUserId
            };

            return result;
        }

        private string GenerateSaleId(string orderNo, int index)
        {
            return string.Format("{0}-{1}", orderNo, index.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
        }


        public abstract bool Support(OrderModel order, SectionStocksModel sectionStocks);
    }
}
