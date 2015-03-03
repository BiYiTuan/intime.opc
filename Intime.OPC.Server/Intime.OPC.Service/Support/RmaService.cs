using System.Data.Entity;
using System.Linq;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Intime.OPC.Service.Support
{
    public class RmaService : BaseService<OPC_RMA>, IRmaService
    {
        private readonly IRmaDetailRepository _rmaDetailRepository;
        private readonly IRmaCommentRepository _rmaCommentRepository;
        private readonly IConnectProduct _connectProduct;
        private readonly ISaleRMARepository _saleRmaRepository;
        private IStockRepository _stockRepository;
        private IAccountService _accountService;


        private readonly IRMARepository _rmaRepository;

        public RmaService(IRMARepository repository, IRmaDetailRepository rmaDetailRepository, IRmaCommentRepository rmaCommentRepository, IConnectProduct connectProduct, ISaleRMARepository saleRmaRepository, IStockRepository stockRepository, IAccountService accountService)
            : base(repository)
        {
            _rmaRepository = repository;
            _rmaDetailRepository = rmaDetailRepository;
            _rmaCommentRepository = rmaCommentRepository;
            _connectProduct = connectProduct;
            _saleRmaRepository = saleRmaRepository;
            _stockRepository = stockRepository;
            _accountService = accountService;
        }

        #region IRmaService Members

        public PageResult<RMADto> GetAll(PackageReceiveRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = rep.GetAll(dto.OrderNo, dto.SaleOrderNo, dto.StartDate, dto.EndDate, EnumRMAStatus.ShipNoReceive.AsId(), EnumReturnGoodsStatus.NoProcess, dto.pageIndex, dto.pageSize);
            return lst;
        }

        public PageResult<RmaDetail> GetDetails(string rmaNo, int pageIndex, int pageSize)
        {
            var lst = _rmaDetailRepository.GetByRmaNo(rmaNo, pageIndex, pageSize);
            return lst;
        }
        public PageResult<RMADto> GetByOrderNo(string orderNo, int? rmaStatus, EnumReturnGoodsStatus returnGoodsStatus, int pageIndex, int pageSize)
        {
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            var lst = rep.GetAll(orderNo, "", new DateTime(2000, 1, 1), DateTime.Now.Date.AddDays(1), rmaStatus, returnGoodsStatus, pageIndex, pageSize);
            return lst;
        }

        public PageResult<RMADto> GetByRmaNo(string rmaNo)
        {
            var rep = (IRMARepository)_repository;
            var lst = rep.GetByRmaNo(rmaNo);

            return lst;
        }

        public void AddComment(OPC_RMAComment comment)
        {
            _rmaCommentRepository.Create(comment);
        }

        public IList<OPC_RMAComment> GetCommentByRmaNo(string rmaNo)
        {
            return _rmaCommentRepository.GetByRmaID(rmaNo);
        }

        #endregion


        /// <summary>
        /// Gets all pack verify.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>PageResult{RMADto}.</returns>
        public PageResult<RMADto> GetAllPackVerify(PackageReceiveRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            PageResult<RMADto> lst = rep.GetAll(dto.OrderNo, dto.SaleOrderNo, dto.StartDate, dto.EndDate, EnumRMAStatus.ShipReceive.AsId(), EnumReturnGoodsStatus.NoProcess, dto.pageIndex, dto.pageSize);
            return lst;
        }

        public PageResult<RMADto> GetByFinaceDto(FinaceRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            PageResult<RMADto> lst = rep.GetAll(dto.OrderNo, dto.SaleOrderNo, dto.StartDate, dto.EndDate, EnumRMAStatus.NoDelivery.AsId(), EnumReturnGoodsStatus.CompensateVerify, dto.pageIndex, dto.pageSize);
            return lst;
        }

        public PageResult<RMADto> GetRmaByPackPrintPress(RmaExpressRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            PageResult<RMADto> lst = rep.GetByPackPrintPress(dto.OrderNo, "", dto.StartDate, dto.EndDate, EnumRMAStatus.ShipReceive.AsId(), dto.pageIndex, dto.pageSize);
            return lst;
        }

        public PageResult<RMADto> GetRmaCashByExpress(RmaExpressRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            PageResult<RMADto> lst = rep.GetByPackPrintPress(dto.OrderNo, "", dto.StartDate, dto.EndDate, EnumRMAStatus.ShipVerifyPass.AsId(), dto.pageIndex, dto.pageSize);
            return lst;
        }

        public void SetRmaCash(string rmaNo)
        {
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            var rma = rep.GetByRmaNo2(rmaNo);
            var saleRma = _saleRmaRepository.GetByRmaNo(rmaNo);

            var cashNo = _connectProduct.GetCashNo(saleRma.OrderNo, rmaNo, saleRma.RealRMASumMoney.Value);
            rma.RmaCashNum = cashNo;
            rma.RmaCashDate = DateTime.Now;
            rep.Update(rma);



        }

        public void SetRmaCashOver(string rmaNo)
        {
            //var saleRma = _saleRmaRepository.GetByRmaNo(rmaNo);

            //saleRma.RMACashStatus = EnumRMACashStatus.CashOver.AsID();
            //saleRma.RMAStatus = EnumReturnGoodsStatus.Valid.AsID();
            //_saleRmaRepository.Update(saleRma);

            using (TransactionScope ts = new TransactionScope())
            {

                var rep = (IRMARepository)_repository;
                var rma = rep.GetByRmaNo2(rmaNo);
                rma.RMACashStatus = EnumRMACashStatus.CashOver.AsId();
                rma.RMAStatus = EnumReturnGoodsStatus.Valid.AsId();
                rep.Update(rma);



                var lstDetail = _rmaDetailRepository.GetByRmaNo(rmaNo, 1, 1000);

                //更新库存
                foreach (var detail in lstDetail.Result)
                {
                    var stock = _stockRepository.GetByID(detail.StockId.Value);
                    stock.Count += detail.BackCount;
                    _stockRepository.Update(stock);
                }
                ts.Complete();
            }

        }

        public PageResult<RMADto> GetRmaReturnByExpress(RmaExpressRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            PageResult<RMADto> lst = rep.GetRmaReturnByExpress(dto.OrderNo, dto.StartDate, dto.EndDate, dto.pageIndex, dto.pageSize);
            return lst;
        }

        /// <summary>
        /// 物流入库
        /// </summary>
        /// <param name="rmaNo"></param>
        public void SetRmaShipInStorage(string rmaNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                var opcRma = db.OPC_RMAs.FirstOrDefault(x => x.RMANo == rmaNo);
                if (opcRma == null)
                {
                    throw new RmaException(string.Format("无效的退货单号{0}", rmaNo));
                }
                opcRma.Status = EnumRMAStatus.ShipInStorage.AsId();
                opcRma.UpdatedDate = DateTime.Now;
                opcRma.UpdatedUser = -1;
                opcRma.BackDate = DateTime.Now;
                db.Entry(opcRma).State = EntityState.Modified;
                var rmaEntity = db.RMAs.FirstOrDefault(x => x.RMANo == rmaNo);
                if (rmaEntity != null)
                {
                    rmaEntity.Status = 12;//退货已签收
                    rmaEntity.UpdateDate = DateTime.Now;
                    rmaEntity.UpdateUser = -1;
                    db.Entry(rmaEntity).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public PageResult<RMADto> GetRmaPrintByExpress(RmaExpressRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(_accountService.GetByUserID(UserId));
            // PageResult<RMADto> lst = rep.GetByPackPrintPress(dto.OrderNo, "", dto.StartDate, dto.EndDate, EnumRMAStatus.ShipInStorage.AsID(), dto.pageIndex, dto.pageSize);
            var lst = rep.GetRmaPrintByExpress(dto.OrderNo, dto.StartDate, dto.EndDate, dto.pageIndex, dto.pageSize);
            return lst;
        }

        public void SetRmaPint(string rmaNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                var opcRma = db.OPC_RMAs.FirstOrDefault(x => x.RMANo == rmaNo);
                if (opcRma == null)
                {
                    throw new RmaException(string.Format("无效的退货单号{0}", rmaNo));
                }
                opcRma.Status = EnumRMAStatus.PrintRMA.AsId();
                opcRma.UpdatedDate = DateTime.Now;
                opcRma.UpdatedUser = -1;
                db.SaveChanges();
            }
        }

        public PageResult<RMADto> GetRmaByShoppingGuide(ShoppingGuideRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);
            var userDto = _accountService.GetByUserID(UserId);
            var rep = (IRMARepository)_repository;
            rep.SetCurrentUser(userDto);
            var lst = rep.GetRmaByShoppingGuide(dto.OrderNo, dto.StartDate, dto.EndDate, dto.pageIndex, dto.pageSize);
            return lst;

        }

        public PageResult<RMADto> GetRmaByAllOver(ShoppingGuideRequest dto)
        {
            dto.StartDate = dto.StartDate.Date;
            dto.EndDate = dto.EndDate.Date.AddDays(1);


            var rep = (IRMARepository)_repository;
            var userDto = _accountService.GetByUserID(UserId);
            rep.SetCurrentUser(userDto);
            var lst = rep.GetRmaByAllOver(dto.OrderNo, dto.StartDate, dto.EndDate, dto.pageIndex, dto.pageSize);
            return lst;
        }

        public PagerInfo<RMADto> GetPagedList(RmaQueryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return _rmaRepository.GetPagedList(request);
        }

        /// <summary>
        /// 设置收银
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExectueResult SetCashNo(RmaSetCashNoRequest request, int userId)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            /*  设置收银
             *  1.只有迷你银的RMA单可以设置
             *  2.[Description("完成打印退货单")] PrintRMA = 30,
             *  3.收银状态不判断，可以重复录收银
             */
            var rmaModel = _rmaRepository.GetItem(request.RmaNo);
            if (rmaModel == null)
            {
                throw new NotExistsRmaException(request.RmaNo);
            }

            if (rmaModel.OrderProductType != OrderProductType.MiniSilver.AsId())
            {
                throw new NotOrderProductTypeRmaException(request.RmaNo, OrderProductType.MiniSilver);
            }

            if (rmaModel.Status != EnumRMAStatus.PrintRMA.AsId())
            {
                throw new NotStatusRmaException(request.RmaNo, (EnumRMAStatus)rmaModel.Status, EnumRMAStatus.PrintRMA);
            }

            var entity = _rmaRepository.GetByRmaNo2(rmaModel.RMANo);


            entity.UpdatedUser = UserId;
            //entity.UpdatedDate = DateTime.Now;
            entity.RmaCashNum = request.CashNo;
            entity.RmaCashDate = DateTime.Now;
            entity.RMACashStatus = EnumRMACashStatus.CashOver.AsId();

            _rmaRepository.Update(entity);

            //rma_details.cash
            //var details = _rmaDetailRepository.GetListByRmano(entity.RMANo);
            //foreach (var item in details)
            //{
            //    item.UpdatedDate = DateTime.Now;
            //    item.UpdatedUser = UserId;
            //    item.CashNum = entity.RmaCashNum;
            //}

            return new OkExectueResult();
        }

        /// <summary>
        /// 设置收货 目前没有同步状态到ram_details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ExectueResult SetReceipt(RmaReceiptRequest request, int userId)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var entity = _rmaRepository.GetByRmaNo2(request.RmaNo);
            if (entity == null)
            {
                throw new NotExistsRmaException(request.RmaNo);
            }

            /*
             * 1.判断状态 [Description("物流收货")] ShipReceive = 5,
             * 2.
             */

            if (entity.Status > EnumRMAStatus.ShipReceive.AsId())
            {
                throw new NotStatusRmaException(request.RmaNo, (EnumRMAStatus)entity.Status, EnumRMAStatus.ShipReceive);
            }

            if (entity.Status == EnumRMAStatus.ShipReceive.AsId())
            {
                throw new OpcException(String.Format("当前已经是{0}({1})", EnumRMAStatus.ShipReceive.GetDescription(), EnumRMAStatus.ShipReceive.AsId()));
            }

            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUser = userId;
            entity.Status = EnumRMAStatus.ShipReceive.AsId();

            _rmaRepository.Update(entity);

            return new OkExectueResult();
        }

        /// <summary>
        /// 设置退货
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ExectueResult SetReturnOfGoods(RmaReturnOfGoodsRequest request, int userId)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var entity = _rmaRepository.GetByRmaNo2(request.RmaNo);
            if (entity == null)
            {
                throw new NotExistsRmaException(request.RmaNo);
            }

            /*
             * 1.[Description("完成打印退货单")] PrintRMA = 30, &&  [Description("完成收银")]  CashOver = 10
             * 2.
             */

            //rma
            if (entity.Status != EnumRMAStatus.PrintRMA.AsId())
            {
                throw new NotStatusRmaException(request.RmaNo, (EnumRMAStatus)entity.Status, EnumRMAStatus.PrintRMA);
            }

            //收银状态
            if (entity.RMACashStatus != EnumRMACashStatus.CashOver.AsId())
            {
                throw new NotCashStatusRmaException(request.RmaNo, (EnumRMACashStatus)entity.Status, EnumRMACashStatus.CashOver);
            }

            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUser = userId;
            entity.Status = EnumRMAStatus.ShoppingGuideReceive.AsId();

            _rmaRepository.Update(entity);

            return new OkExectueResult();
        }
    }
}