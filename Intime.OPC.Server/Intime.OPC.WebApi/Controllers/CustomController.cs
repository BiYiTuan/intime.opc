using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Controllers
{
    public class CustomController : BaseController
    {
        private readonly IRmaService _rmaService;
        private readonly ISaleRMAService _saleRmaService;
        private readonly IShippingSaleService _shippingSaleService;
        private IOrderService _orderService;
        private IAccountService _accountService;
        public CustomController(IOrderService orderService, ISaleRMAService saleRmaService, IRmaService rmaService,
            IShippingSaleService shippingSaleService, IAccountService accountService)
        {
            _orderService = orderService;
            _saleRmaService = saleRmaService;
            _rmaService = rmaService;
            _shippingSaleService = shippingSaleService;
            _accountService = accountService;
        }

        [NonAction]
        private IHttpActionResult GetList4ShippingPageResult(GetShippingSaleOrderRequest request, UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var pageResult = _shippingSaleService.GetPagedList(request);

            return RetrunHttpActionResult(pageResult);
        }


        [NonAction]
        private IHttpActionResult GetPagedList4RmaPagedInfo(RmaQueryRequest request, UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }
            var dto = _rmaService.GetPagedList(request);

            return RetrunHttpActionResult(dto);
        }

        [NonAction]
        private IHttpActionResult GetPagedList4RmaPagedResult(RmaQueryRequest request, UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var dto = _rmaService.GetPagedList(request);

            var restResult = new PageResult<RMADto>(dto.Datas, dto.TotalCount);

            return RetrunHttpActionResult(restResult);
        }

        /// <summary>
        /// 客服退货
        /// </summary>
        /// <param name="request"></param>
        /// <param name="uid"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetOrder([FromUri] ReturnGoodsRequest request, [UserId] int uid, [UserProfile] UserProfile userProfile)
        {
            //var queryRequest = AutoMapper.Mapper.Map<ReturnGoodsRequest, RmaQueryRequest>(request);

            //return GetPagedList4RmaPagedResult(queryRequest, userProfile);
            //注意 关联 RMA 时 是LEFT JOIN
            return DoFunction(() => _saleRmaService.GetByReturnGoods(request, uid), "查询订单失败");
        }

        /// <summary>
        ///     客服同意退货
        /// </summary>
        /// <param name="rmaNos">The rma nos.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult AgreeReturnGoods([FromBody] IEnumerable<string> rmaNos)
        {
            return DoAction(() =>
            {
                foreach (string rmaNo in rmaNos)
                {
                    _saleRmaService.AgreeReturnGoods(rmaNo);
                }
            }, "查询订单失败");
        }

        /// <summary>
        ///     物流确认收货
        /// </summary>
        /// <param name="rmaNos">The rma nos.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult ShippingReceiveGoods([FromBody] IEnumerable<string> rmaNos)
        {
            return DoAction(() =>
            {
                foreach (string rmaNo in rmaNos)
                {
                    _saleRmaService.ShippingReceiveGoods(rmaNo);
                }
            }, "查询订单失败");
        }

        #region 退货包裹审核

        /// <summary>
        ///     查询退货单信息
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetRmaPackVerifyByPack([FromUri] PackageReceiveRequest request, [UserId] int uid, [UserProfile]UserProfile userProfile)
        {
            return DoFunction(() =>
            {
                _rmaService.UserId = uid;
                return _rmaService.GetAllPackVerify(request);
            }, "查询退货单信息失败");

            //left
            //var rmarequest = Mapper.Map<PackageReceiveRequest, RmaQueryRequest>(request);
            //rmarequest.Status = EnumRMAStatus.ShipReceive.AsId();
            //rmarequest.ReturnGoodsStatus = EnumReturnGoodsStatus.NoProcess;

            //return GetPagedList4RmaPagedResult(rmarequest, userProfile);

        }

        /// <summary>
        ///     退货包裹审核
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult PackageVerify([FromBody] PackageVerifyRequest request)
        {
            return DoAction(() =>
            {
                foreach (string rmaNo in request.RmaNos)
                {
                    _saleRmaService.PackageVerify(rmaNo, request.Pass);
                    //_shippingSaleService.CreateRmaShipping(rmaNo, UserID);
                }
            }, "查询退货单信息失败");
        }

        #endregion

        #region 包裹退回-打印快递单

        [HttpPost]
        public IHttpActionResult GetRmaByPackPrintPress(string rmaNo, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _rmaService.UserId = uid;
                return _rmaService.GetByRmaNo(rmaNo);
            }, "查询退货单信息失败");
        }

        /// <summary>
        ///     包裹退回-打印快递单 设定快递公司
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult UpdateShipRmaVia([FromBody] RmaExpressSaveDto request)
        {
            return DoAction(() => { _shippingSaleService.UpdateRmaShipping(request); }, "查询退货单信息失败");
        }

        /// <summary>
        /// 打印快递单
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult PintRmaShipping([FromBody] IEnumerable<string> shippingCodes)
        {
            return DoAction(() =>
            {
                foreach (var shippingCode in shippingCodes)
                    _shippingSaleService.PintRmaShipping(shippingCode);
            });
        }

        /// <summary>
        /// 打印完成
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <returns>IHttpActionResult.</returns>PintRmaShippingOver
        [HttpPost]
        public IHttpActionResult PintRmaShippingOver([FromBody] IEnumerable<string> shippingCodes)
        {
            return DoAction(() =>
            {
                foreach (var shippingCode in shippingCodes)
                    _shippingSaleService.PintRmaShippingOver(shippingCode);
            });
        }

        [HttpPost]
        public IHttpActionResult GetRmaShippingByPackPrintPress([FromUri] RmaExpressRequest request, [UserId] int uid, [UserProfile]UserProfile userProfile)
        {
            //return DoFunction(() =>
            //{
            //    _shippingSaleService.UserId = uid;
            //    return _shippingSaleService.GetRmaByPackPrintPress(request);
            //}, "查询退货单信息失败");

            var req = Mapper.Map<RmaExpressRequest, GetShippingSaleOrderRequest>(request);

            req.Status = EnumRmaShippingStatus.NoPrint.AsId();

            return GetList4ShippingPageResult(req, userProfile);
        }
        #endregion

        #region 包裹退回-快递交接
        /// <summary>
        /// 查询快递
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetRmaShippingPrintedByPack([FromUri] RmaExpressRequest request, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _shippingSaleService.UserId = uid;
                return _shippingSaleService.GetRmaShippingPrintedByPack(request);
            }, "查询退货单信息失败");
        }

        /// <summary>
        /// 打印完成快递交接
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult PintRmaShippingOverConnect([FromBody] IEnumerable<string> shippingCodes)
        {
            return DoAction(() =>
        {
            foreach (var shippingCode in shippingCodes)
                _shippingSaleService.PintRmaShippingOverConnect(shippingCode);
        });
        }
        #endregion


        #region 导购退货收货查询
        /// <summary>
        /// 查询退货信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetRmaByShoppingGuide([FromUri] ShoppingGuideRequest request, [UserProfile] UserProfile userProfile)
        {
            return DoFunction(() =>
            {
                _rmaService.UserId = userProfile.Id;
                return _rmaService.GetRmaByShoppingGuide(request);
            }, "查询退货单信息失败");

            //var queryRequest = AutoMapper.Mapper.Map<ShoppingGuideRequest, RmaQueryRequest>(request);

            //return GetPagedList4RmaPagedResult(queryRequest, userProfile);
        }

        #endregion




        #region 已完成退货单查询
        /// <summary>
        ///已完成退货单查询
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetRmaByAllOver([FromUri] ShoppingGuideRequest request, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _rmaService.UserId = uid;
                return _rmaService.GetRmaByAllOver(request);
            }, "查询退货单信息失败");
        }

        #endregion

        #region  网络自助退货
        [HttpPost]
        public IHttpActionResult GetOrderAutoBack([FromUri] ReturnGoodsRequest request, [UserProfile] UserProfile userProfile)
        {


            var queryRequest = AutoMapper.Mapper.Map<ReturnGoodsRequest, RmaQueryRequest>(request);
            //start end 是购买时间
            queryRequest.BuyStartDate = queryRequest.StartDate;
            queryRequest.BuyEndDate = queryRequest.EndDate;

            queryRequest.StartDate = null;
            queryRequest.EndDate = null;




            return GetPagedList4RmaPagedResult(queryRequest, userProfile);

            //return DoFunction(() => _saleRmaService.GetOrderAutoBack(request), "查询订单失败");
        }
        [HttpPost]
        public IHttpActionResult GetOrderItemsByOrderNoAutoBack(string orderNo, int pageIndex, int pageSize)
        {
            //todo 查询订单明细 未实现
            return DoFunction(() =>
            {
                return _orderService.GetOrderItemsAutoBack(orderNo, pageIndex, pageSize);
            }, "读取订单明细失败");

        }


        #endregion
    }


}