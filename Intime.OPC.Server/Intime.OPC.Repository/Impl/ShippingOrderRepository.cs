using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using PredicateBuilder = LinqKit.PredicateBuilder;


namespace Intime.OPC.Repository.Impl
{

    /// <summary>
    /// 发货单（物流单）
    /// </summary>
    internal class ShippingOrderRepository : OPCBaseRepository<int, OPC_ShippingSale>//, IShippingOrderRepository
    {
        #region methods

        private static Expression<Func<OPC_ShippingSale, bool>> Filter(ShippingOrderFilter filter)
        {
            var query = PredicateBuilder.True<OPC_ShippingSale>();

            if (filter != null)
            {
                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.ShippingStatus == (int)filter.Status);
                }

                if (filter.StoreIds != null)
                {
                    query = PredicateBuilder.And(query, v => filter.StoreIds.Contains(v.StoreId ?? -1));
                }

                //订单号互斥 时间
                if (filter.OrderNo != null)
                {
                    query = PredicateBuilder.And(query, v => v.OrderNo == filter.OrderNo);
                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.ExpressNo))
                {
                    query = PredicateBuilder.And(query, v => v.ShippingCode == filter.ExpressNo);

                    return query;
                }



                //发货时间 目前数据库中没这个字段
                if (filter.ShipStartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate >= filter.ShipStartDate);
                }

                if (filter.ShipEndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate < filter.ShipEndDate);
                }

                if (!String.IsNullOrWhiteSpace(filter.CustomersPhoneNumber))
                {
                    query = PredicateBuilder.And(query, v => v.ShippingContactPhone == filter.CustomersPhoneNumber);
                }
            }

            return query;
        }

        private static Expression<Func<Order, bool>> Order4Filter(ShippingOrderFilter filter)
        {
            var query = PredicateBuilder.True<Order>();

            if (filter != null)
            {
                if (filter.DateRange != null && String.IsNullOrWhiteSpace(filter.OrderNo))
                {
                    if (filter.DateRange.StartDateTime != null)
                        query = PredicateBuilder.And(query, v => v.CreateDate >= (filter.DateRange.StartDateTime));

                    if (filter.DateRange.EndDateTime != null)
                    {
                        query = PredicateBuilder.And(query, v => v.CreateDate < (filter.DateRange.EndDateTime));
                    }
                }
            }

            return query;
        }

        private static Func<IQueryable<OPC_ShippingSale>, IOrderedQueryable<OPC_ShippingSale>> OrderBy(ShippingOrderSortOrder sortOrder)
        {
            Func<IQueryable<OPC_ShippingSale>, IOrderedQueryable<OPC_ShippingSale>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    orderBy = v => v.OrderByDescending(s => s.CreateDate).ThenBy(s => s.OrderNo);
                    break;
            }

            return orderBy;
        }

        #endregion

        public override IEnumerable<OPC_ShippingSale> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public List<ShippingOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount,
            ShippingOrderFilter filter,
            ShippingOrderSortOrder sortOrder)
        {
            var shippingOrderFilter = Filter(filter);
            var orderFilter = Order4Filter(filter);


            var rst = Func(db =>
            {
                var shippingOrders = db.Set<OPC_ShippingSale>();
                var orders = db.Set<Order>();
                var salesOrders = db.Set<OPC_Sale>();
                var stores = db.Set<Store>();

                var q1 = from ss in shippingOrders.AsExpandable().Where(shippingOrderFilter)
                         from o in orders.AsExpandable().Where(orderFilter)
                         from sale in salesOrders
                         join store in stores on ss.StoreId equals store.Id
                         where ss.Id == sale.ShippingSaleId && o.OrderNo == ss.OrderNo
                         select new
                         {
                             Order = o,
                             ShippingSale = ss,
                             SaleOrders = sale,
                             Store = store
                         };

                var t = q1.Count();

                var q =
                    q1.OrderByDescending(v => v.ShippingSale.CreateDate)
                        .Skip(pagerRequest.SkipCount)
                        .Take(pagerRequest.PageSize).Select(v => new ShippingOrderModel
                        {
                            Id = v.ShippingSale.Id,
                            CustomerAddress = v.ShippingSale.ShippingAddress,
                            CustomerName = v.ShippingSale.ShippingContactPerson,
                            CustomerPhone = v.ShippingSale.ShippingContactPhone,
                            ExpressCode = v.ShippingSale.ShippingCode,
                            ExpressFee = v.ShippingSale.ShippingFee ?? 0,
                            GoodsOutCode = String.Empty,
                            GoodsOutDate = v.ShippingSale.CreateDate,
                            GoodsOutType = String.Empty,//v.ShippingSale.
                            OrderNo = v.ShippingSale.OrderNo,
                            PrintTimes = v.ShippingSale.PrintTimes,
                            RmaNo = v.ShippingSale.RmaNo,
                            SaleOrderNo = v.SaleOrders.SaleOrderNo,
                            ShipCompanyName = v.ShippingSale.ShipViaName,
                            ShipManName = String.Empty,
                            ShippingMethod = String.Empty,
                            ShippingStatus = v.ShippingSale.ShippingStatus,
                            ShippingZipCode = v.ShippingSale.ShippingZipCode,
                            ShipViaExpressFee = v.ShippingSale.ShippingFee ?? 0,
                            ShipViaId = v.ShippingSale.ShipViaId,
                            RMAAddress = v.Store.RMAAddress,
                            RMAPerson = v.Store.RMAPerson,
                            RMAPhone = v.Store.RMAPhone,
                            RMAZipCode = v.Store.RMAZipCode
                        }).ToList();

                return new
                {
                    total = t,
                    data = q
                };
            });

            totalCount = rst.total;
            return rst.data;
        }

        public ShippingOrderModel GetItemModel(int id)
        {
            return Func(db =>
            {
                var shippingOrders = db.Set<OPC_ShippingSale>();
                var orders = db.Set<Order>();
                var salesOrders = db.Set<OPC_Sale>();
                var stores = db.Set<Store>();


                var q1 = from ss in shippingOrders.Where(v => v.Id == id)//.AsExpandable().Where(shippingOrderFilter)
                         from o in orders//.AsExpandable().Where(orderFilter)
                         from sale in salesOrders
                         join store in stores on ss.StoreId equals store.Id
                         where ss.Id == sale.ShippingSaleId && o.OrderNo == ss.OrderNo
                         select new
                         {
                             Order = o,
                             ShippingSale = ss,
                             SaleOrders = sale,
                             Store = store
                         };

                var q =
                    q1.Select(v => new ShippingOrderModel
                        {
                            Id = v.ShippingSale.Id,
                            CustomerAddress = v.ShippingSale.ShippingAddress,
                            CustomerName = v.ShippingSale.ShippingContactPerson,
                            CustomerPhone = v.ShippingSale.ShippingContactPhone,
                            ExpressCode = v.ShippingSale.ShippingCode,
                            ExpressFee = v.ShippingSale.ShippingFee ?? 0,
                            GoodsOutCode = String.Empty,
                            GoodsOutDate = v.ShippingSale.CreateDate,
                            GoodsOutType = String.Empty,//v.ShippingSale.
                            OrderNo = v.ShippingSale.OrderNo,
                            PrintTimes = v.ShippingSale.PrintTimes,
                            RmaNo = v.ShippingSale.RmaNo,
                            SaleOrderNo = v.SaleOrders.SaleOrderNo,
                            ShipCompanyName = v.ShippingSale.ShipViaName,
                            ShipManName = String.Empty,
                            ShippingMethod = String.Empty,
                            ShippingStatus = v.ShippingSale.ShippingStatus,
                            ShippingZipCode = v.ShippingSale.ShippingZipCode,
                            ShipViaExpressFee = v.ShippingSale.ShippingFee ?? 0,
                            ShipViaId = v.ShippingSale.ShipViaId,
                            RMAAddress = v.Store.RMAAddress,
                            RMAPerson = v.Store.RMAPerson,
                            RMAPhone = v.Store.RMAPhone,
                            RMAZipCode = v.Store.RMAZipCode
                        }).FirstOrDefault();

                return q;
            });
        }

        /// <summary>
        /// 生成快递单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        public void Update4ShippingCode(OPC_ShippingSale entity, int userId)
        {
            Action(db =>
           {
               using (var trans = new TransactionScope())
               {
                   EFHelper.Update(db, entity);
                   //同步状态
                   SyncStatus(db, entity.ShippingStatus, entity.Id, userId, entity);



                   trans.Complete();
               }
               // return entity;
           });
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
            return Func(db =>
            {
                using (var trans = new TransactionScope())
                {
                    entity = EFHelper.Insert(db, entity);
                    if (!String.IsNullOrWhiteSpace(shippingRemark))
                    {
                        var remark = new OPC_ShippingSaleComment
                        {
                            Content = shippingRemark,
                            CreateDate = DateTime.Now,
                            CreateUser = userId,
                            ShippingCode = String.Empty,
                            ShippingSaleId = entity.Id,
                            UpdateDate = DateTime.Now,
                            UpdateUser = userId,

                        };
                        EFHelper.Insert(db, remark);
                    }

                    foreach (var saleEntity in saleOrderModels)
                    {
                        //var saleEntity = EFHelper.Find<OPC_Sale>(db, sale.Id);

                        if (saleEntity == null)
                        {
                            throw new SalesOrderException("参数saleordermodels为空");
                        }

                        //判断必须是入库状态才能生成发货单
                        if (saleEntity.Status != (int)Domain.Enums.EnumSaleOrderStatus.ShipInStorage)
                        {
                            throw new SalesOrderException(String.Format("请检查销售单状态{0}，必须是{1}，才能生成发货单。", saleEntity.Status.ToString(), Intime.OPC.Domain.Enums.EnumSaleOrderStatus.ShipInStorage.GetDescription()));
                        }

                        saleEntity.ShippingSaleId = entity.Id;
                        saleEntity.UpdatedUser = userId;
                        saleEntity.UpdatedDate = DateTime.Now;
                        saleEntity.ShippingRemark = shippingRemark ?? String.Empty;
                        saleEntity.Status = entity.ShippingStatus ?? (int)Domain.Enums.EnumSaleOrderStatus.PrintInvoice;
                        saleEntity.ShippingStatus = entity.ShippingStatus ?? (int)Domain.Enums.EnumSaleOrderStatus.PrintInvoice;

                        EFHelper.UpdateEntityFields(db, saleEntity, new List<string> { "Status", "ShippingStatus", "ShippingSaleId", "UpdatedUser", "UpdatedDate", "ShippingRemark" });
                    }

                    trans.Complete();
                }

                return GetItemModel(entity.Id);
            });
        }

        public OPC_ShippingSaleComment CreateComment(OPC_ShippingSaleComment entity, int userId)
        {
            return Func(db =>

                EFHelper.Insert(db, entity)
            );
        }

        public void UpdateComment(OPC_ShippingSaleComment entity, int userId)
        {
            Action(db =>
   EFHelper.Update(db, entity)
);
        }

        public void Update4Print(ShippingOrderModel model, Intime.OPC.Domain.Dto.Request.DeliveryOrderPrintRequest request, int userId)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Action(db =>
            {
                var entity = EFHelper.Find<OPC_ShippingSale>(db, model.Id);
                if (entity == null)
                {
                    throw new ShippingSaleException(String.Format("没有找到 快递单{0}，请重新选择或与管理员联系", model.Id));
                }

                entity.PrintTimes = entity.PrintTimes + request.Times ?? 1;
                //entity.UpdateUser = userId;
                //entity.UpdateDate = DateTime.Now;

                var fieldUpdate = new List<string>();
                fieldUpdate.Add("PrintTimes");
                //fieldUpdate.Add("UpdateUser");
                //fieldUpdate.Add("UpdateDate");

                EFHelper.UpdateEntityFields(db, entity, fieldUpdate);

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="status"></param>
        /// <param name="shippingId"></param>
        /// <param name="userId"></param>
        private static void SyncStatus(DbContext db, int? status, int shippingId, int userId, OPC_ShippingSale entity)
        {
            if (status != null)
            {
                var sales = EFHelper.Get<OPC_Sale>(db, v => v.ShippingSaleId == shippingId).ToList();
                if (sales.Count == 0)
                {
                    throw new SaleOrderNotFoundException(String.Format("没有找到发货单id:{0},对应的销售单", shippingId));
                }

                foreach (var s in sales)
                {
                    //重置状态的 不应大于 打印快递单
                    //已经发货就不能重新至状态了
                    if (s.ShippingStatus > (int)Intime.OPC.Domain.Enums.EnumSaleOrderStatus.PrintExpress)
                    {
                        throw new SalesOrderException(String.Format("销售单no:{0}，状态{1}、发货单状态是{2},不能再恢复发货单状态{3},或请与管理员联系", s.SaleOrderNo, s.Status, s.ShippingStatus, status));
                    }
                    s.ShippingStatus = status.Value;
                    s.Status = status.Value;
                    s.UpdatedDate = DateTime.Now;
                    s.UpdatedUser = userId;
                    var fieldList =
                   new List<string>
                    {
                        "Status",
                        "ShippingStatus",
                        "UpdatedDate",
                        "UpdatedUser"
                    };

                    if (entity != null)
                    {
                        s.ShipViaId = entity.ShipViaId;
                        s.ShippingFee = entity.ShippingFee;
                        s.ShippingCode = entity.ShippingCode;

                        fieldList.AddRange(new List<string>     {                   "ShipViaId",
                        "ShippingFee",
                        "ShippingCode"});
                    }


                    EFHelper.UpdateEntityFields(db, s, fieldList);
                }
            }
        }

        public void Sync4Status(ShippingOrderModel model, int userId)
        {
            var entity = GetItem(model.Id);
            if (entity == null)
            {
                throw new ShippingSaleException(String.Format("不能获取 快递单 单号{0}", model.Id));
            }

            entity.ShippingStatus = model.ShippingStatus;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = userId;

            Action(db =>
            {
                using (var trans = new TransactionScope())
                {
                    EFHelper.UpdateEntityFields(db, entity, new List<string>
                    {
                        "ShippingStatus",
                        "UpdateDate",
                        "UpdateUser",
                    });
                    SyncStatus(db, entity.ShippingStatus, entity.Id, userId, null);

                    trans.Complete();
                }
            });


        }
    }
}
