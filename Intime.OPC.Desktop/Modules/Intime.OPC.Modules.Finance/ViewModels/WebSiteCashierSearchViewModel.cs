using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Excel;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Infrastructure;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Finance.Criteria;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export("WebSiteCashierSearchViewModel", typeof (WebSiteCashierSearchViewModel))]
    public class WebSiteCashierSearchViewModel : BindableBase
    {
        [Import]
        private IService<WebSiteCashierSearchDto> _service;

        private CashingDetailQueryCriteria _searchCashierDtos;
        private List<WebSiteCashierSearchDto> _webSiteCashierSearchDtos;

        public WebSiteCashierSearchViewModel()
        {
            InitCombo();
            CommandSearch = new AsyncQueryCommand(Search, () => WebSiteCashierSearchDtos, "对账明细");
            CommandExport = new AsyncDelegateCommand(ExportExcel);
        }

        public CashingDetailQueryCriteria SearchCashierDto
        {
            get { return _searchCashierDtos; }
            set { SetProperty(ref _searchCashierDtos, value); }
        }

        public List<WebSiteCashierSearchDto> WebSiteCashierSearchDtos
        {
            get { return _webSiteCashierSearchDtos; }
            set { SetProperty(ref _webSiteCashierSearchDtos, value); }
        }

        public IList<KeyValue> StoreList { get; set; }
        public IList<KeyValue> PaymentTypeList { get; set; }
        public IList<KeyValue> FinancialTypeList { get; set; }

        public ICommand CommandExport { get; set; }
        public ICommand CommandSearch { get; set; }

        public void InitCombo()
        {
            SearchCashierDto = new CashingDetailQueryCriteria();
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();

            PaymentTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetPayMethod();
            FinancialTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetFinancialTypeList();
        }

        private void Search()
        {
            WebSiteCashierSearchDtos = _service.QueryAll(SearchCashierDto).ToList();
        }

        private async void ExportExcel()
        {
            if (WebSiteCashierSearchDtos == null || !WebSiteCashierSearchDtos.Any())
            {
                await MvvmUtility.ShowMessageAsync("没有要导出的对账明细");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<WebSiteCashierSearchDto>> 
            {
                new ColumnDefinition<WebSiteCashierSearchDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<WebSiteCashierSearchDto>("订单号",dto => dto.OrderNo),
                new ColumnDefinition<WebSiteCashierSearchDto>("渠道订单号",dto => dto.OrderChannelNo),
                new ColumnDefinition<WebSiteCashierSearchDto>("销售单号",dto => dto.SalesOrderNo),
                new ColumnDefinition<WebSiteCashierSearchDto>("支付方式",dto => dto.PaymentMethodName),
                new ColumnDefinition<WebSiteCashierSearchDto>("订单来源",dto => dto.OrderSouce),
                new ColumnDefinition<WebSiteCashierSearchDto>("购买时间",dto => dto.BuyDate),
                new ColumnDefinition<WebSiteCashierSearchDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<WebSiteCashierSearchDto>("品牌",dto => dto.Brand),
                new ColumnDefinition<WebSiteCashierSearchDto>("款号",dto => dto.StyleNo),
                new ColumnDefinition<WebSiteCashierSearchDto>("规格",dto => dto.Size),
                new ColumnDefinition<WebSiteCashierSearchDto>("色码",dto => dto.Color),
                new ColumnDefinition<WebSiteCashierSearchDto>("数量",dto => dto.Count),
                new ColumnDefinition<WebSiteCashierSearchDto>("零售价",dto => dto.LabelPrice),
                new ColumnDefinition<WebSiteCashierSearchDto>("销售价",dto => dto.SalePrice),
                new ColumnDefinition<WebSiteCashierSearchDto>("销售金额",dto => dto.SaleTotalPrice),
                new ColumnDefinition<WebSiteCashierSearchDto>("专柜码",dto => dto.SectionCode),
                new ColumnDefinition<WebSiteCashierSearchDto>("收银流水号",dto => dto.CashNum),
                new ColumnDefinition<WebSiteCashierSearchDto>("退货收银流水号",dto => dto.RmaCashNum)
            };
            ExcelUtility.Export(WebSiteCashierSearchDtos, columnDefinitions, "对账明细");
        }
    }
}