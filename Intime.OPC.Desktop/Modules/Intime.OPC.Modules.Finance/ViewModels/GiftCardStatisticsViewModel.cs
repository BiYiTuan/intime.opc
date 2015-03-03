using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Excel;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Modules.Finance.Criteria;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export(typeof(GiftCardStatisticsViewModel))]
    public class GiftCardStatisticsViewModel : ViewModelBase
    {
        [Import]
        private IService<GiftCardStatisticsDto> _service;

        private IList<GiftCardStatisticsDto> _statisticsDtos;

        public IList<KeyValue> Stores { get; set; }

        public IList<KeyValue> PaymentMethods { get; set; }

        public GiftCardStatisticsQueryCriteria QueryCriteria { get; set; }

        public IList<GiftCardStatisticsDto> StatisticsDtos
        {
            get { return this._statisticsDtos; }
            set { SetProperty(ref this._statisticsDtos, value); }
        }

        public ICommand QueryCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        [ImportingConstructor]
        public GiftCardStatisticsViewModel(ICommonInfo dimensionService)
        {
            Stores = dimensionService.GetStoreList();
            PaymentMethods = dimensionService.GetPayMethod();
            QueryCriteria = new GiftCardStatisticsQueryCriteria();

            QueryCommand = new AsyncDelegateCommand(OnQuery);
            ExportCommand = new AsyncDelegateCommand(OnExport);
        }

        private async void OnExport()
        {
            if (StatisticsDtos == null || !StatisticsDtos.Any())
            {
                await MvvmUtility.ShowMessageAsync("没有要导出的礼品卡销售明细");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<GiftCardStatisticsDto>> 
            {
                new ColumnDefinition<GiftCardStatisticsDto>("礼品卡编号",dto => dto.GiftCardNo),
                new ColumnDefinition<GiftCardStatisticsDto>("渠道订单号",dto => dto.TransNo),
                new ColumnDefinition<GiftCardStatisticsDto>("支付方式",dto => dto.PaymentMethodName),
                new ColumnDefinition<GiftCardStatisticsDto>("购买时间",dto => dto.BuyDate),
                new ColumnDefinition<GiftCardStatisticsDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<GiftCardStatisticsDto>("金额",dto => dto.Amount),
                new ColumnDefinition<GiftCardStatisticsDto>("销售金额",dto => dto.SalesAmount),
                new ColumnDefinition<GiftCardStatisticsDto>("是否充值",dto => dto.Recharge)
            };
            ExcelUtility.Export(StatisticsDtos, columnDefinitions, "礼品卡销售明细");
        }

        private void OnQuery()
        {
            StatisticsDtos = _service.QueryAll(QueryCriteria);

            MvvmUtility.WarnIfEmpty(StatisticsDtos, "礼品卡销售明细");
        }
    }
}
