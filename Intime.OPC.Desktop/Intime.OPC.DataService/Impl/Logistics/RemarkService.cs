using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Impl.Trans
{
    [Export(typeof (IRemarkService))]
    public class RemarkService : IRemarkService
    {
        #region 销售单备注  保存、列表查询  接口实现

        /// <summary>
        ///     销售单备注保存
        /// </summary>
        /// <param name="saleComment"></param>
        /// <returns></returns>
        public bool WriteSaleRemark(OPC_SaleComment saleComment)
        {
            try
            {
                bool bFalg = RestClient.Post("sale/writesaleremark", saleComment);
                return bFalg;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///     销售单备注查询
        /// </summary>
        /// <param name="saleIds"></param>
        /// <returns></returns>
        public PageResult<OPC_SaleComment> GetSaleRemark(string saleIds)
        {
            try
            {
                IList<OPC_SaleComment> lst = RestClient.Get<OPC_SaleComment>("sale/getsaleremarks", saleIds);
                return new PageResult<OPC_SaleComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool WriteOrderRemark(OPC_OrderComment orderComment)
        {
            try
            {
                bool bResult = RestClient.Post("order/addordercomment", orderComment);
                return bResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region 销售单备注  保存、列表查询  接口实现

        public PageResult<OPC_SaleRMAComment> GetSaleRmaRemark(string rmaId)
        {
            try
            {
                IList<OPC_SaleRMAComment> lst = RestClient.Get<OPC_SaleRMAComment>("rma/GetCommentByRmaNo",
                    rmaId);
                return new PageResult<OPC_SaleRMAComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool WriteRmaRemark(OPC_SaleRMAComment RmaComment)
        {
            try
            {
                bool bResult = RestClient.Post("rma/AddRmaComment", RmaComment);
                return bResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PageResult<OPC_SaleRMAComment> GetRmaRemark(string rmaId)
        {
            try
            {
                IList<OPC_SaleRMAComment> lst = RestClient.Get<OPC_SaleRMAComment>("rma/GetRmaCommentByRmaNo",
                    rmaId);
                return new PageResult<OPC_SaleRMAComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///     销售单备注保存
        /// </summary>
        /// <param name="saleDetailsComment"></param>
        /// <returns></returns>
        public bool WriteSaleDetailsRemark(OPC_SaleDetailsComment saleDetailsComment)
        {
            try
            {
                bool bFalg = RestClient.Post("sale/writesaledetailsremark", saleDetailsComment);
                return bFalg;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///     销售单备注查询
        /// </summary>
        /// <param name="SaleDetailIds"></param>
        /// <returns></returns>
        public PageResult<OPC_SaleDetailsComment> GetSaleDetailsRemark(string SaleDetailIds)
        {
            try
            {
                IList<OPC_SaleDetailsComment> lst = RestClient.Get<OPC_SaleDetailsComment>("sale/getsaledetailsremark",
                    SaleDetailIds);
                return new PageResult<OPC_SaleDetailsComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        public PageResult<OPC_OrderComment> GetOrderRemark(string orderId)
        {
            try
            {
                IList<OPC_OrderComment> lst = RestClient.Get<OPC_OrderComment>("order/GetCommentByOderNo",
                    orderId);
                return new PageResult<OPC_OrderComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool WriteShippingRemark(OPC_ShipComment saleComment)
        {
            try
            {
                bool bResult = RestClient.Post("trans/AddShippingSaleComment", saleComment);
                return bResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PageResult<OPC_ShipComment> GetShipRemark(string shipId)
        {
            try
            {
                IList<OPC_ShipComment> lst =
                    RestClient.Get<OPC_ShipComment>("trans/GetShippingSaleCommentByShippingSaleNo",
                        shipId);
                return new PageResult<OPC_ShipComment>(lst, lst.Count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool WriteSaleRmaRemark(OPC_SaleRMAComment rmaComment)
        {
            try
            {
                bool bResult = RestClient.Post("rma/AddSaleRmaComment", rmaComment);
                return bResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}