using System;
using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service.Map;

namespace Intime.OPC.Service.Support
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderRemarkRepository _orderRemarkRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        //private readonly IBrandRepository _brandRepository;
        private readonly IAccountService _accountService;
        //private ISaleDetailRepository _saleDetailRepository;
        private readonly ISaleRMARepository _saleRmaRepository;
        private readonly IEnumService _enumService;

        public OrderService(IOrderRepository orderRepository, IOrderRemarkRepository orderRemarkRepository, IOrderItemRepository orderItemRepository, IAccountService accountService, ISaleRMARepository saleRmaRepository, IEnumService enumService)
            : base(orderRepository)
        {
            _orderRepository = _repository as IOrderRepository;
            _orderRemarkRepository = orderRemarkRepository;
            _orderItemRepository = orderItemRepository;
            // _brandRepository = brandRepository;
            _accountService = accountService;
            //_saleDetailRepository = saleDetailRepository;
            _saleRmaRepository = saleRmaRepository;
            _enumService = enumService;
        }

        public PageResult<OrderDto> GetOrder(string orderNo, string orderSource, DateTime dtStart, DateTime dtEnd,
            int storeId, int brandId, int status, string paymentType, string outGoodsType, string shippingContactPhone,
            string expressDeliveryCode, int expressDeliveryCompany, int userId, int pageIndex, int pageSize = 20)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var pg = _orderRepository.GetOrder(orderNo, orderSource, dtStart, dtEnd, storeId, brandId,
                status, paymentType,
                outGoodsType, shippingContactPhone, expressDeliveryCode, expressDeliveryCompany, pageIndex, pageSize);

            var r = Mapper.Map<Order, OrderDto>(pg.Result);
            return new PageResult<OrderDto>(r, pg.TotalCount);
        }

        public OrderDto GetOrderByOrderNo(string orderNo)
        {
            //var e = _orderRepository.GetOrderByOrderNo(orderNo);
            //if (e==null)
            //{
            //    throw new OrderNotExistsException(orderNo);
            //}
            //return  Mapper.Map<Order, OrderDto>(e);
            var model = _orderRepository.GetItemByOrderNo(orderNo);
            if (model == null)
            {
                throw new OrderNotExistsException(orderNo);
            }

            return AutoMapper.Mapper.Map<OrderModel, OrderDto>(model);
        }

        #region 有关备注


        public bool AddOrderComment(OPC_OrderComment comment)
        {

            return _orderRemarkRepository.Create(comment);
        }

        public PageResult<OrderDto> GetOrderByOderNoTime(string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date.AddDays(1);
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lstOrder = _orderRepository.GetOrderByOderNoTime(orderNo, dtStart, dtEnd, pageIndex, pageSize);
            return Mapper.Map<Order, OrderDto>(lstOrder);
        }

        public PageResult<OrderItemDto> GetOrderItems(string orderNo, int pageIndex, int pageSize)
        {
            _orderItemRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lstOrderItems = _orderItemRepository.GetByOrderNo(orderNo, pageIndex, pageSize);

            return lstOrderItems;
        }

        public PageResult<OrderDto> GetOrderByShippingNo(string shippingNo, int pageIndex, int pageSize)
        {
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderRepository.GetOrderByShippingNo(shippingNo, pageIndex, pageSize);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PageResult<OrderDto> GetByReturnGoodsInfo(ReturnGoodsInfoRequest request)
        {
            request.FormatDate();
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderRepository.GetByReturnGoodsInfo(request);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PageResult<OrderDto> GetShippingBackByReturnGoodsInfo(ReturnGoodsInfoRequest request)
        {
            request.FormatDate();

            int status = EnumRMAStatus.ShipVerifyNotPass.AsId();
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderRepository.GetBySaleRma(request, status, EnumReturnGoodsStatus.NoProcess);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PageResult<OrderDto> GetSaleRmaByReturnGoodsCompensate(ReturnGoodsInfoRequest request)
        {
            request.FormatDate();
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderRepository.GetBySaleRma(request, null, EnumReturnGoodsStatus.CompensateVerifyFailed);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PageResult<OrderDto> GetOrderByOutOfStockNotify(OutOfStockNotifyRequest request)
        {
            request.FormatDate();
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            int orderstatus = EnumOderStatus.StockOut.AsId();
            var lst = _orderRepository.GetByOutOfStockNotify(request, orderstatus);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PageResult<OrderDto> GetOrderOfVoid(OutOfStockNotifyRequest request)
        {
            request.FormatDate();
            _orderRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            int orderstatus = EnumOderStatus.Void.AsId();
            var lst = _orderRepository.GetByOutOfStockNotify(request, orderstatus);
            return Mapper.Map<Order, OrderDto>(lst);
        }

        public PagedSaleDetailStatListDto WebSiteStatSaleDetail(SearchStatRequest request)
        {
            //request.FormatDate();
            //_orderItemRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderItemRepository.WebSiteStatSaleDetailPaged(request);
            lst.Stat();
            return lst;

        }

        public PagedReturnGoodsStatListDto WebSiteStatReturnDetail(SearchStatRequest request)
        {
            //request.FormatDate();
            //_orderItemRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _orderItemRepository.WebSiteStatReturnGoodsPaged(request);
            lst.Stat();
            return lst;
        }

        public PagedCashierList WebSiteCashier(SearchCashierRequest request)
        {
            //request.FormatDate();
            //_orderItemRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            if (!String.IsNullOrWhiteSpace(request.FinancialType))
            {
                var finacialItems = _enumService.All(DefinitionField.Financial);
                var item = finacialItems.FirstOrDefault(v => String.Compare(v.Key, request.FinancialType, StringComparison.OrdinalIgnoreCase) == 0);
                if (item != null)
                {
                    switch (item.Key)
                    {
                        case "-1"://全部
                            request.FinancialType = String.Empty;
                            break;
                        case "0"://进账
                            request.FinancialType = DefinitionField.Sales;
                            break;
                        case "5"://退帐
                            request.FinancialType = DefinitionField.Rma;
                            break;
                    }
                }
            }

            var lst = _orderItemRepository.WebSiteCashierPaged(request);
            lst.Stat();
            return lst;
        }

        public PageResult<OrderItemDto> GetOrderItemsAutoBack(string orderNo, int pageIndex, int pageSize)
        {
            _orderItemRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lstOrderItems = _orderItemRepository.GetOrderItemsAutoBack(orderNo, pageIndex, pageSize);

            return lstOrderItems;
        }

        public void SetSaleOrderVoid(string saleOrderNo)
        {
            _orderItemRepository.SetSaleOrderVoid(saleOrderNo);
            _saleRmaRepository.SetVoidBySaleOrder(saleOrderNo);

        }

        public PageResult<OrderDto> GetPagedList(OrderQueryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var pager = _orderRepository.GetPagedList(request.PagerRequest, request, request.OrderSortOrder);

            var datas = AutoMapper.Mapper.Map<List<OrderModel>, List<OrderDto>>(pager.Datas);

            var rst = new PageResult<OrderDto>(datas, pager.TotalCount);

            return rst;
        }


        public PageResult<OrderDto> GetPagedListExcludeSalesOrder(OrderQueryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var pager = _orderRepository.GetPagedListExcludeSalesOrder(request.PagerRequest, request, request.OrderSortOrder);

            var datas = AutoMapper.Mapper.Map<List<OrderModel>, List<OrderDto>>(pager.Datas);

            var rst = new PageResult<OrderDto>(datas, pager.TotalCount);

            return rst;
        }

        public IList<OPC_OrderComment> GetCommentByOderNo(string orderNo)
        {
            return _orderRemarkRepository.GetByOrderNo(orderNo);
        }
        #endregion

    }
}