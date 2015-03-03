using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service.Map;
using Mapper = AutoMapper.Mapper;

namespace Intime.OPC.Service.Support
{
    public class ShippingSaleService : BaseService<OPC_ShippingSale>, IShippingSaleService
    {
        private readonly IShippingSaleRepository _shippingSaleRepository;
        private readonly IOrderRepository _orderRepository;
        private ISaleRMARepository _saleRmaRepository;
        private IAccountService _accountService;
        public ShippingSaleService(IShippingSaleRepository repository, IOrderRepository orderRepository, ISaleRMARepository saleRmaRepository, IAccountService accountService)
            : base(repository)
        {
            _shippingSaleRepository = repository;
            _orderRepository = orderRepository;
            _saleRmaRepository = saleRmaRepository;
            _accountService = accountService;
        }

        public PageResult<OPC_ShippingSale> GetByShippingCode(string shippingCode, int pageIndex, int pageSize = 20)
        {
            return _shippingSaleRepository.GetByShippingCode(shippingCode, pageIndex, pageSize);
        }

        public void Shipped(string saleOrderNo, int userID)
        {
            var lst = _shippingSaleRepository.GetBySaleOrderNo(saleOrderNo);

            if (lst == null)
            {
                throw new ShippingSaleNotExistsException(saleOrderNo);
            }
            lst.ShippingStatus = EnumSaleOrderStatus.Shipped.AsId();
            lst.UpdateDate = DateTime.Now;
            lst.UpdateUser = userID;

            _shippingSaleRepository.Update(lst);


        }

        public void PrintExpress(string orderNo, int userId)
        {
            //todo  确定是销售单还是订单
            var lst = _shippingSaleRepository.GetBySaleOrderNo(orderNo);
            if (lst == null)
            {
                throw new ShippingSaleNotExistsException(orderNo);
            }
            lst.ShippingStatus = EnumSaleOrderStatus.PrintExpress.AsId();
            lst.UpdateDate = DateTime.Now;
            lst.UpdateUser = userId;

            _shippingSaleRepository.Update(lst);

        }

        public void CreateRmaShipping(string rmaNo, int userId)
        {
            var dt = DateTime.Now;
            var saleRma = _saleRmaRepository.GetByRmaNo(rmaNo);
            var order = _orderRepository.GetOrderByOrderNo(saleRma.OrderNo);

            var sale = new OPC_ShippingSale();
            sale.RmaNo = rmaNo;
            sale.CreateDate = dt;
            sale.CreateUser = userId;
            sale.UpdateDate = dt;
            sale.UpdateUser = userId;
            sale.OrderNo = saleRma.OrderNo;

            sale.ShippingStatus = EnumRmaShippingStatus.NoPrint.AsId();
            sale.ShipViaName = "";
            sale.BrandId = order.BrandId;
            sale.ShippingAddress = order.ShippingAddress;
            sale.ShippingContactPerson = order.ShippingContactPerson;
            sale.ShippingContactPhone = order.ShippingContactPhone;
            sale.StoreId = order.StoreId;


            var bl = _shippingSaleRepository.Create(sale);

        }

        public void UpdateRmaShipping(RmaExpressSaveDto request)
        {
            var shipping = _shippingSaleRepository.GetByRmaNo(request.RmaNo);
            if (shipping == null)
            {
                throw new OpcException(string.Format("快递单不存在,快递单号:{0}", request.ShippingCode));
            }

            shipping.ShipViaId = request.ShipViaID;
            shipping.ShipViaName = request.ShipViaName;
            shipping.ShippingFee = (decimal)(request.ShippingFee);
            shipping.ShippingCode = request.ShippingCode;
            _shippingSaleRepository.Update(shipping);

        }

        public void PintRmaShippingOver(string shippingCode)
        {
            var shipping = _shippingSaleRepository.GetByShippingCode(shippingCode, 1, 100).Result.FirstOrDefault();
            if (shipping == null)
            {
                throw new Exception(string.Format("快递单不存在,快递单号:{0}", shippingCode));
            }

            if (shipping.ShippingStatus == EnumRmaShippingStatus.NoPrint.AsId())
            {
                shipping.ShippingStatus = EnumRmaShippingStatus.Printed.AsId();
                shipping.UpdateDate = DateTime.Now;
                shipping.UpdateUser = UserId;
                _shippingSaleRepository.Update(shipping);
            }

        }

