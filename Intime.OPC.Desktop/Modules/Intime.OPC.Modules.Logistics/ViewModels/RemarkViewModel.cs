using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Logistics.ViewModels
{
    [Export(typeof (RemarkViewModel))]
    public class RemarkViewModel : BindableBase
    {
        private string id;
        private EnumSetRemarkType enumSetRemarkType;
        private string remark;
        private IEnumerable<OPC_Comment> remark4list;
        public DelegateCommand CommandSave { get; set; } //保存
        public DelegateCommand CommandBack { get; set; } //返回

        public RemarkViewModel()
        {
            RemarkContent = "";
        }

        //Grid数据集

        public IEnumerable<OPC_Comment> Remark4List
        {
            get { return remark4list; }
            set { SetProperty(ref remark4list, value); }
        }

        //保存数据库传递的值

        public string RemarkContent
        {
            get { return remark; }
            set { SetProperty(ref remark, value); }
        }

        public void SaveRemark()
        {
            var comment = new OPC_Comment();
            comment.RelationId = id;
            comment.Content =RemarkContent;
            comment.CreateUser = 1;
            comment.CreateDate = DateTime.Now;

            bool isSuccess = false;
            switch (enumSetRemarkType)
            {
                case EnumSetRemarkType.SetSaleRemark:
                    OPC_SaleComment salecomment = Mapper.Map<OPC_Comment, OPC_SaleComment>(comment);
                    salecomment.SaleOrderNo = comment.RelationId;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteSaleRemark(salecomment);
                    break;
                case EnumSetRemarkType.SetSaleDetailRemark:
                    OPC_SaleDetailsComment saledetailscomment = Mapper.Map<OPC_Comment, OPC_SaleDetailsComment>(comment);
                    saledetailscomment.SaleDetailId = id;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteSaleDetailsRemark(saledetailscomment);
                    break;
                case EnumSetRemarkType.SetOrderRemark:
                    OPC_OrderComment orderComment = Mapper.Map<OPC_Comment, OPC_OrderComment>(comment);
                    orderComment.OrderNo = id;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteOrderRemark(orderComment);
                    break;
                case EnumSetRemarkType.SetShipSaleRemark:
                    OPC_ShipComment shipSaleComment = Mapper.Map<OPC_Comment, OPC_ShipComment>(comment);
                    shipSaleComment.ShippingCode = comment.RelationId;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteShippingRemark(shipSaleComment);
                    break;
                case EnumSetRemarkType.SetSaleRMARemark:
                    OPC_SaleRMAComment rmaSaleRemark = Mapper.Map<OPC_Comment, OPC_SaleRMAComment>(comment);
                    rmaSaleRemark.RMANo = comment.RelationId;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteSaleRmaRemark(rmaSaleRemark);
                    break;
                case EnumSetRemarkType.SetRMARemark:
                    OPC_SaleRMAComment rmaRemark = Mapper.Map<OPC_Comment, OPC_SaleRMAComment>(comment);
                    rmaRemark.RMANo = comment.RelationId;
                    isSuccess = AppEx.Container.GetInstance<IRemarkService>().WriteRmaRemark(rmaRemark);
                    break;
            }
            if (!isSuccess)
            {
                MvvmUtility.ShowMessageAsync("保存备注失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                RemarkContent = "";
                OpenWinSearch(id,enumSetRemarkType);
            }
        }

        public void OpenWinSearch(string id, EnumSetRemarkType type)
        {
            this.id = id;
            this.enumSetRemarkType = type;
            switch (type)
            {
                case EnumSetRemarkType.SetSaleRemark:
                    PageResult<OPC_SaleComment> saleRemark =
                        AppEx.Container.GetInstance<IRemarkService>().GetSaleRemark(string.Format("saleId={0}", id));
                    Remark4List = Mapper.Map<OPC_SaleComment, OPC_Comment>(saleRemark.Result);
                    break;
                case EnumSetRemarkType.SetSaleDetailRemark:
                    PageResult<OPC_SaleDetailsComment> saleDetailRemark =
                        AppEx.Container.GetInstance<IRemarkService>()
                            .GetSaleDetailsRemark(string.Format("saledetailId={0}", id));
                    Remark4List = Mapper.Map<OPC_SaleDetailsComment, OPC_Comment>(saleDetailRemark.Result);
                    break;
                case EnumSetRemarkType.SetOrderRemark:
                    PageResult<OPC_OrderComment> orderRemark =
                        AppEx.Container.GetInstance<IRemarkService>().GetOrderRemark(string.Format("orderNo={0}", id));
                    Remark4List = Mapper.Map<OPC_OrderComment, OPC_Comment>(orderRemark.Result);
                    break;
                case EnumSetRemarkType.SetShipSaleRemark:
                    PageResult<OPC_ShipComment> shipRemark =
                        AppEx.Container.GetInstance<IRemarkService>()
                            .GetShipRemark(string.Format("shippingSaleNo={0}", id));
                    Remark4List = Mapper.Map<OPC_ShipComment, OPC_Comment>(shipRemark.Result);
                    break;
                case EnumSetRemarkType.SetSaleRMARemark:
                    PageResult<OPC_SaleRMAComment> saleRmaRemark =
                        AppEx.Container.GetInstance<IRemarkService>()
                            .GetSaleRmaRemark(string.Format("rmaNo={0}", id));
                    Remark4List = Mapper.Map<OPC_SaleRMAComment, OPC_Comment>(saleRmaRemark.Result);
                    break;
                case EnumSetRemarkType.SetRMARemark:
                    PageResult<OPC_SaleRMAComment> rmaRemark =
                        AppEx.Container.GetInstance<IRemarkService>()
                            .GetRmaRemark(string.Format("rmaNo={0}", id));
                    Remark4List = Mapper.Map<OPC_SaleRMAComment, OPC_Comment>(rmaRemark.Result);
                    break;
            }
        }
    }
}