using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Transactions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository.Base;
using Intime.OPC.Repository.Impl;


namespace Intime.OPC.Repository.Support
{
    public class ShippingSaleRepository : BaseRepository<OPC_ShippingSale>, IShippingSaleRepository
    {
        private readonly ShippingOrderRepository _shippingOrderRepository = new ShippingOrderRepository();

        #region IShippingSaleRepository Members

        public OPC_ShippingSale GetBySaleOrderNo(string saleOrderNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                OPC_Sale sale = db.OPC_Sales.FirstOrDefault(t => t.SaleOrderNo == saleOrderNo);
                if (sale == null)
                {
                    throw new SaleOrderNotExistsException(saleOrderNo);
                }
                return db.OPC_ShippingSales.FirstOrDefault(t => t.ShippingCode == sale.ShippingCode);
            }
        }

        public PageResult<OPC_ShippingSale> GetByShippingCode(string shippingCode, int pageIndex, int pageSize = 20)
        {
            return Select(t => t.ShippingCode == shippingCode, t => t.UpdateDate, false, pageIndex, pageSize);
        }

        public PageResult<OPC_ShippingSale> Get(string shippingCode, DateTime startTime, DateTime endTime,
            int shippingStatus, int pageIndex, int pageSize = 20)
        {
            Expression<Func<OPC_ShippingSale, bool>> filterExpression =
                t => t.CreateDate >= startTime && t.CreateDate < endTime && t.ShippingStatus == shippingStatus;
            if (!string.IsNullOrWhiteSpace(shippingCode))
            {
                filterExpression.And(t => t.ShippingCode.Contains(shippingCode));
            }
            return Select(filterExpression, t => t.UpdateDate, false, pageIndex, pageSize);
        }

        public PageResult<OPC_ShippingSale> GetShippingSale(string saleOrderNo, string expressNo,
            DateTime startGoodsOutDate, DateTime endGoodsOutDate, string outGoodsCode, int sectionId, int shippingStatus,
            string customerPhone, int brandId, int pageIndex, int pageSize)
        {
            //Expression<Func<OPC_ShippingSale, bool>> filterExpression =
            //   t => t.CreateDate >= startGoodsOutDate && t.CreateDate < endGoodsOutDate ;
            ////todo 增加订单查询条件
            //if (shippingStatus>-1)
            //{
            //    filterExpression = filterExpression.And(t => t.ShippingStatus == shippingStatus);
            //}

            //if (!string.IsNullOrWhiteSpace(expressNo))
            //{
            //    filterExpression = filterExpression.And(t => t.ShippingCode.Contains(expressNo));
            //}

            //return Select(filterExpression, t => t.CreateDate, false, pageIndex, pageSize);

            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OPC_ShippingSale> query =
                    db.OPC_ShippingSales.Where(t => t.CreateDate >= startGoodsOutDate && t.CreateDate < endGoodsOutDate && t.StoreId.HasValue && CurrentUser.StoreIds.Contains(t.StoreId.Value));
                if (shippingStatus > -1)
                {
                    query = query.Where(t => t.ShippingStatus == shippingStatus);
                }

                if (!string.IsNullOrWhiteSpace(expressNo))
                {
                    query = query.Where(t => t.ShippingCode.Contains(expressNo));
                }
                query = query.OrderBy(t => t.CreateDate);
                return query.ToPageResult(pageIndex, pageSize);
            }
        }

        public OPC_ShippingSale GetByRmaNo(string rmaNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_ShippingSales.FirstOrDefault(t => t.RmaNo == rmaNo);
            }
        }

        public PageResult<OPC_ShippingSale> GetByOrderNo(string orderNo, DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
            int shippingStatus)
        {
            using (var db = new YintaiHZhouContext())
            {
                var lst =
                    db.OPC_ShippingSales.Where(
                        t => t.CreateDate >= startDate && t.CreateDate < endDate && t.ShippingStatus == shippingStatus && t.StoreId.HasValue && CurrentUser.StoreIds.Contains(t.StoreId.Value));
                if (orderNo.IsNotNull())
                {
                    lst = lst.Where(t => t.OrderNo.Contains(orderNo));
                }
                lst = lst.OrderByDescending(t => t.CreateDate);
                return lst.ToPageResult(pageIndex, pageSize);
            }
        }



        #endregion

        public PageResult<OPC_ShippingSale> GetShippingSale(string saleOrderNo, string expressNo,
            DateTime startGoodsOutDate,
            DateTime endGoodsOutDate, int sectionId, int shippingStatus,
            string customerPhone, int brandId, int pageIndex, int pageSize)
        {
            Expression<Func<OPC_ShippingSale, bool>> filterExpression =
                t =>
                    t.CreateDate >= startGoodsOutDate && t.CreateDate < endGoodsOutDate &&
                    t.ShippingStatus == shippingStatus;

            if (string.IsNullOrWhiteSpace(expressNo))
            {
                filterExpression = filterExpression.And(t => t.ShippingCode.Contains(expressNo));
            }

            if (sectionId > 0)
            {
                //filterExpression = filterExpression.And(t => t(expressNo));
            }
            if (CurrentUser != null)
            {
                var ll = CurrentUser.StoreIds;
                filterExpression = filterExpression.And(t => t.StoreId.HasValue && ll.Contains(t.StoreId.Value));
                // && CurrentUser.StoreIDs.Contains(t.StoreId)
            }
            return Select(filterExpression, t => t.CreateDate, false, pageIndex, pageSize);
        }


        public List<ShippingOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount,
    ShippingOrderFilter filter,
    ShippingOrderSortOrder sortOrder)
        {

            return _shippingOrderRepository.GetPagedList(pagerRequest, out totalCount, filter, sortOrder);
        }

        public ShippingOrderModel GetItemModel(int id)
        {
            return _shippingOrderRepository.GetItemModel(id);
        }

        /// <summary>
        /// 生成快递单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        public void Update4ShippingCode(OPC_ShippingSale entity, int userId)
        {
            _shippingOrderRepository.Update4ShippingCode(entity, userId);
        }

        /// <summary>
        /// 创建发货单，
        /// </summary>
        /// <param name="entity">shipping</param>
        /// <param name="saleOrderModels">销售单</param>
        /// <param name="userId">操作人</param>
        /// <returns></returns>
        public ShippingOrderModel CreateBySaleOrder(OPC_ShippingSale entity, List<OPC_Sale> saleOrderModels, int userId, string shippingRemark)
        {
            return _shippingOrderRepository.CreateBySaleOrder(entity, saleOrderModels, userId, shippingRemark);
        }

        public OPC_ShippingSaleComment CreateComment(OPC_ShippingSaleComment entity, int userId)
        {
            return _shippingOrderRepository.CreateComment(entity, userId);
        }

        public void UpdateComment(OPC_ShippingSaleComment entity, int userId)
        {
            _shippingOrderRepository.UpdateComment(entity, userId);
        }

        public void Update4Print(ShippingOrderModel model, Intime.OPC.Domain.Dto.Request.DeliveryOrderPrintRequest request, int userId)
        {
            _shippingOrderRepository.Update4Print(model, request, userId);
        }

        public void Sync4Status(ShippingOrderModel model, int userId)
        {
            _shippingOrderRepository.Sync4Status(model, userId);
        }
    }
}