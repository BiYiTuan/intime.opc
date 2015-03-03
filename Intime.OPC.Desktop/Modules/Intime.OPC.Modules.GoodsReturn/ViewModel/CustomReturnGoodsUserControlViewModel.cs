using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure;
using System.Windows;
using Intime.OPC.Modules.GoodsReturn.Common;
using Intime.OPC.Modules.GoodsReturn.Print;
using System;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (CustomReturnGoodsUserControlViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomReturnGoodsUserControlViewModel : BindableBase
    {
        private IList<RMADto> _rmaDtos;
        private IList<RmaDetail> rmaDetails;
        private RMADto rmaDto;
        private bool _isReturnAcceptCashierVisible;
        private bool _isReturnAcceptCashierConfirm;
        private bool _isPrintButtonVisible;
        private bool _isPrintPreviewButtonVisible;
        private bool _isCompletePrintButtionVisible;
        private bool _isReturnGoodsInStoreButtionVisible;
        private bool _isReturnGoodsConsignedButtonVisible;

        public CustomReturnGoodsUserControlViewModel()
        {
            CommandGetRmaSaleDetailByRma = new AsyncDelegateCommand(GetRmaSaleDetailByRma);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);
            CompletePaymentAcceptanceCommand = new AsyncDelegateCommand(OnCompletePaymentAcceptance, CanActionExecute);
            AcceptPaymentCommand = new AsyncDelegateCommand(OnPaymentAccept, CanActionExecute);
            PreviewReturnGoodsReceiptCommand = new DelegateCommand(() => PrintCommon());
            PrintReturnGoodsReceiptCommand = new DelegateCommand(() => PrintCommon(true));
            CompletePrintCommand = new AsyncDelegateCommand(OnPrintComplete, CanActionExecute);
            ReturnGoodInStockCommand = new AsyncDelegateCommand(OnReturnGoodsInStock, CanActionExecute);
            ConsignReturnGoodsCommand = new AsyncDelegateCommand(OnReturnGoodsConsigned, CanActionExecute);
            ApplyPaymentNumberCommand = new AsyncDelegateCommand(OnPaymentNumberApply, CanPaymentNumberApply);
        }

        public BaseReturnGoodsSearchCommonWithRma MasterViewModel { get; set; }

        public RMADto SelectedRma
        {
            get { return rmaDto; }
            set { SetProperty(ref rmaDto, value); }
        }
        public IList<RMADto> RmaList
        {
            get { return _rmaDtos; }
            set { SetProperty(ref _rmaDtos, value); }
        }
        public IList<RmaDetail> RmaDetailList
        {
            get { return rmaDetails; }
            set { SetProperty(ref rmaDetails, value); }
        }

        #region Button Visibility

        public bool IsReturnGoodsInStoreButtionVisible
        {
            get { return _isReturnGoodsInStoreButtionVisible; }
            set { SetProperty(ref _isReturnGoodsInStoreButtionVisible, value); }
        }

        public bool IsPrintButtonVisible
        {
            get { return _isPrintButtonVisible; }
            set { SetProperty(ref _isPrintButtonVisible, value); }
        }

        public bool IsPrintPreviewButtonVisible
        {
            get { return _isPrintPreviewButtonVisible; }
            set { SetProperty(ref _isPrintPreviewButtonVisible, value); }
        }

        public bool IsCompletePrintButtionVisible
        {
            get { return _isCompletePrintButtionVisible; }
            set { SetProperty(ref _isCompletePrintButtionVisible, value); }
        }

        public bool IsReturnAcceptCashierVisible
        {
            get { return _isReturnAcceptCashierVisible; }
            set { SetProperty(ref _isReturnAcceptCashierVisible, value); }
        }

        public bool IsReturnAcceptCashierConfirmVisible
        {
            get { return _isReturnAcceptCashierConfirm; }
            set { SetProperty(ref _isReturnAcceptCashierConfirm, value); }
        }

        public bool IsReturnGoodsConsignedButtonVisible
        {
            get { return _isReturnGoodsConsignedButtonVisible; }
            set { SetProperty(ref _isReturnGoodsConsignedButtonVisible, value); }
        }

        #endregion

        #region Commands

        public ICommand CommandGetRmaSaleDetailByRma { get; set; }
        public ICommand CommandSetRmaRemark { get; set; }
        public ICommand CompletePaymentAcceptanceCommand { get; set; }
        public ICommand AcceptPaymentCommand { get; set; }
        public ICommand CompletePrintCommand { get; set; }
        public ICommand PrintReturnGoodsReceiptCommand { get; set; }
        public ICommand PreviewReturnGoodsReceiptCommand { get; set; }
        /// <summary>
        ///  退货入库
        /// </summary>
        public ICommand ReturnGoodInStockCommand { get; set; }

        /// <summary>
        /// 迷你银退货收货确认
        /// </summary>
        public ICommand ConsignReturnGoodsCommand { get; set; }
        /// <summary>
        /// 补录收银流水号
        /// </summary>
        public ICommand ApplyPaymentNumberCommand { get; set; }

        #endregion

        private bool CanPaymentNumberApply()
        {
            return SelectedRma != null;
        }

        /// <summary>
        /// 补录收银流水号
        /// </summary>
        private async void OnPaymentNumberApply()
        {
            //如果是迷你银商品
            var cashNumber = await MvvmUtility.ShowInputDialogAsync("补录收银流水号", "收银流水号", SelectedRma.RmaCashNum);
            //用户点击取消按钮
            if (cashNumber == null) return;

            var service = AppEx.Container.GetInstance<IMiniIntimeReturnService>();
            //补录收银流水号
            service.ReceivePayment(SelectedRma, cashNumber);
            SelectedRma.RmaCashNum = cashNumber;
        }

        /// <summary>
        /// 退货收货确认
        /// </summary>
        private void OnReturnGoodsConsigned()
        {
            foreach (var rma in RmaList.Where(rma => rma.IsSelected))
            {
                var service = AppEx.Container.GetInstance<IMiniIntimeReturnService>();
                //收货确认
                service.ConsignReturnGoods(rma);
            }

            ReloadData();
        }

        /// <summary>
        /// 退货入库
        /// </summary>
        private void OnReturnGoodsInStock()
        {
            List<string> rmaList = GetRmaNumberList();
            bool succeeded = AppEx.Container.GetInstance<IGoodsReturnService>().SetReturnGoodsInStorage(rmaList);
            MvvmUtility.ShowMessageAsync(succeeded ? "物流入库成功" : "物流入库失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);
            
            if (succeeded) ReloadData();
        }

        /// <summary>
        /// 退货入收银
        /// </summary>
        private void OnPaymentAccept()
        {
            List<string> listRmaNo = GetRmaNumberList();
            bool succeeded = AppEx.Container.GetInstance<IGoodsReturnService>().SetReturnGoodsCash(listRmaNo);
            MvvmUtility.ShowMessageAsync(succeeded ? "退货入收银成功" : "退货入收银失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);

            if (succeeded) ReloadData();
        }

        /// <summary>
        /// 完成退货入收银
        /// </summary>
        private void OnCompletePaymentAcceptance()
        {
            List<string> listRmaNo = GetRmaNumberList();
            bool succeeded = AppEx.Container.GetInstance<IGoodsReturnService>().SetReturnGoodsComplete(listRmaNo);
            MvvmUtility.ShowMessageAsync(succeeded ? "完成退货入收银成功" : "完成退货入收银失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);

            if (succeeded) ReloadData();
        }

        public void SetRmaRemark()
        {
            string id = SelectedRma.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }

        public void GetRmaSaleDetailByRma()
        {
            if (rmaDto != null)
            {
                RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(rmaDto.RMANo).ToList();
            }
        }

        /// <summary>
        /// 完成退货单打印
        /// </summary>
        private void OnPrintComplete()
        {
            List<string> rmaNos = GetRmaNumberList();
            bool succeeded = AppEx.Container.GetInstance<IGoodsReturnService>().PrintReturnGoodsComplete(rmaNos);
            MvvmUtility.ShowMessageAsync(succeeded ? "设置打印完成状态成功" : "设置打印完成状态失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);

            if (succeeded) ReloadData();
        }

        /// <summary>
        /// 预览或打印退货单
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool PrintCommon(bool flag = false)
        {
            if (SelectedRma == null)
            {
                MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            try
            {
                IPrint pr = new PrintWin();
                string xsdName = "InvoiceDataSet";
                string rdlcName = "Print//PrintRMA.rdlc";

                var invoiceModel = new ReturnGoodsPrintModel();
                invoiceModel.RmaDT = new List<RMADto> { SelectedRma };
                invoiceModel.RMADetailDT = RmaDetailList;
                invoiceModel.OrderDT = AppEx.Container.GetInstance<IPackageService>()
                        .GetOrderByOrderNo(SelectedRma.OrderNo)
                        .ToList();

                pr.PrintReturnGoods(xsdName, rdlcName, invoiceModel, flag);
                return true;
            }
            catch (Exception Ex)
            {
                MvvmUtility.ShowMessageAsync("打印退货单失败，" + Ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private bool CanActionExecute()
        {
            return (RmaList != null && RmaList.Where(rma => rma.IsSelected).Any());
        }

        private List<string> GetRmaNumberList()
        {
            return RmaList.Where(e => e.IsSelected).Select(e => e.RMANo).ToList();
        }

        private void ReloadData()
        {
            if (RmaDetailList != null && RmaDetailList.Count != 0)
            {
                RmaDetailList = null;
            }
            MasterViewModel.QueryRma();
        }
    }
}