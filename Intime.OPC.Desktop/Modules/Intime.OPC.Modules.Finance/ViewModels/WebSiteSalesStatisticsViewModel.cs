using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Excel;
using OPCAPP.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Finance.Criteria;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export("WebSiteSalesStatisticsViewModel", typeof (WebSiteSalesStatisticsViewModel))]
    public class WebSiteSalesStatisticsViewModel : BindableBase
    {
        [Import]
        private IService<WebSiteSalesStatisticsDto> _service;

        private StatisticsQueryCrteria _searchCashierDtos;
        private List<WebSiteSalesStatisticsDto> _webSiteSalesStatisticsDtos;

        public WebSiteSalesStatisticsViewModel()
        {
            SearchCashierDto = new StatisticsQueryCrteria();
            CommandSearch = new AsyncQueryCommand(Search, () => WebSiteSalesStatisticsDtos, "销售明细");
            CommandExport = new AsyncDelegateCommand(ExportExcel);

            Initialize();
        }

        public IList<KeyValue> StoreList { get; set; }

        public StatisticsQueryCrteria SearchCashierDto
        {
            get { return _searchCashierDtos; }
            set { SetProperty(ref _searchCashierDtos, value); }
        }

        public List<WebSiteSalesStatisticsDto> WebSiteSalesStatisticsDtos
        {
            get { return _webSiteSalesStatisticsDtos; }
            set { SetProperty(ref _webSiteSalesStatisticsDtos, value); }
        }

        public ICommand CommandSearch { get; set; }
        public ICommand CommandExport { get; set; }

        private void Initialize()
        {
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
        }

        private void Search()
        {
            WebSiteSalesStatisticsDtos = _service.QueryAll(SearchCashierDto).ToList();
        }

        private async void ExportExcel()
        {
            if (WebSiteSalesStatisticsDtos == null || !WebSiteSalesStatisticsDtos.Any())
            {
                await MvvmUtility.ShowMessageAsync("没有要导出的销售明细");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<WebSiteSalesStatisticsDto>> 
            {
                new ColumnDefinition<WebSiteSalesStatisticsDto>("订单号",dto => dto.OrderNo),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("渠道订单号",dto => dto.OrderChannelNo),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("销售单号",dto => dto.SalesOrderNo),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("支付方式",dto => dto.PaymentMethodName),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("订单来源",dto => dto.OrderSouce),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("购买时间",dto => dto.BuyDate),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("品牌",dto => dto.Brand),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("款号",dto => dto.StyleNo),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("规格",dto => dto.Size),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("色码",dto => dto.Color),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("数量",dto => dto.SellCount),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("零售价",dto => dto.LabelPrice),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("销售价",dto => dto.SalePrice),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("销售金额",dto => dto.SaleTotalPrice),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("专柜码",dto => dto.SectionCode),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("运费",dto => dto.OrderTransFee),
                new ColumnDefinition<WebSiteSalesStatisticsDto>("销售编码",dto => dto.SalesCode)
            };
            ExcelUtility.Export(WebSiteSalesStatisticsDtos, columnDefinitions, "销售明细");
        }
    }
}