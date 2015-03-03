using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;
using Intime.OPC.Job.Trade.SplitOrder.Models;
using Intime.OPC.Job.Trade.SplitOrder.Supports;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 拆单处理器
    /// </summary>
    public class SplitOrderProcessor
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private IOrderStore _orderStore = new DefaultOrderStore();
        private IOrderStockStore _orderStockStore = new DefaultOrderStockStore();
        private ISaleOrderStore _saleOrderStore = new DefaultSaleOrderStore();
        private readonly IEnumerable<ISplitStrategy> _splitStrategies;

        public SplitOrderProcessor(IEnumerable<ISplitStrategy> strategies)
        {
            _splitStrategies = strategies;
        }

        /// <summary>
        /// 处理拆单
        /// </summary>
        public void Process()
        {
            var orders = _orderStore.GetPendingOrders().ToList();

            Log.InfoFormat("获取订单数量{0}", orders.Count);

            foreach (var order in orders)
            {
                if (order.OrderProductType.HasValue && order.OrderProductType.Value != 1)
                {
                    Log.InfoFormat("自拍商品订单{0},暂不处理",order.OrderNo);
                    continue;
                }
                try
                {
                    ProcessOrder(order);
                }
                catch (OrderSplitException e)
                {
                    _saleOrderStore.SaveSplitOrderLog(order.OrderNo, e.Message, (int)e.Status);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        /**=======================================================
         * 
         * 拆单逻辑简介:
         * 
         * 1. 获取订单待处理的订单信息
         * 2. 从订单中获取所有的SKU
         * 3. 根据SKU获取专柜的库存列表
         * 4. 调用拆单的策略链条进行拆单
         * 5. 根据拆单后的结果进行处理，保存销售单
         =========================================================*/
        private void ProcessOrder(OrderModel order)
        {
            if (order.Items == null || !order.Items.Any())
            {
                throw new OrderSplitException(string.Format("orderId:{0},商品清单为空,请进行排查", order.OrderNo), OrderSplitErrorStatus.InvalidItems);
            }

            // 提取订单中的SKU列表
            var skus = order.Items.Select(item => item.SkuId).ToList();

            // 获取专柜的库存列表
            var stocks = _orderStockStore.GetStocksBySkus(skus);

            if (stocks == null || stocks.Count == 0)
            {
                throw new OrderSplitException(string.Format("orderId:{0},skus:{1}专柜库存不足,请进行排查", order.OrderNo, String.Join(",", skus)), OrderSplitErrorStatus.InsufficientStock);
            }

            var splitResult = Spit(order, stocks);

            //处理拆单异常,进行状态的保存
            if (splitResult.HasException)
            {
                throw new OrderSplitException(splitResult.Exception.Message, OrderSplitErrorStatus.SplitError);
            }

            //保存数据
            _saleOrderStore.Save(splitResult.SaleOrderInfos);

            // 更新库存
            _orderStockStore.UpdateStock(splitResult.SaleOrderInfos);
            Log.InfoFormat("更新库存完成,orderNo:{0}", order.OrderNo);
            _saleOrderStore.SaveSplitOrderLog(order.OrderNo, "拆单完成", 1);
        }

        private SpliltResultModel Spit(OrderModel order, SectionStocksModel stocks)
        {
            foreach (var splitStrategy in _splitStrategies)
            {
                if (splitStrategy.Support(order, stocks))
                {
                    return splitStrategy.Split(order, stocks);
                }
            }

            return new SpliltResultModel(null, new NotSupportedException("splitStrategy"));
        }

        /// <summary>
        /// 设置专柜库存存储提供实现
        /// </summary>
        /// <param name="orderStockStore">专柜库存存储提供实现</param>
        public void SetOrderStockStore(IOrderStockStore orderStockStore)
        {
            if (orderStockStore == null)
            {
                throw new ArgumentNullException("orderStockStore");
            }
            _orderStockStore = orderStockStore;
        }

        /// <summary>
        /// 设置订单存储存储提供实现
        /// </summary>
        /// <param name="orderStore">订单存储存储提供实现</param>
        public void SetOrderStore(IOrderStore orderStore)
        {
            if (orderStore == null)
            {
                throw new ArgumentNullException("orderStore");
            }

            _orderStore = orderStore;
        }

        /// <summary>
        /// 设置订单存储存储提供实现
        /// </summary>
        /// <param name="saleOrderStore">订单存储存储提供实现</param>
        public void SetSaleOrderStore(ISaleOrderStore saleOrderStore)
        {
            if (saleOrderStore == null)
            {
                throw new ArgumentNullException("saleOrderStore");
            }

            _saleOrderStore = saleOrderStore;
        }
    }

    public class OrderSplitException : Exception
    {

        public OrderSplitErrorStatus Status { get; private set; }

        public OrderSplitException(string message, OrderSplitErrorStatus status)
            : base(message)
        {
            this.Status = status;
        }
    }

    public enum OrderSplitErrorStatus
    {
        InvalidItems = -1,
        InsufficientStock = -2,
        SplitError = -3,
        UnKown = -4,
    }
}
