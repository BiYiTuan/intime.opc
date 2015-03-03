using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service.Map;

namespace Intime.OPC.Service.Support
{
    public class SaleService : BaseService<OPC_Sale>, ISaleService
    {
        private readonly ISaleRemarkRepository _saleRemarkRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IAccountService _accountService;

        public SaleService(ISaleRepository saleRepository, ISaleRemarkRepository saleRemarkRepository, IAccountService accountService)
            : base(saleRepository)
        {
            _saleRepository = saleRepository;
            _saleRemarkRepository = saleRemarkRepository;
            _accountService = accountService;
        }

        #region ISaleService Members

        public IList<OPC_Sale> Select()
        {
            return _saleRepository.Select();
        }

        public IList<OPC_SaleComment> GetRemarksBySaleNo(string saleNo)
        {
            return _saleRemarkRepository.GetBySaleOrderNo(saleNo);
        }

        public bool PrintSale(string saleNo, int userId)
        {
            OPC_Sale saleOrder = _saleRepository.GetBySaleNo(saleNo);
            if (saleOrder == null)
            {
                throw new SaleOrderNotExistsException(saleNo);
            }

            if (saleOrder.Status > (int)EnumSaleOrderStatus.PrintSale)
            {
                return true;
            }

            saleOrder.PrintTimes += 1;
            saleOrder.UpdatedDate = DateTime.Now;
            saleOrder.UpdatedUser = userId;
            _saleRepository.Update(saleOrder);
            return true;
        }

        [Obsolete("废弃此方法 Wallace 2014-05-02")]
        public PageResult<SaleDetailDto> GetSaleOrderDetails(string saleOrderNo, int userId, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(saleOrderNo))
            {
                throw new ArgumentNullException("saleOrderNo");
            }
            OPC_Sale saleOrder = _saleRepository.GetBySaleNo(saleOrderNo);
            if (saleOrder == null)
            {
                throw new SaleOrderNotExistsException(saleOrderNo);
            }

            return _saleRepository.GetSaleOrderDetails(saleOrderNo, pageIndex, pageSize);

        }

        public bool SetSaleOrderPickUp(string saleOrderNo, int userId)
        {
            return UpdateSatus(saleOrderNo, userId, EnumSaleOrderStatus.PickUp);
        }

        /// <summary>
        /// 物流入库
        /// </summary>
        /// <param name="saleOrderNo"></param>
        /// <param name="userId"></param>
        /// <param name="dataRolesStores"></param>
        /// <returns></returns>
        public bool SetShipInStorage(string saleOrderNo, int userId, List<int> dataRolesStores)
        {
            var dto = _saleRepository.GetItemModel(saleOrderNo);
            if (dto == null)
            {
                throw new OpcException(String.Format("销售单{0}未找到.", saleOrderNo));
            }

            if (dataRolesStores != null)
            {
                if (!dataRolesStores.Contains(dto.StoreId))
                {
                    throw new OpcException(String.Format("销售单{0}当前门店{1}({2}),您的门店权限不能操作该销售单入库。", saleOrderNo, dto.StoreName, dto.StoreId));
                }
            }

            var s1 = (EnumCashStatus)(dto.CashStatus ?? 0);
            if (s1 != EnumCashStatus.CashOver)
            {
                throw new OpcException(String.Format("销售单{0}当前收银状态{1}({2})不能入库必须是{3}({4})才能入库。", saleOrderNo, s1.GetDescription(), s1.AsId(), EnumCashStatus.CashOver.GetDescription(), EnumCashStatus.CashOver.AsId()));
            }

            var s2 = (EnumSaleOrderStatus)dto.Status;

            if (dto.OrderProductType == OrderProductType.MiniSilver.AsId())
            {
                //迷你银
                if (!AllowSetCashSatus.Contains(s2.AsId()))
                {
                    throw new OpcException(String.Format("销售单{0}当前状态{1}({2})不能入库必须是{3}({4})才能入库。", saleOrderNo, s2.GetDescription(), s2.AsId(), ((EnumSaleOrderStatus)AllowSetCashSatus[0]).GetDescription(), AllowSetCashSatus[0]));
                }
            }
            else
            {
                if (s2 != EnumSaleOrderStatus.ShoppingGuidePickUp)
                {
                    throw new OpcException(String.Format("销售单{0}当前状态{1}({2})不能入库必须是{3}({4})才能入库。", saleOrderNo, s2.GetDescription(), s2.AsId(), EnumSaleOrderStatus.ShoppingGuidePickUp.GetDescription(), EnumSaleOrderStatus.ShoppingGuidePickUp.AsId()));
                }
            }

            return UpdateSatus(saleOrderNo, userId, EnumSaleOrderStatus.ShipInStorage);
        }

