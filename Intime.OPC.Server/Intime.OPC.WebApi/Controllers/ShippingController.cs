using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.Filters;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/deliveryorder")]
    public class ShippingController : BaseController
    {
        private readonly IShippingSaleRepository _shippingOrderRepository;
        private readonly ISaleRepository _saleOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShipViaRepository _shipViaRepository;
        private readonly ISectionRepository _sectionRepository;

        public ShippingController(IShippingSaleRepository shippingOrderRepository, ISaleRepository saleOrderRepository, IOrderRepository orderRepository, IShipViaRepository shipViaRepository, ISectionRepository sectionRepository)
        {
            _shippingOrderRepository = shippingOrderRepository;
            _saleOrderRepository = saleOrderRepository;
            _orderRepository = orderRepository;
            _shipViaRepository = shipViaRepository;
            _sectionRepository = sectionRepository;
        }

        private IHttpActionResult Check(int shippingId, UserProfile userProfile)
        {
            if (userProfile.StoreIds != null && !userProfile.StoreIds.Any())
            {
                return BadRequest("您请先维护门店信息后，再查询");
            }

            var model = _shippingOrderRepository.GetByID(shippingId);

            if (model == null)
            {
                return NotFound();
            }

            if (userProfile.StoreIds != null && !userProfile.StoreIds.Contains(model.StoreId ?? 0))
            {
                return BadRequest("您请先维护门店信息后，再查询");
            }

            return null;
        }

        /// <summary>
        /// 生成发货单
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult PostOrder([FromBody]CreateShippingSaleOrderRequest request, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            //不同的销售单可以生成同一个出库单
            if (!ModelState.IsValid || request == null || request.SalesOrderNos == null || request.SalesOrderNos.Count == 0)
            {
                return BadRequest("参数错误");
            }

            //验证用户合法性
            if (userProfile.StoreIds != null && !userProfile.StoreIds.Any())
            {
                return BadRequest("您没有门店信息，请先维护门店信息");
            }

            var stores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            var saleList = _saleOrderRepository.GetListByNos(request.SalesOrderNos, new SaleOrderFilter
            {
                DataRoleStores = stores,
                // HasDeliveryOrderGenerated = false,
                Status = (int)EnumSaleOrderStatus.ShipInStorage

            });
            if (saleList.Count != request.SalesOrderNos.Count)
            {
                return BadRequest("获取销售单信息不一致，请重新选择销售单");
            }

            var s = saleList[0];

            //目前规则只能 单订单 1对1 出货单
            foreach (var item in saleList)
            {
                if (s.OrderNo != item.OrderNo)
                {
                    return BadRequest(String.Format("订单ID的不一致，请确认。订单号：{0} vs {1}", s.OrderNo, item.OrderNo));
                }
            }

            var order = _orderRepository.GetOrderByOrderNo(s.OrderNo);
            var section = _sectionRepository.GetByID(s.SectionId ?? 0);

            var entity = new OPC_ShippingSale
            {
                CreateDate = DateTime.Now,
                CreateUser = userId,
                UpdateDate = DateTime.Now,
                UpdateUser = userId,
                OrderNo = order.OrderNo,
                PrintTimes = 0,
                RmaNo = String.Empty,
                ShipViaId = 0,
                ShipViaName = String.Empty,
                ShippingAddress = order.ShippingAddress,
                ShippingCode = String.Empty,
                ShippingContactPerson = order.ShippingContactPerson,
                ShippingContactPhone = order.ShippingContactPhone,
                ShippingFee = 0m,
                ShippingRemark = String.Empty,
                ShippingStatus = (int)EnumSaleOrderStatus.PrintInvoice,
                ShippingZipCode = order.ShippingZipCode,
                StoreId = section == null ? order.StoreId : section.StoreId,//拿销售单storeid
            };

            var model = _shippingOrderRepository.CreateBySaleOrder(entity, saleList, userId, String.Empty);

            var dto = Mapper.Map<ShippingOrderModel, ShippingSaleDto>(model);

            return RetrunHttpActionResult(dto);
        }

        /// <summary>
        /// 获取出库单
        /// </summary>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            var checkRst = Check(id, userProfile);

            if (checkRst != null)
            {
                return checkRst;
            }

            var model = _shippingOrderRepository.GetItemModel(id);

            var dto = Mapper.Map<ShippingOrderModel, ShippingSaleDto>(model);

            return RetrunHttpActionResult(dto);
        }

        /// <summary>
        /// 获取出库单
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri]GetShippingSaleOrderRequest request, [UserId]int userId, [UserProfile] UserProfile userProfile)
        {
            var filter = Mapper.Map<GetShippingSaleOrderRequest, ShippingOrderFilter>(request);
            var pagerRequest = new Domain.PagerRequest(request.Page ?? 1, request.PageSize ?? 10);

            request.ArrangeParams();
            if (request.EndDate != null || request.StartDate != null)
            {
                var daterange = new DateRangeFilter { EndDateTime = request.EndDate, StartDateTime = request.StartDate };

                filter.DateRange = daterange;
            }

            int total;

            filter.StoreIds = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();

            //按创建日期倒序
            var datas = _shippingOrderRepository.GetPagedList(pagerRequest, out total, filter,
                (ShippingOrderSortOrder)(request.SortOrder ?? 0));

            var dto = Mapper.Map<List<ShippingOrderModel>, List<ShippingSaleDto>>(datas);

            var pagerdto = new PagerInfo<ShippingSaleDto>(pagerRequest, total) { Datas = dto };

            return RetrunHttpActionResult(pagerdto);
        }

        /// <summary>
        /// 添加 物流信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] PutShippingSaleOrderRequest request, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var checkRst = Check(id, userProfile);

            if (checkRst != null)
            {
                return checkRst;
            }

            var item = _shippingOrderRepository.GetByID(id);

            if (item == null)
            {
                return NotFound();
            }


            var shipVia = _shipViaRepository.GetByID(request.ShipViaId ?? 0);

            item.ShipViaId = request.ShipViaId;
            item.ShippingFee = request.ShippingFee;
            item.ShippingCode = request.ShippingNo;
            item.ShipViaName = shipVia == null ? String.Empty : shipVia.Name;
            item.ShippingStatus = (int)Domain.Enums.EnumSaleOrderStatus.PrintExpress;
            item.UpdateDate = DateTime.Now;
            item.UpdateUser = userId;

            _shippingOrderRepository.Update4ShippingCode(item, userId);


            return RetrunHttpActionResult("OK");
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">1为 出库单  2为快递单</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("{id:int}/print")]
        [HttpPut]
        public IHttpActionResult PutPrint(int id, [FromBody] DeliveryOrderPrintRequest request, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            if (request == null)
            {
                return BadRequest("参数请求出错");
            }

            var model = _shippingOrderRepository.GetItemModel(id);

            if (model == null)
            {
                return NotFound();
            }

            _shippingOrderRepository.Update4Print(model, request, userId);

            return RetrunHttpActionResult("OK");
        }


        /// <summary>
        ///  完成物流交接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [Route("{id:int}/finish")]
        [HttpPut]
        public IHttpActionResult PutFinish(int id, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            var model = _shippingOrderRepository.GetItemModel(id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.ShippingStatus < (int)EnumSaleOrderStatus.Shipped)
            {
                model.ShippingStatus = (int)EnumSaleOrderStatus.Shipped;

                _shippingOrderRepository.Sync4Status(model, userId);
            }
            else
            {
                return BadRequest(String.Format("当前快递单状态{0}({1}),不能处理状态为{2}({3})", model.ShippingStatus, model.ShippingStatus == null ? String.Empty : ((EnumSaleOrderStatus)model.ShippingStatus).GetDescription(), (int)EnumSaleOrderStatus.Shipped, EnumSaleOrderStatus.Shipped.GetDescription()));
            }

            return RetrunHttpActionResult("OK");
        }
    }
}
