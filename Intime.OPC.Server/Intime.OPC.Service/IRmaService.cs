using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IRmaService : IService
    {
        PageResult<RMADto> GetAll(PackageReceiveRequest dto);

        /// <summary>
        ///     获得退货单详情
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        /// <returns>IList{RmaDetail}.</returns>
        PageResult<RmaDetail> GetDetails(string rmaNo,int pageIndex,int pageSize);

        /// <summary>
        /// Gets the by order no.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="rmaStatus">退货单状态</param>
        /// <param name="returnGoodsStatus">退货状态</param>
        /// <returns>IList{RMADto}.</returns>
        PageResult<RMADto> GetByOrderNo(string orderNo, int? rmaStatus, EnumReturnGoodsStatus returnGoodsStatus, int pageIndex, int pageSize);

        PageResult<RMADto> GetByRmaNo(string rmaNo);



        void AddComment(OPC_RMAComment comment);

        IList<OPC_RMAComment> GetCommentByRmaNo(string rmaNo);

        /// <summary>
        /// 退货包裹审核 查询退货单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{RMADto}.</returns>
        PageResult<RMADto> GetAllPackVerify(PackageReceiveRequest request);

        PageResult<RMADto> GetByFinaceDto(FinaceRequest request);
        /// <summary>
        /// 包裹退回-打印快递单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{RMADto}.</returns>
        PageResult<RMADto> GetRmaByPackPrintPress(RmaExpressRequest request);



        /// <summary>
        /// 退货入收银  查询 退货单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{RMADto}.</returns>
        PageResult<RMADto> GetRmaCashByExpress(RmaExpressRequest request);

        /// <summary>
        /// 退货入收银
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void SetRmaCash(string rmaNo);

        void SetRmaCashOver(string rmaNo);
        /// <summary>
        /// 退货入库  查询 退货单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{RMADto}.</returns>
        PageResult<RMADto> GetRmaReturnByExpress(RmaExpressRequest request);

        /// <summary>
        /// 退货入库
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void SetRmaShipInStorage(string rmaNo);

        /// <summary>
        /// 打印退货单，（物流审核通过）
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{RMADto}.</returns>
        PageResult<RMADto> GetRmaPrintByExpress(RmaExpressRequest request);

        /// <summary>
        /// 设置打印状态
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void SetRmaPint(string rmaNo);

        PageResult<RMADto> GetRmaByShoppingGuide(ShoppingGuideRequest request);

        PageResult<RMADto> GetRmaByAllOver(ShoppingGuideRequest request);

        /// <summary>
        /// 获取RMA单 
        /// </summary>
        /// <param name="request">参数集</param>
        /// <returns></returns>
        PagerInfo<RMADto> GetPagedList(RmaQueryRequest request);

        /// <summary>
        /// 设置收银
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ExectueResult SetCashNo(RmaSetCashNoRequest request, int userId);

        /// <summary>
        /// 设置收货
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ExectueResult SetReceipt(RmaReceiptRequest request, int userId);

        /// <summary>
        /// 设置退货
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ExectueResult SetReturnOfGoods(RmaReturnOfGoodsRequest request, int userId);
    }
}