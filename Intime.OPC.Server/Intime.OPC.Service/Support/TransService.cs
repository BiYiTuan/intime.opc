using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service.Map;
using YintaiHZhouContext = Intime.OPC.Domain.Models.YintaiHZhouContext;
using System.Transactions;

namespace Intime.OPC.Service.Support
{
    public class TransService :  ITransService
    {
        private readonly IOrderRemarkRepository _orderRemarkRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ITransRepository _transRepository;
        private readonly IShippingSaleCommentRepository _shippingSaleCommentRepository;
        private readonly IShippingSaleRepository _shippingSaleRepository;
        private readonly IAccountService  _accountService;
        private readonly IOrderRepository _orderRepository;
        private readonly ISectionRepository _sectionRepository;


        public TransService(ITransRepository transRepository, 
            IOrderRemarkRepository orderRemarkRepository,
            ISaleRepository saleRepository,
            IShippingSaleRepository shippingSaleRepository,
            IAccountService accountService,
            IOrderRepository  orderRepository,
            IShippingSaleCommentRepository shippingSaleCommentRepository,
            ISectionRepository sectionRepository)
        {
            _transRepository = transRepository;
            _orderRemarkRepository = orderRemarkRepository;
            _shippingSaleRepository = shippingSaleRepository;
            _saleRepository = saleRepository;
            _shippingSaleCommentRepository = shippingSaleCommentRepository;
            _accountService = accountService;
            _orderRepository = orderRepository;
            _sectionRepository = sectionRepository;
        }

        #region ITransService Members

        public PageResult<OPC_Sale> Select(DateTime startDate, DateTime endDate, string orderNo, string saleOrderNo,int pageIndex,int pageSize=20)
        {
            var user = _accountService.GetByUserID(UserId);
            _transRepository.SetCurrentUser(user);
            return _transRepository.Select(startDate, endDate, orderNo, saleOrderNo, pageIndex, pageSize);
        }

      

        public IList<OPC_SaleDetail> SelectSaleDetail(IEnumerable<string> saleNos)
        {
            return _transRepository.SelectSaleDetail(saleNos);
        }

        public IList<OPC_OrderComment> GetRemarksByOrderNo(string orderNo)
        {
            return _orderRemarkRepository.GetByOrderNo(orderNo);
        }

        #endregion


        public bool AddOrderComment(OPC_OrderComment comment)
        {
           
           return   _orderRemarkRepository.Create(comment);
        }

        public ShippingSaleDto GetShippingSaleBySaleNo(string saleNo)
        {
            var entity = _shippingSaleRepository.GetBySaleOrderNo(saleNo);
            if (entity==null)
            {
                throw new ShippingSaleNotExistsException(saleNo);
            }
            return Mapper.Map<OPC_ShippingSale, ShippingSaleDto>(entity);
        }


        public bool AddShippingSaleComment(OPC_ShippingSaleComment comment)
        {
            return   _shippingSaleCommentRepository.Create(comment);
        }

        public IList<OPC_ShippingSaleComment> GetByShippingCommentCode(string shippingCode)
        {
            return _shippingSaleCommentRepository.GetByShippingCode(shippingCode);
        }

        public bool CreateShippingSale(int userId, ShippingSaleCreateDto shippingSaleDto)
        {
            var dt = DateTime.Now;
            var user = _accountService.GetByUserID(userId);
            _orderRepository.SetCurrentUser(user);

            _shippingSaleRepository.SetCurrentUser(user);
            _sectionRepository.SetCurrentUser(user);
            _saleRepository.SetCurrentUser(user);

            var order = _orderRepository.GetOrderByOrderNo(shippingSaleDto.OrderNo);
            foreach (var saleID in shippingSaleDto.SaleOrderIDs)
            {
                var saleOrder = _saleRepository.GetBySaleNo(saleID);
                Section section = null; ;
                if (saleOrder != null)
                    section = _sectionRepository.GetByID((int)saleOrder.SectionId);

                var sale = new OPC_ShippingSale();
                sale.CreateDate = dt;
                sale.CreateUser = userId;
                sale.UpdateDate = dt;
                sale.UpdateUser = userId;
                sale.OrderNo = shippingSaleDto.OrderNo;

                sale.ShipViaId = shippingSaleDto.ShipViaID;
                sale.ShippingCode = shippingSaleDto.ShippingCode;
                sale.ShippingFee = (decimal)(shippingSaleDto.ShippingFee);
                sale.ShippingStatus = EnumSaleOrderStatus.PrintInvoice.AsId();
                sale.ShipViaName = shippingSaleDto.ShipViaName;
                sale.ShippingAddress = order.ShippingAddress;
                sale.ShippingContactPerson = order.ShippingContactPerson;
                sale.ShippingContactPhone = order.ShippingContactPhone;
                if (section != null)
                    sale.StoreId = section.StoreId;

                //sale.BrandId = order.BrandId;


                //验证是否已经生成过发货单
                var lst = _shippingSaleRepository.GetBySaleOrderNo(saleID);

                if (lst != null)
                {
                    throw new ShippingSaleExistsException(shippingSaleDto.ShippingCode);
                }
                //验证发货单号 是否重复
                var e = _shippingSaleRepository.GetByShippingCode(shippingSaleDto.ShippingCode, 1, 1);
                if (e.TotalCount > 0)
                {
                    throw new ShippingSaleExistsException(shippingSaleDto.ShippingCode);
                }

                var pSale = _saleRepository.GetBySaleNo(saleID);
                pSale.ShippingCode = sale.ShippingCode;
                pSale.ShippingStatus = sale.ShippingStatus;
                _saleRepository.Update(pSale);

                var bl = _shippingSaleRepository.Create(sale);
                _saleRepository.UpdateSatus(saleID, EnumSaleOrderStatus.PrintExpress, userId);
            }

            return true;
        }