        public bool PrintInvoice(string orderNo, int userId)
        {
            return UpdateSatus(orderNo, userId, EnumSaleOrderStatus.PrintInvoice);
        }

        public bool FinishPrintSale(string orderNo, int userId)
        {
            //return UpdateSatus(orderNo, userId, EnumSaleOrderStatus.PrintSale);
            using (var db = new YintaiHZhouContext())
            {
                var saleOrder = db.OPC_Sales.FirstOrDefault(x => x.SaleOrderNo == orderNo);
                if (saleOrder == null)
                {
                    throw new OpcException(string.Format("无效的销售单号:{0}", orderNo));
                }

                var order = db.Orders.FirstOrDefault(x => x.OrderNo == saleOrder.OrderNo);
                if (order == null)
                {
                    throw new OpcException(string.Format("数据异常，根据销售单号({0})无法找到订单",orderNo));
                }

                if (order.OrderProductType == (int) OrderType.SystemProduct ||
                    order.OrderProductType == (int) OrderType.None)
                {
                    if (string.IsNullOrEmpty(saleOrder.CashNum) || saleOrder.CashStatus != (int)EnumCashStatus.CashOver)
                    {
                        throw new OpcException(string.Format("系统商品订单必须完成收银后才能打印"));
                    }
                }

                saleOrder.Status = (int) EnumSaleOrderStatus.PrintSale;
                saleOrder.UpdatedDate = DateTime.Now;
                saleOrder.UpdatedUser = userId;
                db.SaveChanges();
            }
            return true;
        }

        public bool PrintExpress(string orderNo, int userId)
        {
            return UpdateSatus(orderNo, userId, EnumSaleOrderStatus.PrintExpress);
        }

        public bool Shipped(string orderNo, int userId)
        {
            return UpdateSatus(orderNo, userId, EnumSaleOrderStatus.Shipped);
        }

        public bool StockOut(string orderNo, int userId)
        {
            return UpdateSatus(orderNo, userId, EnumSaleOrderStatus.StockOut);
        }

