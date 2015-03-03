
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    [System.Obsolete("salerma 过期，请使用 rma")]
    public interface ISaleRMAService:IService
    {
        void CreateSaleRMA(int userId, RMARequest rma);

        PageResult<SaleRmaDto> GetByReturnGoodsInfo(ReturnGoodsInfoRequest request);


        PageResult<SaleRmaDto> GetByReturnGoods(ReturnGoodsRequest rquest,int userId);


        void AddComment(OPC_SaleRMAComment comment);

        IList<OPC_SaleRMAComment> GetCommentByRmaNo(string rmaNo);
        PageResult<SaleRmaDto> GetByPack(PackageReceiveRequest dto);

        /// <summary>
        /// 客服同意退货
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void AgreeReturnGoods(string rmaNo);

        /// <summary>
        /// 物流收货确认
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void ShippingReceiveGoods(string rmaNo);

        PageResult<SaleRmaDto> GetByReturnGoodPay(ReturnGoodsPayRequest request);

        /// <summary>
        /// 退货付款确认
        /// </summary>
        /// <param name="ramNo">The ram no.</param>
        /// <param name="money">The money.</param>
        void CompensateVerify(string ramNo, decimal money);

        PageResult<SaleRmaDto> GetByRmaNo(string rmaNo,int pageIndex,int pageSize);

        /// <summary>
        ///
        /// </summary>
        /// <param name="ramNo">The ram no.</param>
        /// <param name="passed">if set to <c>true</c> [passed].</param>
        void PackageVerify(string ramNo, bool passed);

        PageResult<SaleRmaDto> GetByFinaceDto(FinaceRequest request);

        void FinaceVerify(string rmaNo, bool pass);
        /// <summary>
        /// 客服退货查询
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PageResult{SaleRmaDto}.</returns>
        PageResult<SaleRmaDto> GetByReturnGoodsCompensate(ReturnGoodsInfoRequest request);

        void SetSaleRmaServiceAgreeGoodsBack(string rmaNo);
        PageResult<SaleRmaDto> GetOrderAutoBack(ReturnGoodsRequest request);
        void CreateSaleRmaAuto(int user, RMARequest request);
    }
}