        public void PintRmaShipping(string shippingCode)
        {
            var shipping = _shippingSaleRepository.GetByShippingCode(shippingCode, 1, 100).Result.FirstOrDefault();
            if (shipping == null)
            {
                throw new Exception(string.Format("快递单不存在,快递单号:{0}", shippingCode));
            }
            shipping.PrintTimes++;
            _shippingSaleRepository.Update(shipping);
            //if (shipping.ShippingStatus == EnumRmaShippingStatus.NoPrint.AsID() || shipping.ShippingStatus == EnumRmaShippingStatus.Printed.AsID() 
            //    )
            //{
            //    shipping.ShippingStatus = EnumRmaShippingStatus.Printed.AsID();
            //    shipping.UpdateDate = DateTime.Now;
            //    shipping.UpdateUser = UserId;
            //    _shippingSaleRepository.Update(shipping);
            //}


        }

        public PageResult<ShippingSaleDto> GetRmaByPackPrintPress(RmaExpressRequest request)
        {
            request.FormateDate();
            _shippingSaleRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _shippingSaleRepository.GetByOrderNo(request.OrderNo, request.StartDate, request.EndDate, request.pageIndex,
                  request.pageSize, EnumRmaShippingStatus.NoPrint.AsId());

            IList<ShippingSaleDto> lstDtos = new List<ShippingSaleDto>();
            foreach (var shippingSale in lst.Result)
            {
                var o = AutoMapper.Mapper.Map<OPC_ShippingSale, ShippingSaleDto>(shippingSale);
                EnumRmaShippingStatus rmaShippingStatus = (EnumRmaShippingStatus)shippingSale.ShippingStatus;
                o.ShippingStatus = rmaShippingStatus.GetDescription();
                lstDtos.Add(o);
            }
            return new PageResult<ShippingSaleDto>(lstDtos, lst.TotalCount);


        }

        public PageResult<ShippingSaleDto> GetRmaShippingPrintedByPack(RmaExpressRequest request)
        {
            request.FormateDate();
            _shippingSaleRepository.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = _shippingSaleRepository.GetByOrderNo(request.OrderNo, request.StartDate, request.EndDate, request.pageIndex,
                 request.pageSize, EnumRmaShippingStatus.Printed.AsId());

            IList<ShippingSaleDto> lstDtos = new List<ShippingSaleDto>();
            foreach (var shippingSale in lst.Result)
            {
                var o = AutoMapper.Mapper.Map<OPC_ShippingSale, ShippingSaleDto>(shippingSale);
                EnumRmaShippingStatus rmaShippingStatus = (EnumRmaShippingStatus)shippingSale.ShippingStatus;
                o.PrintStatus = rmaShippingStatus.GetDescription();
                lstDtos.Add(o);
            }
            return new PageResult<ShippingSaleDto>(lstDtos, lst.TotalCount);
        }

        public void PintRmaShippingOverConnect(string shippingCode)
        {
            var shipping = _shippingSaleRepository.GetByShippingCode(shippingCode, 1, 100).Result.FirstOrDefault();
            if (shipping == null)
            {
                throw new Exception(string.Format("快递单不存在,快递单号:{0}", shippingCode));
            }
            if (shipping.ShippingStatus == EnumRmaShippingStatus.Printed.AsId())
            {
                shipping.ShippingStatus = EnumRmaShippingStatus.PrintOver.AsId();
                shipping.UpdateDate = DateTime.Now;
                shipping.UpdateUser = UserId;
                _shippingSaleRepository.Update(shipping);
            }
        }

        public PageResult<ShippingSaleDto> GetPagedList(GetShippingSaleOrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var filter = Mapper.Map<GetShippingSaleOrderRequest, ShippingOrderFilter>(request);
            int total;
            var order = request.SortOrder == null
                            ? ShippingOrderSortOrder.Default
                            : (ShippingOrderSortOrder)request.SortOrder;

            if (request.StoreId != null || request.DataRoleStores != null || request.StoreIds != null)
            {
                if (filter.StoreIds == null)
                {
                    filter.StoreIds = new List<int>();
                }
            }


            if (request.StoreId != null)
            {
                filter.StoreIds.Add(request.StoreId.Value);
            }

            if (request.DataRoleStores != null)
            {
                filter.StoreIds.AddRange(request.DataRoleStores);
            }

            if (request.StoreIds != null)
            {
                filter.StoreIds.AddRange(request.StoreIds);
            }

            var datas = _shippingSaleRepository.GetPagedList(request.PagerRequest, out total, filter, order);

            var dto = Mapper.Map<List<ShippingOrderModel>, List<ShippingSaleDto>>(datas);

            var pageResult = new PageResult<ShippingSaleDto>(dto, total);

            return pageResult;
        }
    }
}
