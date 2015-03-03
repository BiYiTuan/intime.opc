using System.Activities.Expressions;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Intime.OPC.Service.Support
{
    public class SaleRMAService : BaseService<OPC_SaleRMA>, ISaleRMAService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IRmaDetailRepository _rmaDetailRepository;
        private readonly IRMARepository _rmaRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ISectionRepository _sectionRepository;
        private ISaleRmaCommentRepository _saleRmaCommentRepository;
        private static readonly int[] InvalidRmaStatus = new[] { -10, 13, 15 };

        public SaleRMAService(ISaleRMARepository saleRmaRepository, ISaleDetailRepository saleDetailRepository,
            ISaleRepository saleRepository, IOrderItemRepository orderItemRepository,
            IRmaDetailRepository rmaDetailRepository, ISectionRepository sectionRepository, IRMARepository rmaRepository, ISaleRmaCommentRepository saleRmaCommentRepository, IAccountService accountService)
            : base(saleRmaRepository)
        {
            _saleDetailRepository = saleDetailRepository;
            _saleRepository = saleRepository;
            _orderItemRepository = orderItemRepository;
            _rmaDetailRepository = rmaDetailRepository;
            _sectionRepository = sectionRepository;
            _rmaRepository = rmaRepository;
            _saleRmaCommentRepository = saleRmaCommentRepository;
        }

        #region ISaleRMAService Members

        public void CreateSaleRMA(int userId, RMARequest rma)
        {
            CreateSaleRmaSub(userId, rma, "客服退货");
        }

        private void CreateSaleRmaSub(int userId, RMARequest rma, string saleSource)
        {
            if (string.Empty == rma.Remark)
            {
                throw new RmaException(string.Format("没有退货备注,订单明细号:{0}", rma.OrderNo));
            }

            List<OPC_SaleDetail> saleDetails =
                _saleDetailRepository.GetByOrderNo(rma.OrderNo, 1, 1000).Result.Where(t => !t.BackNumber.HasValue || t.BackNumber.Value < t.SaleCount).OrderByDescending(t => t.SaleCount).ToList();
            List<OPC_Sale> sales =
                _saleRepository.GetByOrderNo(rma.OrderNo, -1).OrderByDescending(t => t.SalesCount).ToList();
            IList<OrderItem> orderItems =
                _orderItemRepository.GetByIDs(rma.ReturnProducts.Select(t => t.Key));
            using (var db = new YintaiHZhouContext())
            using (var ts = new TransactionScope())
            {
                IList<RmaConfig> rmaInfos = new List<RmaConfig>();
                foreach (var kv in rma.ReturnProducts)
                {
                    var orderItemId = kv.Key;
                    var rmaCount = kv.Value;
                    var item = db.Set<OrderItem>().FirstOrDefault(x => x.Id == orderItemId);
                    if (item == null)
                    {
                        throw new RmaException(string.Format("无效的商品明细记录({0})或商品ID({1})", kv.Key, kv.Value));
                    }

                    var items = from i in db.Set<RMAItem>()
                                from r in db.Set<RMA>()
                                from o in db.Set<Order>()
                                where
                                    i.RMANo == r.RMANo &&
                                    r.OrderNo == o.OrderNo &&
                                    i.ProductId == item.ProductId &&
                                    o.OrderNo == rma.OrderNo &&
                                    !InvalidRmaStatus.Contains(r.Status)

                                select i;

                    int countOfItems = items.Any() ? items.Sum(i=>i.Quantity) : 0;

                    if (item.Quantity - countOfItems < rmaCount)
                    {
                        throw new RmaException("退货数量大于可退货数量");
                    }

                    OrderItem oItem = orderItems.FirstOrDefault(t => t.Id == kv.Key);

                    List<OPC_SaleDetail> details =
                        saleDetails.Where(t => t.OrderItemId == kv.Key).OrderByDescending(t => t.SaleCount).ToList();
                    int returnCount = kv.Value;
                    OPC_SaleDetail detail = details.FirstOrDefault();
                    if (detail == null)
                    {
                        //没有销售明细
                        throw new RmaException(string.Format("没有销售明细不存在,订单明细号:{0}", kv.Key));
                    }

                    while (detail != null && returnCount > CalculateRmaCount(detail))
                    {
                        RmaConfig opcRma = rmaInfos.FirstOrDefault(t => t.SaleOrderNo == detail.SaleOrderNo);
                        if (opcRma == null)
                        {
                            opcRma = new RmaConfig(userId)
                            {
                                SaleRmaSource = saleSource,
                                RmaNo = CreateRmaNo(),
                                SaleOrderNo = detail.SaleOrderNo,
                                RefundAmount = rma.RealRMASumMoney,
                                StoreFee = rma.StoreFee,
                                CustomFee = rma.CustomFee
                            };
                            rma.RealRMASumMoney = 0;
                            rma.StoreFee = 0;
                            rma.CustomFee = 0;

                            opcRma.OpcSale = sales.FirstOrDefault(t => t.SaleOrderNo == opcRma.SaleOrderNo);
                            opcRma.StoreID = _sectionRepository.GetByID(opcRma.OpcSale.SectionId.Value).StoreId.Value;
                            rmaInfos.Add(opcRma);
                        }

                        int countCanRMA = CalculateRmaCount(detail);

                        var opcRmaDetail = new SubRmaConfig
                        {
                            OpcSaleDetail = detail,
                            OrderItem = oItem,
                            OrderDetailId = kv.Key,
                            ReturnCount = countCanRMA
                        };
                        opcRma.Details.Add(opcRmaDetail);
                        rmaInfos.Add(opcRma);

                        //更新退货数量
                        UpdateSaleDetailOfBackNum(detail, countCanRMA, db);

                        returnCount = returnCount - countCanRMA;
                        details.Remove(detail);
                        detail = details.FirstOrDefault();
                    }

                    if (returnCount > 0)
                    {
                        RmaConfig config = rmaInfos.FirstOrDefault(t => t.SaleOrderNo == detail.SaleOrderNo);
                        if (config == null)
                        {
                            config = new RmaConfig(userId)
                            {
                                SaleRmaSource = saleSource,
                                Reason = rma.Remark,
                                SaleOrderNo = detail.SaleOrderNo,
                                RefundAmount = rma.RealRMASumMoney,
                                StoreFee = rma.StoreFee,
                                CustomFee = rma.CustomFee
                            };

                            config.OpcSale = sales.FirstOrDefault(t => t.SaleOrderNo == config.SaleOrderNo);
                            config.StoreID = _sectionRepository.GetByID(config.OpcSale.SectionId.Value).StoreId.Value;
                            config.RmaNo = CreateRmaNo();
                            rmaInfos.Add(config);
                        }
                        var subConfig = new SubRmaConfig
                        {
                            OpcSaleDetail = detail,
                            OrderItem = oItem,
                            OrderDetailId = kv.Key,
                            ReturnCount = returnCount
                        };
                        config.Details.Add(subConfig);
                        //更新退货数量
                        UpdateSaleDetailOfBackNum(detail, returnCount, db);
                    }
                }
                Save(rmaInfos);
                ts.Complete();
            }
        }

        /// <summary>
        /// 计算销售单明细可以退货的数量
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        private int CalculateRmaCount(OPC_SaleDetail detail)
        {
            return detail.SaleCount - (detail.BackNumber.HasValue ? detail.BackNumber.Value : 0);
        }

        private void UpdateSaleDetailOfBackNum(OPC_SaleDetail saleDetail, int backCount, YintaiHZhouContext db)
        {

            OPC_SaleDetail sd = db.OPC_SaleDetails.FirstOrDefault(x => x.Id == saleDetail.Id);
            if (sd == null)
            {
                throw new RmaException("不存在的销售单明细");
            }
            sd.BackNumber = (sd.BackNumber.HasValue ? sd.BackNumber.Value : 0) + backCount;
            db.SaveChanges();

        }
        public PageResult<SaleRmaDto> GetByReturnGoodsInfo(ReturnGoodsInfoRequest request)
        {
            ISaleRMARepository rep = _repository as ISaleRMARepository;

            return rep.GetAll(request.OrderNo, request.SaleOrderNo, request.PayType, request.RmaNo,
                 request.StartDate, request.EndDate, request.RmaStatus, request.StoreID, EnumReturnGoodsStatus.NoProcess, request.pageIndex, request.pageSize);
        }

        public PageResult<SaleRmaDto> GetByReturnGoods(ReturnGoodsRequest request, int userId)
        {

            ISaleRMARepository rep = _repository as ISaleRMARepository;
            request.StartDate = request.StartDate.Date;
            request.EndDate = request.EndDate.Date.AddDays(1);
            var lst = rep.GetAll(request.OrderNo, request.PayType, request.BandId, request.StartDate, request.EndDate,
                request.Telephone, request.pageIndex, request.pageSize);

            return lst;
        }

        public void AddComment(OPC_SaleRMAComment comment)
        {
            _saleRmaCommentRepository.Create(comment);
        }

        public IList<OPC_SaleRMAComment> GetCommentByRmaNo(string rmaNo)
        {

            return _saleRmaCommentRepository.GetByRmaID(rmaNo);
        }

        public PageResult<SaleRmaDto> GetByPack(PackageReceiveRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);

            var rep = _repository as ISaleRMARepository;
            var lst = rep.GetAll(dto.OrderNo, dto.SaleOrderNo, "", "", dto.StartDate, dto.EndDate,
                EnumRMAStatus.ShipNoReceive.AsId(), null, EnumReturnGoodsStatus.NoProcess, dto.pageIndex, dto.pageSize);

            return lst;
        }

        #endregion


        /// <summary>
        /// 客服同意退货
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        public void AgreeReturnGoods(string rmaNo)
        {
            var rep = (ISaleRMARepository)_repository;
            var saleRma = rep.GetByRmaNo(rmaNo);
            if (saleRma == null)
            {
                throw new OpcException("快递单不存在,退货单号:" + rmaNo);
            }
            if (saleRma.RMAStatus != EnumReturnGoodsStatus.NoProcess.AsId())
            {
                throw new OpcException("快递单已经确认过，退货单号:" + rmaNo);
            }
            if (saleRma.Status > EnumRMAStatus.NoDelivery.AsId())
            {
                throw new OpcException("快递单已经确认过，退货单号:" + rmaNo);
            }
            saleRma.RMAStatus = EnumReturnGoodsStatus.ServiceApprove.AsId();
            saleRma.ServiceAgreeTime = DateTime.Now;
            rep.Update(saleRma);
        }

        public void ShippingReceiveGoods(string rmaNo)
        {
            var saleRma = _rmaRepository.GetByRmaNo2(rmaNo);
            if (saleRma == null)
            {
                throw new OpcException("快递单不存在,退货单号:" + rmaNo);
            }
            if (saleRma.RMAStatus < (int)EnumReturnGoodsStatus.NoProcess)
            {
                throw new OpcException("客服未确认,退货单号:" + rmaNo);
            }
            if (saleRma.Status > (int)EnumRMAStatus.ShipNoReceive)
            {
                throw new OpcException("该退货单已经确认或正在审核,退货单号:" + rmaNo);
            }

            saleRma.Status = EnumRMAStatus.ShipReceive.AsId();
            _rmaRepository.Update(saleRma);
        }

        public PageResult<SaleRmaDto> GetByReturnGoodPay(ReturnGoodsPayRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            /*
             
             财务退款确认的查询状态条件，就要改为退货状态为已生效，且退货单状态为物流入库的
             zxy 物流入库+通知单品 2014-5-10
             */

            ISaleRMARepository rep = _repository as ISaleRMARepository;
            var lst = rep.GetByReturnGoodPay(dto.OrderNo, "", dto.PayType, "", dto.StartDate, dto.EndDate,
                 null, dto.pageIndex, dto.pageSize);


            return lst;
        }

        /// <summary>
        /// 财务确认
        /// </summary>
        /// <param name="rmaNo"></param>
        /// <param name="money"></param>
        public void CompensateVerify(string rmaNo, decimal money)
        {
            using (var db = new YintaiHZhouContext())
            {
                using (var ts = new TransactionScope())
                {
                    var saleRma = db.OPC_RMAs.FirstOrDefault(o => o.RMANo == rmaNo);
                    if (saleRma == null)
                    {
                        throw new RmaException(string.Format("无效的退货单号:{0}",rmaNo));
                    }

                    if (saleRma.Status == EnumRMAStatus.PrintRMA.AsId() && saleRma.SaleOrderNo.Substring(3, 1) != "-")
                    {
                        throw new RmaException("非自拍商品需要导购确认收货才能付款确认,退货单号:" + rmaNo);
                    }

                    if (saleRma.Status == EnumRMAStatus.PayVerify.AsId())
                    {
                        throw new RmaException("该退货单已经确认,退货单号:" + rmaNo);
                    }

                    var rma = db.RMAs.FirstOrDefault(x => x.RMANo == rmaNo);
                    if (rma != null)
                    {
                        rma.Status = 10;//--退货完成
                        rma.UpdateDate = DateTime.Now;
                        rma.UpdateUser = -1;
                        db.Entry(rma).State = EntityState.Modified;
                    }
                    saleRma.RealRMASumMoney = money;
                    saleRma.RecoverableSumMoney = saleRma.RealRMASumMoney - saleRma.CompensationFee;
                    saleRma.Status = EnumRMAStatus.PayVerify.AsId();
                    saleRma.UpdatedDate = DateTime.Now;
                    saleRma.UpdatedUser = -1;
                    db.Entry(saleRma).State = EntityState.Modified;

                    
                    db.SaveChanges();
                    ts.Complete();
                }
            }
        }

        public PageResult<SaleRmaDto> GetByRmaNo(string rmaNo, int pageIndex, int pageSize)
        {
            ISaleRMARepository rep = _repository as ISaleRMARepository;
            return rep.GetAll("", "", "", rmaNo, new DateTime(2000, 1, 1), DateTime.Now.Date.AddDays(1),
                EnumRMAStatus.NoDelivery.AsId(), null, EnumReturnGoodsStatus.ServiceApprove, pageIndex, pageSize);
        }

        /// <summary>
        /// 包裹审核
        /// </summary>
        /// <param name="rmaNo"></param>
        /// <param name="passed"></param>
        public void PackageVerify(string rmaNo, bool passed)
        {
            var saleRma = _rmaRepository.GetByRmaNo2(rmaNo);
            if (saleRma == null)
            {
                throw new OpcException("快递单不存在,退货单号:" + rmaNo);
            }

            if (saleRma.RMAStatus == (int)EnumReturnGoodsStatus.Void)
            {
                throw new RmaException(string.Format("退货单已作废"));
            }

            if (saleRma.Status != EnumRMAStatus.ShipReceive.AsId())
            {
                throw new OpcException("该退货单已经确认或正在财务审核,退货单号:" + rmaNo);
            }
            var status = passed ? EnumRMAStatus.ShipVerifyPass.AsId() : EnumRMAStatus.ShipVerifyNotPass.AsId();
            using (var ts = new TransactionScope())
            {
                saleRma.Status = status;
                _rmaRepository.Update(saleRma);
                if (!passed) //物流审核未通过，则回滚退货数量，置退货单状态为取消
                {
                    using (var db = new YintaiHZhouContext())
                    {
                        var rmaItems = db.Set<RMAItem>().Where(i => i.RMANo == rmaNo);
                        var rma = db.Set<RMA>().First(x => x.RMANo == rmaNo);
                        rma.Status = 13; //物流审核不通过
                        rma.UpdateDate = DateTime.Now;
                        rma.UpdateUser = -1;

                        db.Entry(rma).State = EntityState.Modified;
                        foreach (var rmaItem in rmaItems)
                        {
                            rmaItem.Status = -1;
                            rmaItem.UpdateDate = DateTime.Now;
                            db.Entry(rmaItem).State = EntityState.Modified;
                            var detail =
                                db.OPC_RMADetails.Where(x => x.RMANo == rmaNo)
                                    .Join(
                                        db.OrderItems.Where(
                                            x => x.OrderNo == rma.OrderNo && x.ProductId == rmaItem.ProductId),
                                        d => d.OrderItemId, i => i.Id, (d, i) => d)
                                    .FirstOrDefault();
                            var item = (from d in db.OPC_SaleDetails
                                from i in db.OrderItems
                                from r in db.OPC_RMAs
                                from s in db.OPC_Sales
                                where
                                    d.OrderItemId == i.Id && 
                                    r.RMANo == rmaNo &&
                                    r.SaleOrderNo == s.SaleOrderNo &&
                                    d.SaleOrderNo == s.SaleOrderNo
                                select d).FirstOrDefault();
                            //detail.BackCount = detail.BackCount - rmaItem.Quantity;
                            if (detail == null)
                            {
                                throw new RmaException(string.Format("没有找到退货明细，退货单号:{0}",rmaNo));
                            }

                            if (item == null)
                            {
                                throw new RmaException(string.Format("没有找到销售单明细，退货单号:{0}",rmaNo));
                            }

                            item.BackNumber = item.BackNumber - rmaItem.Quantity;
                            item.UpdatedDate = DateTime.Now;
                            item.UpdatedUser = -1;
                            db.Entry(item).State = EntityState.Modified;
                            detail.UpdatedDate = DateTime.Now;
                            detail.UpdatedUser = -1;
                            detail.Status = -1; //物流审核不通过，则该记录废弃

                            db.Entry(detail).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                }
                ts.Complete();
            }
        }

        public PageResult<SaleRmaDto> GetByFinaceDto(FinaceRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);

            var rep = _repository as ISaleRMARepository;
            var lst = rep.GetAll(dto.OrderNo, dto.SaleOrderNo, "", "", dto.StartDate, dto.EndDate,
                EnumRMAStatus.NoDelivery.AsId(), null, EnumReturnGoodsStatus.CompensateVerify, dto.pageIndex, dto.pageSize);

            return lst;
        }

        public void FinaceVerify(string rmaNo, bool pass)
        {

            var rep = _rmaRepository;
            var saleRma = rep.GetByRmaNo2(rmaNo);
            if (saleRma == null)
            {
                throw new OpcException("快递单不存在,退货单号:" + rmaNo);
            }
            if (saleRma.RMAStatus == EnumReturnGoodsStatus.CompensateVerify.AsId())
            {
                saleRma.RMAStatus = (int)(pass ? EnumReturnGoodsStatus.CompensateVerifyPass : EnumReturnGoodsStatus.CompensateVerifyFailed);
                if (pass)
                {
                    saleRma.Status = EnumRMAStatus.ShipNoReceive.AsId();
                }
                rep.Update(saleRma);
            }
        }

        public PageResult<SaleRmaDto> GetByReturnGoodsCompensate(ReturnGoodsInfoRequest request)
        {
            var rep = _repository as ISaleRMARepository;
            return rep.GetAll(request.OrderNo, request.SaleOrderNo, request.PayType, request.RmaNo,
                 request.StartDate, request.EndDate, request.RmaStatus, request.StoreID, EnumReturnGoodsStatus.CompensateVerifyFailed, request.pageIndex, request.pageSize);
        }

        public void SetSaleRmaServiceAgreeGoodsBack(string rmaNo)
        {
            var rma = _rmaRepository.GetByRmaNo2(rmaNo);
            if (rma == null)
            {
                throw new OpcException(string.Format("退货收货单不存在，退货单号{0}", rmaNo));
            }
            rma.RMAStatus = EnumReturnGoodsStatus.ServiceAgreeGoodsBack.AsId();
            _rmaRepository.Update(rma);
        }

        public PageResult<SaleRmaDto> GetOrderAutoBack(ReturnGoodsRequest request)
        {
            var rep = _repository as ISaleRMARepository;

            request.StartDate = request.StartDate.Date;
            request.EndDate = request.EndDate.Date.AddDays(1);
            return rep.GetOrderAutoBack(request);
        }

        public void CreateSaleRmaAuto(int user, RMARequest request)
        {
            CreateSaleRmaSub(user, request, "网络自助退货");
        }

        private void Save(IEnumerable<RmaConfig> configs)
        {
            foreach (RmaConfig config in configs)
            {
                config.Create();
                CreateRma(config);
                _rmaRepository.Create(config.OpcRma);

                foreach (OPC_RMADetail rmaDetail in config.OpcRmaDetails)
                {
                    _rmaDetailRepository.Create(rmaDetail);
                }
            }
        }

        /// <summary>
        /// 增加dbo.RMA及dbo.RMAItems
        /// </summary>
        /// <param name="config"></param>
        private void CreateRma(RmaConfig config)
        {
            using (var db = new YintaiHZhouContext())
            {
                db.RMAs.Add(new RMA
                {
                    CreateDate = DateTime.Now,
                    RMAReason = 10, //默认是其他原因
                    Reason = config.Reason,
                    Status = 2,//退货审核通过，客服发起的默认是审核通过的
                    OrderNo = config.OpcSale.OrderNo,
                    CreateUser = config.UserId,
                    RMAAmount = config.Details.Sum(x => x.ReturnCount * x.OrderItem.ItemPrice),
                    UpdateDate = DateTime.Now,
                    UpdateUser = config.UserId,
                    RMAType = 2,//2为线下退货
                    ContactPerson = string.Empty,
                    ContactPhone = string.Empty,
                    RMANo = config.RmaNo
                });
                foreach (var detail in config.Details)
                {

                    db.RMAItems.Add(new RMAItem()
                    {
                        RMANo = config.RmaNo,
                        ItemPrice = detail.OrderItem.ItemPrice,
                        ExtendPrice = detail.OrderItem.ItemPrice * detail.ReturnCount,
                        ProductDesc = detail.OrderItem.ProductDesc,
                        ProductId = detail.OrderItem.ProductId,
                        ColorId = detail.OrderItem.ColorId,
                        SizeId = detail.OrderItem.SizeId,
                        Quantity = detail.ReturnCount,
                        ColorValueId = detail.OrderItem.ColorValueId,
                        SizeValueId = detail.OrderItem.SizeValueId,
                        ColorValueName = detail.OrderItem.ColorValueName,
                        SizeValueName = detail.OrderItem.SizeValueName,
                        UpdateDate = DateTime.Now,
                        StoreItem = detail.OrderItem.StoreItemNo,
                        StoreDesc = detail.OrderItem.StoreItemDesc,
                        BrandId = detail.OrderItem.BrandId,
                        CreateDate = DateTime.Now,
                        UnitPrice = detail.OrderItem.UnitPrice
                    });
                }
                db.SaveChanges();
            }
        }

        private string CreateRmaNo()
        {
            using (var db = new YintaiHZhouContext())
            {
                var code = string.Concat(string.Format("6{0}", DateTime.Now.ToString("yyMMdd"))
           , DateTime.UtcNow.Ticks.ToString().Reverse().Take(5)
               .Aggregate(new StringBuilder(), (s, e) => s.Append(e), s => s.ToString())
               .PadRight(5, '0'));
                var existingCodes = db.RMAs.Count(x => x.RMANo == code && x.CreateDate >= DateTime.Today);
                if (existingCodes > 0)
                    code = string.Concat(code, (existingCodes + 1).ToString());
                return code;
            }
        }
    }

    internal class RmaConfig
    {
        public RmaConfig(int userId)
        {
            UserId = userId;
            Details = new List<SubRmaConfig>();
            OpcRmaDetails = new List<OPC_RMADetail>();
            SaleRmaSource = "客服退货";
        }

        public string Reason { get; set; }
        public decimal CustomFee { get; set; }
        public decimal StoreFee { get; set; }
        public decimal RefundAmount { get; set; }
        public int UserId { get; private set; }

        public int StoreID { get; set; }
        public string RmaNo { get; set; }

        public string SaleOrderNo { get; set; }

        public string SaleRmaSource { get; set; }
        public IList<SubRmaConfig> Details { get; set; }

        public OPC_Sale OpcSale { get; set; }

        public OPC_RMA OpcRma { get; private set; }

        public IList<OPC_RMADetail> OpcRmaDetails { get; private set; }

        public void Create()
        {
            foreach (SubRmaConfig subRmaConfig in Details)
            {
                OpcRmaDetails.Add(subRmaConfig.CreateRmaDetail(UserId, RmaNo));
            }

            OpcRma = CreateOpcRma(UserId);
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>OPC_RMA.</returns>
        private OPC_RMA CreateOpcRma(int userId)
        {
            var fee = ComputeAccount();
            var rma = new OPC_RMA
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                RMANo = this.RmaNo,
                CreatedUser = userId,
                UpdatedUser = userId,
                StoreId = StoreID,
                SaleOrderNo = SaleOrderNo,
                OrderNo = OpcSale.OrderNo,
                RMAType = 1,
                RefundAmount = RefundAmount,
                RMAAmount = fee,
                UpdatedDate = DateTime.Now,
                StoreFee = StoreFee,
                CustomFee = CustomFee,
                SaleRMASource = SaleRmaSource,
                RealRMASumMoney = RefundAmount,
                Reason = Reason,
                CompensationFee = fee,
                //BackDate = DateTime.Now,
                RecoverableSumMoney = RefundAmount - fee,
                RMACashStatus = EnumRMACashStatus.NoCash.AsId(),
                SectionId = OpcSale.SectionId,
                Count = this.Details.Sum(x => x.ReturnCount),
                RMAStatus = RefundAmount - fee > 0
                    ? EnumReturnGoodsStatus.CompensateVerify.AsId()
                    : EnumReturnGoodsStatus.ServiceApprove.AsId(),

                Status = RefundAmount - fee > 0
                        ? EnumRMAStatus.NoDelivery.AsId()
                        : EnumRMAStatus.ShipNoReceive.AsId()

            };
            return rma;
        }

        public decimal ComputeAccount()
        {
            return Details.Sum(config => config.ReturnCount * config.OrderItem.ItemPrice);
        }
    }

    internal class SubRmaConfig
    {
        public int OrderDetailId { get; set; }
        public int ReturnCount { get; set; }

        public OPC_SaleDetail OpcSaleDetail { get; set; }

        public OrderItem OrderItem { get; set; }

        /// <summary>
        ///     生成退货单明细 OPC_RMADetail
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="rmaNo"></param>
        public OPC_RMADetail CreateRmaDetail(int userId, string rmaNo)
        {
            var rmaDetail = new OPC_RMADetail
            {
                CreatedUser = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedUser = userId,
                Price = OpcSaleDetail.Price,
                ProdSaleCode = OpcSaleDetail.ProdSaleCode,
                BackCount = ReturnCount,
                Status = EnumRMAStatus.NoDelivery.AsId(),
                StockId = OpcSaleDetail.StockId,
                RMANo = rmaNo,
                Amount = OpcSaleDetail.Price * ReturnCount,
                RefundDate = DateTime.Now,
                OrderItemId = OrderDetailId,
                SectionCode = OpcSaleDetail.SectionCode
            };
            return rmaDetail;
        }
    }
}