        public PageResult<SaleDto> GetPickUp(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int userID, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            var user = _accountService.GetByUserID(userID);
            _saleRepository.SetCurrentUser(user);

            return _saleRepository.GetPickUped(saleOrderNo, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            //return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }


        public PageResult<SaleDto> GetPrintSale(string saleId, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);
            var user = _accountService.GetByUserID(userId);
            _saleRepository.SetCurrentUser(user);
            return _saleRepository.GetPrintSale(saleId, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            // return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public PageResult<SaleDto> GetShipped(string saleOrderNo, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            var user = _accountService.GetByUserID(userId);
            _saleRepository.SetCurrentUser(user);
            return _saleRepository.GetShipped(saleOrderNo, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            //return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public PageResult<SaleDto> GetPrintExpress(string saleOrderNo, int userId, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);
            var user = _accountService.GetByUserID(userId);
            _saleRepository.SetCurrentUser(user);
            return _saleRepository.GetPrintExpress(saleOrderNo, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            //return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public PageResult<SaleDto> GetPrintInvoice(string saleOrderNo, int userId, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            var user = _accountService.GetByUserID(userId);
            _saleRepository.SetCurrentUser(user);

            return _saleRepository.GetPrintInvoice(saleOrderNo, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            // return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public PageResult<SaleDto> GetShipInStorage(string saleOrderNo, int userId, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);

            var user = _accountService.GetByUserID(userId);
            _saleRepository.SetCurrentUser(user);

            return _saleRepository.GetShipInStorage(saleOrderNo, orderNo, dtStart, dtEnd, pageIndex, pageSize, user.StoreIds.ToArray());
            //return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public bool WriteSaleRemark(OPC_SaleComment comment)
        {
            return _saleRemarkRepository.Create(comment);
        }

        public PageResult<SaleDto> GetByOrderNo(string orderID, int userid, int pageIndex, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(orderID))
            {
                throw new OrderNoIsNullException(orderID);
            }

            var lst = _saleRepository.GetByOrderNo2(orderID);
            return new PageResult<SaleDto>(lst, lst.Count);
        }

        public IList<SaleDto> GetByShippingCode(string shippingCode)
        {
            var sale = _saleRepository.GetByShippingCode(shippingCode);
            return Mapper.Map<OPC_Sale, SaleDto>(sale);
        }

        public PageResult<SaleDto> GetNoPickUp(string saleId, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);
            var userDto = _accountService.GetByUserID(userId);

            return _saleRepository.GetNoPickUp(saleId, orderNo, dtStart, dtEnd, pageIndex, pageSize, userDto.StoreIds.ToArray());
        }

        /// <summary>
        /// 根据销售单号获取
        /// </summary>
        /// <param name="saleOrderNo">销售单号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="storeIds">门店列表</param>
        /// <returns>销售单列表</returns>
        public PagerInfo<SaleDetailDto> GetSalesDetailsBySaleOrderNo(string saleOrderNo, IEnumerable<int> storeIds, int pageIndex, int pageSize)
        {
            return _saleRepository.GetSalesDetailsBySaleOrderNo(saleOrderNo, storeIds, pageIndex, pageSize);
        }

        /// <summary>
        /// 可以录收银的状态
        /// </summary>
        private static List<int> AllowSetCashSatus = new List<int>
        {
            //EnumSaleOrderStatus.None.AsID(),
            //EnumSaleOrderStatus.NoPickUp.AsID(),
            EnumSaleOrderStatus.PrintSale.AsId(),
            //EnumSaleOrderStatus.PickUp.AsID(),
            EnumSaleOrderStatus.ShoppingGuidePickUp.AsId()
        };

        /// <summary>
        /// 设置销售单收银状态 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExectueResult SetSalesOrderCash(SalesOrderCashRequest request)
        {
            //throw new NotImplementedException();

            /*
             * valid
             * 1.目前只有 orderproducetype=2的可以设置收银状态
             * 2.只有在物流入库前可以设置收银状态
             * 3.只有当前用户权限允许下可以设置状态（门店）
             * 
             * exec
             * 1.设置收银状态为EnumCashStatus.CashOver 完成收银,操作人 ID，操作时间
             */

            var model = _saleRepository.GetItemModel(request.SalesOrderNo);
            if (model == null)
            {
                throw new SaleOrderNotExistsException(request.SalesOrderNo);
            }

            if (!model.OrderProductType.HasValue || model.OrderProductType != (int)OrderProductType.MiniSilver)
            {
                throw new SalesOrderException(String.Format("销售单{0},不是迷你银订单。", model.SaleOrderNo));
            }

            //以入库的不能开通

            if (!AllowSetCashSatus.Contains(model.Status))
            {
                throw new SalesOrderException(String.Format("销售单{0},当前状态{1}({2}),不能录入收银。", model.SaleOrderNo, ((EnumSaleOrderStatus)model.Status).GetDescription(), model.Status));
            }

            if (request.DataRoleStores != null && !request.DataRoleStores.Contains(model.StoreId))
            {
                throw new SalesOrderException(String.Format("销售单{0},门店{1}({2}),您的权限还不能操作。", model.SaleOrderNo, model.StoreName, model.StoreId));
            }

            model.CashDate = DateTime.Now;
            model.CashNum = request.CashNo;
            model.CashStatus = EnumCashStatus.CashOver.AsId();

            _saleRepository.Update4Cash(model, request.UserId);

            return new OkExectueResult();
        }

        #endregion

        private bool UpdateSatus(string saleNo, int userId, EnumSaleOrderStatus saleOrderStatus)
        {
            OPC_Sale saleOrder = _saleRepository.GetBySaleNo(saleNo);
            if (saleOrder == null)
            {
                throw new SaleOrderNotExistsException(saleNo);
            }

            if (saleOrder.Status >= (int)saleOrderStatus)
            {
                return true;
            }
            saleOrder.Status = (int)saleOrderStatus;
            saleOrder.ShippingStatus = saleOrder.Status;
            saleOrder.UpdatedDate = DateTime.Now;
            saleOrder.UpdatedUser = userId;
            _saleRepository.Update(saleOrder);
            return true;
        }
    }
}