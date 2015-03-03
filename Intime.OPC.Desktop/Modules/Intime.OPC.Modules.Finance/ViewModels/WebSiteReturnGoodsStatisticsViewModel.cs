using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Excel;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Financial;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Service;
using OPCAPP.Domain.Dto.Financial;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    using Intime.OPC.Modules.Finance.Criteria;

    [Export("WebSiteReturnGoodsStatisticsViewModel", typeof (WebSiteReturnGoodsStatisticsViewModel))]
    public class WebSiteReturnGoodsStatisticsViewModel : BindableBase
    {
        [Import]
        private IService<WebSiteReturnGoodsStatisticsDto> _service;

        private StatisticsQueryCrteria _searchCashierDtos;
        private List<WebSiteReturnGoodsStatisticsDto> _webSiteReturnGoodsStatistics;

        public WebSiteReturnGoodsStatisticsViewModel()
        {
            SearchCashierDto = new StatisticsQueryCrteria();
            CommandSearch = new AsyncQueryCommand(Search, () => WebSiteReturnGoodsStatisticsDtos, "退货明细");
            CommandExport = new AsyncDelegateCommand(ExportExcel);

            Initialize();
        }

        public IList<KeyValue> StoreList { get; set; }

        public StatisticsQueryCrteria SearchCashierDto
        {
            get { return _searchCashierDtos; }
            set { SetProperty(ref _searchCashierDtos, value); }
        }

        public List<WebSiteReturnGoodsStatisticsDto> WebSiteReturnGoodsStatisticsDtos
        {
            get { return _webSiteReturnGoodsStatistics; }
            set { SetProperty(ref _webSiteReturnGoodsStatistics, value); }
        }

        public ICommand CommandSearch { get; set; }

        public ICommand CommandExport { get; set; }

        private void Initialize()
        {
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
        }

        private void Search()
        {
            WebSiteReturnGoodsStatisticsDtos = _service.QueryAll(SearchCashierDto).ToList();
        }

        private async void ExportExcel()
        {
            if (WebSiteReturnGoodsStatisticsDtos == null || !WebSiteReturnGoodsStatisticsDtos.Any())
            {
                await MvvmUtility.ShowMessageAsync("没有要导出的退货明细");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<WebSiteReturnGoodsStatisticsDto>> 
            {
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货单号",dto => dto.RMANo),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("订单号",dto => dto.OrderNo),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("渠道订单号",dto => dto.OrderChannelNo),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("支付方式",dto => dto.PaymentMethodName),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("订单来源",dto => dto.OrderSouce),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货状态",dto => dto.RmaStatusName),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("购买时间",dto => dto.BuyDate),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("品牌",dto => dto.Brand),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("款号",dto => dto.StyleNo),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("规格",dto => dto.Size),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("色码",dto => dto.Color),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货数量",dto => dto.ReturnGoodsCount),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("零售价",dto => dto.LabelPrice),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("销售价",dto => dto.SalePrice),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货总金额",dto => dto.RmaAmount),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("专柜码",dto => dto.SectionCode),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货时间",dto => dto.RmaDate),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("退货申请时间",dto => dto.ApplyRmaDate),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("运费",dto => dto.OrderTransFee),
                new ColumnDefinition<WebSiteReturnGoodsStatisticsDto>("销售编码",dto => dto.SalesCode)
            };
            ExcelUtility.Export(WebSiteReturnGoodsStatisticsDtos, columnDefinitions, "退货明细");
        }
    }
}