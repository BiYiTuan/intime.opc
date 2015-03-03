using System.Web.Http;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/statistics")]
    public class StatisticsController : BaseController
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IEnumService _enumService;
        private static readonly int CashStatus = AssociateIncomeRequestStatus.Transferred.AsId();

        public StatisticsController(IOrderItemRepository orderItemRepository, IEnumService enumService)
        {
            _orderItemRepository = orderItemRepository;
            _enumService = enumService;
        }

        #region methods

        /// <summary>
        /// 专柜表 筛选
        /// </summary>
        /// <param name="sectoinCode"></param>
        /// <returns></returns>
        private static Expression<Func<Section, bool>> SectionFilter(string sectoinCode)
        {
            var query = PredicateBuilder.True<Section>();

            if (!String.IsNullOrEmpty(sectoinCode))
            {
                query = PredicateBuilder.And(query, v => v.SectionCode == sectoinCode);
            }



            return query;
        }

        /// <summary>
        /// 礼品卡表 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<IMS_GiftCardOrder, bool>> GiftCardOrderFilter(GiftCardSalesStatisticsReportRequest filter)
        {
            var query = PredicateBuilder.True<IMS_GiftCardOrder>();

            if (!String.IsNullOrEmpty(filter.GiftCardNo))
            {
                query = PredicateBuilder.And(query, v => v.No == filter.GiftCardNo);
                return query;
            }

            if (!String.IsNullOrEmpty(filter.TransNo))
            {

            }
            else
            {
                if (filter.BuyStartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate >= filter.BuyStartDate);
                }

                if (filter.BuyEndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate < filter.BuyEndDate);
                }

            }

            return query;
        }

        /// <summary>
        /// 渠道订单 筛选
        /// </summary>
        /// <param name="transNo"></param>
        /// <returns></returns>
        private static Expression<Func<OrderTransaction, bool>> OrderTransactionFilter(string transNo)
        {
            var query = PredicateBuilder.True<OrderTransaction>();

            if (!String.IsNullOrEmpty(transNo))
            {
                query = PredicateBuilder.And(query, v => v.TransNo == transNo);
                return query;
            }

            return query;
        }

        /// <summary>
        /// 支付方式筛选
        /// </summary>
        /// <param name="paymentCode"></param>
        /// <returns></returns>
        private static Expression<Func<PaymentMethod, bool>> PaymentFilter(string paymentCode)
        {
            var query = PredicateBuilder.True<PaymentMethod>();

            if (!String.IsNullOrEmpty(paymentCode))
            {
                query = PredicateBuilder.And(query, v => v.Code == paymentCode);
                return query;
            }

            return query;
        }

        /// <summary>
        /// store 筛选
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="datarolestores"></param>
        /// <returns></returns>
        private static Expression<Func<Store, bool>> StoreFilter(int? storeId, ICollection<int> datarolestores)
        {
            var query = PredicateBuilder.True<Store>();

            if (storeId != null)
            {
                query = PredicateBuilder.And(query, v => v.Id == storeId.Value);
            }

            if (storeId == null && datarolestores != null)
            {
                query = PredicateBuilder.And(query, v => datarolestores.Contains(v.Id));
            }

            return query;
        }


        /// <summary>
        /// 用户 筛选
        /// </summary>
        /// <param name="nickname">昵称</param>
        /// <param name="contract">联系方式</param>
        /// <returns></returns>
        private static Expression<Func<User, bool>> UserFilter(string nickname, string contract)
        {
            var query = PredicateBuilder.True<User>();

            if (!String.IsNullOrEmpty(nickname))
            {
                query = PredicateBuilder.And(query, v => v.Nickname == nickname);
            }

            if (!String.IsNullOrEmpty(contract))
            {
                query = PredicateBuilder.And(query, v => v.Mobile == contract);
            }

            return query;
        }


        /// <summary>
        /// 礼品卡表 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<IMS_AssociateIncomeRequest, bool>> CashAssociateCommissionStatisticsReportFilter(CashAssociateCommissionStatisticsReportRequest filter)
        {
            var query = PredicateBuilder.True<IMS_AssociateIncomeRequest>();

            if (!String.IsNullOrEmpty(filter.BankName))
            {
                query = PredicateBuilder.And(query, v => v.BankName == filter.BankName);
                return query;
            }

            if (!String.IsNullOrEmpty(filter.BankCode))
            {
                query = PredicateBuilder.And(query, v => v.BankCode == filter.BankCode);
                return query;
            }

            if (filter.PickUpStartDate != null)
            {
                query = PredicateBuilder.And(query, v => v.CreateDate >= filter.PickUpStartDate);
            }

            if (filter.PickUpEndDate != null)
            {
                query = PredicateBuilder.And(query, v => v.CreateDate < filter.PickUpEndDate);
            }

            return query;
        }

        #endregion


        //        SELECT [NO] AS                          N'礼品卡编号'
        //      ,ot.transno                    AS N'渠道订单号'
        //      ,gc.AMOUNT                     AS N'金额'
        //      ,ot.paymentcode                AS N'支付方式'
        //      ,gc.CREATEDATE                 AS N'下单时间'
        //      ,CASE gc.STATUS
        //            WHEN 1 THEN N'否'
        //            ELSE N'是'
        //       END                           AS N'是否已充值'
        //      ,pm.Name  AS '支付方式名称'
        //      ,d.Id AS Associate_Id
        //      ,d.OperatorCode 
        //      ,d.UserId 
        //FROM   dbo.IMS_GIFTCARDORDER gc WITH(NOLOCK)
        //       INNER JOIN dbo.ordertransaction ot WITH(NOLOCK)
        //            ON  ot.orderno = gc.No
        //       INNER JOIN dbo.PaymentMethod  AS pm WITH(NOLOCK)
        //            ON  ot.PaymentCode = pm.Code
        //       INNER JOIN dbo.IMS_AssociateIncomeHistory AS c
        //            ON  gc.No = c.SourceNo
        //                AND c.SourceType = 1
        //       INNER JOIN dbo.IMS_Associate  AS d
        //            ON  c.AssociateUserId = d.UserId
        //       INNER JOIN dbo.Store          AS e
        //           ON  d.StoreId = e.Id
        //ORDER BY
        //       gc.CREATEDATE ASC;

        /// <summary>
        /// 礼品卡统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("giftcardsalesreport")]
        public IHttpActionResult GetList4GiftCardSalesReport([FromUri]GiftCardSalesStatisticsReportRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var pagerRequest = request.PagerRequest;

            List<GiftCardSalesStatisticsReportDto> datas;
            int totalCount;

            var giftcardorderFilter = GiftCardOrderFilter(request);
            var transFilter = OrderTransactionFilter(request.TransNo);
            var paymentFilter = PaymentFilter(request.PaymentMethodCode);
            var storeFilter = StoreFilter(request.StoreId, request.DataRoleStores);
            using (var db = new YintaiHZhouContext())
            {
                var giftcardorders = db.IMS_GiftCardOrder.AsExpandable().Where(giftcardorderFilter);
                var ordertransactions = db.OrderTransactions.AsExpandable().Where(transFilter);
                var payments = db.PaymentMethods.AsExpandable().Where(paymentFilter);
                var associateIncomeHistory = db.IMS_AssociateIncomeHistory;
                var associates = db.IMS_Associate;
                var stores = db.Stores.AsExpandable().Where(storeFilter);

                var sql = from gco in giftcardorders
                          join ot in ordertransactions on gco.No equals ot.OrderNo
                          join pm in payments on ot.PaymentCode equals pm.Code
                          join aih in associateIncomeHistory on gco.No equals aih.SourceNo
                          join a in associates on aih.AssociateUserId equals a.UserId
                          join store in stores on a.StoreId equals store.Id
                          where aih.SourceType == 1 && a.Status == 1//<---------------礼品卡
                          select new GiftCardSalesStatisticsReportDto
                              {
                                  Amount = gco.Amount,
                                  SalesAmount = gco.Price,
                                  Id = gco.Id,
                                  BuyDate = gco.CreateDate,
                                  OrderNo = ot.OrderNo,
                                  PaymentMethodCode = pm.Code,
                                  PaymentMethodName = pm.Name,
                                  Status = gco.Status,
                                  StoreId = store.Id,
                                  StoreName = store.Name,
                                  TransNo = ot.TransNo
                              };

                totalCount = sql.Count();

                datas = sql.OrderByDescending(v => v.BuyDate).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();
            }

            var pager = new PagerInfo<GiftCardSalesStatisticsReportDto>(pagerRequest, totalCount, datas);
            var execresult = new OkExectueResult<PagerInfo<GiftCardSalesStatisticsReportDto>>(pager);

            return RetrunHttpActionResult4ExectueResult(execresult);
        }


        //        SELECT TOP 100
        //       c.Id                          AS Store_Id
        //      ,c.Name                        AS Store_Name
        //      ,d.Id                          AS Section_Id
        //      ,d.Name                        AS Section_Name
        //      ,d.SectionCode                 AS Section_Code
        //      ,e.Nickname                    AS MiniName
        //      ,ISNULL(e.Mobile ,e.EMail)     AS Contact
        //      ,a.AvailableAmount '未提'
        //      ,a.RequestAmount '申请'
        //      ,f.Amount '不可提'
        //FROM   [YintaiHZhou].[dbo].[IMS_AssociateIncome] AS a
        //       INNER JOIN dbo.IMS_Associate  AS b
        //            ON  a.UserId = b.UserId
        //       INNER JOIN dbo.Store          AS c
        //            ON  b.StoreId = c.Id
        //       INNER JOIN dbo.Section        AS d
        //            ON  b.SectionId = d.Id
        //       INNER JOIN dbo.[User] AS e
        //            ON  b.UserId = e.Id
        //       INNER JOIN (
        //                SELECT ISNULL(SUM([AssociateIncome]) ,0) AS Amount
        //                      ,AssociateUserId
        //                FROM   dbo.IMS_AssociateIncomeHistory
        //                WHERE  SourceType = 1
        //                       AND [Status] = 1
        //                GROUP BY
        //                       AssociateUserId
        //            )                        AS f
        //            ON  a.UserId = f.AssociateUserId;

        /// <summary>
        /// 合伙人 佣金 统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("associatecommissionreport")]
        public IHttpActionResult GetList4AssociateCommissionReport([FromUri]AssociateCommissionStatisticsReportRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var pagerRequest = request.PagerRequest;

            var storeFilter = StoreFilter(request.StoreId, request.DataRoleStores);
            var sectionFilter = SectionFilter(request.SectionCode);
            var userFilter = UserFilter(request.MiniSilverNo, request.Contact);


            List<AssociateCommissionStatisticsReportDto> datas;
            int totalCount;

            using (var db = new YintaiHZhouContext())
            {
                var associateIncomes = db.IMS_AssociateIncome;
                var associates = db.IMS_Associate;
                var stores = db.Stores.AsExpandable().Where(storeFilter);
                var sections = db.Sections.AsExpandable().Where(sectionFilter);
                var users = db.Users.AsExpandable().Where(userFilter);
                var associateIncomeHistorys = db.IMS_AssociateIncomeHistory;

                //var sql = from ai in associateIncomes
                //          join a in associates on ai.UserId equals a.UserId
                //          join store in stores on a.StoreId equals store.Id
                //          join section in sections on a.SectionId equals section.Id
                //          join user in users on a.UserId equals user.Id
                //          let historys = (
                //          from aih in associateIncomeHistorys
                //          where aih.SourceType == 1 && aih.Status == 1 && a.UserId == aih.AssociateUserId
                //          select aih
                //          )


                //          select new AssociateCommissionStatisticsReportDto
                //          {
                //              Id = ai.Id,
                //              ApplicationPickUpAmount = ai.RequestAmount,
                //              Contact = user.Mobile,
                //              HavePickUpAmount = ai.ReceivedAmount,
                //              LockedPickUpAmount = historys == null ? 0m : historys.Sum(v => v.AssociateIncome),
                //              MiniSilverNo = user.Nickname,
                //              NoPickUpAmount = ai.AvailableAmount,
                //              SectionCode = section.StoreCode,
                //              SectionId = section.Id,
                //              SectionName = section.Name,
                //              StoreId = store.Id,
                //              StoreName = store.Name
                //          };

                var sql = from ai in associateIncomes
                          join a in associates on ai.UserId equals a.UserId
                          join store in stores on a.StoreId equals store.Id
                          join section in sections on a.SectionId equals section.Id
                          join user in users on a.UserId equals user.Id
                          join aihs in
                              (
                                  from aih in associateIncomeHistorys
                                  where aih.Status == 1//《-----冻结
                                  group aih by aih.AssociateUserId into g
                                  select new
                                  {
                                      g.Key,
                                      income = g.Sum(v => v.AssociateIncome)
                                  }
                                  ) on ai.UserId equals aihs.Key into temp
                          from aihs in temp.DefaultIfEmpty()
                          where a.Status == 1
                          select new AssociateCommissionStatisticsReportDto
                          {
                              Id = ai.Id,
                              ApplicationPickUpAmount = ai.RequestAmount,
                              Contact = user.Mobile,
                              HavePickUpAmount = ai.ReceivedAmount,
                              LockedPickUpAmount = aihs == null ? 0m : aihs.income,
                              MiniSilverNo = user.Nickname,
                              NoPickUpAmount = ai.AvailableAmount,
                              SectionCode = section.SectionCode,
                              SectionId = section.Id,
                              SectionName = section.Name,
                              StoreId = store.Id,
                              StoreName = store.Name
                          };

                totalCount = sql.Count();
                datas = sql.OrderByDescending(v => v.NoPickUpAmount).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();
            }

            var pager = new PagerInfo<AssociateCommissionStatisticsReportDto>(pagerRequest, totalCount, datas);
            var execresult = new OkExectueResult<PagerInfo<AssociateCommissionStatisticsReportDto>>(pager);

            return RetrunHttpActionResult4ExectueResult(execresult);
        }


        //        SELECT TOP 1000 
        //       c.Id                          AS Store_Id
        //      ,c.Name                        AS Store_Name
        //      ,d.Id                          AS Section_Id
        //      ,d.Name                        AS Section_Name
        //      ,d.SectionCode                 AS Section_Code
        //      ,e.Nickname                    AS MiniName
        //      ,ISNULL(e.Mobile ,e.EMail)     AS Contact
        //      ,e.Nickname --迷你银帐号
        //      ,a.CreateDate                  AS '提取时间'
        //      ,a.Amount                      AS '提取金额'
        //      ,a.BankAccountName             AS '银行账户名'
        //      ,a.BankName                    AS '银行'
        //      ,a.BankNo                      AS '银行帐号'
        //      ,a.BankCode                    AS '银行编码'
        //      ,1                             AS '手续费'
        //      ,NULL                          AS '税率'
        //FROM   [YintaiHZhou].[dbo].[IMS_AssociateIncomeRequest] AS a
        //       INNER JOIN dbo.IMS_Associate  AS b
        //            ON  a.UserId = b.UserId
        //       INNER JOIN dbo.Store          AS c
        //            ON  b.StoreId = c.Id
        //       INNER JOIN dbo.Section        AS d
        //            ON  b.SectionId = d.Id
        //       INNER JOIN dbo.[User] AS e
        //            ON  b.UserId = e.Id;

        /// <summary>
        /// 已兑现 合伙人 佣金 统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cashassociatecommissionreport")]
        public IHttpActionResult GetList4CashAssociateCommissionReport([FromUri]CashAssociateCommissionStatisticsReportRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var pagerRequest = request.PagerRequest;

            var associateIncomeRequestFilter = CashAssociateCommissionStatisticsReportFilter(request);
            var storeFilter = StoreFilter(request.StoreId, request.DataRoleStores);
            var sectionFilter = SectionFilter(request.SectionCode);
            var userFilter = UserFilter(request.MiniSilverNo, request.Contact);

            List<CashAssociateCommissionStatisticsReportDto> datas;
            int totalCount;

            using (var db = new YintaiHZhouContext())
            {
                var associateIncomeRequest = db.IMS_AssociateIncomeRequest.AsExpandable().Where(associateIncomeRequestFilter);
                var associates = db.IMS_Associate;
                var stores = db.Stores.AsExpandable().Where(storeFilter);
                var sections = db.Sections.AsExpandable().Where(sectionFilter);
                var users = db.Users.AsExpandable().Where(userFilter);


                var sql = from air in associateIncomeRequest
                          join a in associates on air.UserId equals a.UserId
                          join store in stores on a.StoreId equals store.Id
                          join section in sections on a.SectionId equals section.Id
                          join user in users on a.UserId equals user.Id
                          where air.Status == CashStatus && a.Status == 1
                          select new CashAssociateCommissionStatisticsReportDto
                          {
                              Id = air.Id,
                              PickUpDate = air.CreateDate,
                              BankCardNo = air.BankNo,
                              BankName = air.BankName,
                              //Fee = 1,
                              MiniSilverNo = user.Nickname,
                              PickUpAmount = air.Amount,
                              PickUpPerson = air.BankAccountName,
                              SectionCode = section.SectionCode,
                              SectionId = section.Id,
                              SectionName = section.Name,
                              StoreId = store.Id,
                              StoreName = store.Name,
                              Contact = user.Mobile
                              //Taxes = null
                          };



                totalCount = sql.Count();
                datas = sql.OrderByDescending(v => v.PickUpDate).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();
            }


            var pager = new PagerInfo<CashAssociateCommissionStatisticsReportDto>(pagerRequest, totalCount, datas);
            var execresult = new OkExectueResult<PagerInfo<CashAssociateCommissionStatisticsReportDto>>(pager);

            return RetrunHttpActionResult4ExectueResult(execresult);
        }

        /// <summary>
        /// 销售明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("salesdetailsreport")]
        public IHttpActionResult GetList4SalesDetailsReport([FromUri] SearchStatRequest request, [UserProfile] UserProfile userProfile)
        {
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }
            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            request.ArrangeParams();

            var pagedinfo = _orderItemRepository.GetPagedList4SaleStat(request);

            return RetrunHttpActionResult(pagedinfo);
        }

        /// <summary>
        /// 退货明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("rmadetailsreport")]
        public IHttpActionResult GetList4RmaDetailsReport([FromUriAttribute] SearchStatRequest request, [UserProfile] UserProfile userProfile)
        {
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            request.ArrangeParams();


            var pagedinfo = _orderItemRepository.GetPagedList4RmaStat(request);

            return RetrunHttpActionResult(pagedinfo);
        }

        /// <summary>
        /// 收银明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cashierdetailsreport")]
        public IHttpActionResult GetList4CashierDetailsReport([FromUri] SearchCashierRequest request, [UserProfile] UserProfile userProfile)
        {
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            request.ArrangeParams();

            #region 参数调整


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

            #endregion

            var pagedinfo = _orderItemRepository.GetPagedList4CashierStat(request);

            return RetrunHttpActionResult(pagedinfo);
        }
    }
}