        public PageResult<ShippingSaleDto> GetShippingSale(string shippingCode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize = 20)
        {
            startTime = startTime.Date;
            endTime = endTime.Date.AddDays(1);
            var user = _accountService.GetByUserID(UserId);
            _shippingSaleRepository.SetCurrentUser(user);
            var lst=  _shippingSaleRepository.Get(shippingCode, startTime, endTime,EnumSaleOrderStatus.PrintExpress.AsId(),pageIndex,pageSize);
            return Mapper.Map<OPC_ShippingSale, ShippingSaleDto>(lst);
        }

        public PageResult<SaleDto> GetSaleByShippingSaleNo(string shippingSaleNo, int pageIndex, int pageSize = 20)
        {

            var user = _accountService.GetByUserID(UserId);
            _shippingSaleRepository.SetCurrentUser(user);
            var lst = _shippingSaleRepository.GetByShippingCode(shippingSaleNo,1,1);
            if (lst.TotalCount>0)
            {
                throw new ShippingSaleNotExistsException(shippingSaleNo);
            }
            //todo 修改销售单查询
            IList<OPC_Sale> lstSales= new List<OPC_Sale>();// lst.Result.Select(opcShippingSale => _saleRepository.GetBySaleNo(opcShippingSale.SaleOrderNo)).ToList();
            

            var lst2= Mapper.Map<OPC_Sale, SaleDto>(lstSales);
            return new PageResult<SaleDto>(lst2,lst.TotalCount);
        }

        public PageResult<SaleDto> GetSaleOrderPickup(string orderNo, string saleOrderNo, DateTime startDate, DateTime endDate,int userid,int pageIndex,int pageSize)
        {
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1);
             var user = _accountService.GetByUserID(userid);
            _saleRepository.SetCurrentUser(user);
             return _saleRepository.GetPickUped(saleOrderNo, orderNo, startDate, endDate, pageIndex, pageSize, user.StoreIds.ToArray());
           // return Mapper.Map<OPC_Sale, SaleDto>(lst);
        }

        public PageResult<ShippingSaleDto> GetShippingSale(string saleOrderNo, string expressNo, DateTime startGoodsOutDate, DateTime endGoodsOutDate,
            string outGoodsCode, int sectionId, int shippingStatus, string customerPhone, int brandId, int pageIndex,
            int pageSize)
        {
            startGoodsOutDate = startGoodsOutDate.Date;
            endGoodsOutDate = endGoodsOutDate.Date.AddDays(1);
            var user = _accountService.GetByUserID(UserId);
            _shippingSaleRepository.SetCurrentUser(user);

            var lst=  _shippingSaleRepository.GetShippingSale(saleOrderNo, expressNo, startGoodsOutDate, endGoodsOutDate,
                outGoodsCode, sectionId, shippingStatus, customerPhone, brandId, pageIndex, pageSize);

            return  Mapper.Map<OPC_ShippingSale, ShippingSaleDto>(lst);
        }

        public IList<ShippingSaleDto> GetShippingSale(string shippingCode, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }


        public IList<SaleDto> GetSaleByShippingSaleNo(string shippingSaleNo)
        {
            var user = _accountService.GetByUserID(UserId);
            _shippingSaleRepository.SetCurrentUser(user);
            var lst = _shippingSaleRepository.GetByShippingCode(shippingSaleNo, 1, 1);
            if (lst.TotalCount == 0)
            {
                throw new ShippingSaleNotExistsException(shippingSaleNo);
            }

            IList<OPC_Sale> lstSales = _saleRepository.GetByShippingCode(shippingSaleNo); // lst.Result.Select(opcShippingSale => _saleRepository.GetBySaleNo(opcShippingSale.SaleOrderNo)).ToList();

            return  Mapper.Map<OPC_Sale, SaleDto>(lstSales);
           
        }

        public int UserId
        {
            get; set; }
    }
}