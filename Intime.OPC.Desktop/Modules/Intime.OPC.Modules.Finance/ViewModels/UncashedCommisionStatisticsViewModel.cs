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
    [Export(typeof(UncashedCommisionStatisticsViewModel))]
    public class UncashedCommisionStatisticsViewModel : ViewModelBase
    {
        [Import]
        private IService<UncashedCommissionStatisticsDto> _service;
        private IList<UncashedCommissionStatisticsDto> _statisticsDtos;

        public IList<KeyValue> Stores { get; set; }

        public UncashedCommisionStatisticsQueryCriteria QueryCriteria { get; set; }

        public IList<UncashedCommissionStatisticsDto> StatisticsDtos
        {
            get { return this._statisticsDtos; }
            set { SetProperty(ref this._statisticsDtos, value); }
        }

        public ICommand QueryCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        [ImportingConstructor]
        public UncashedCommisionStatisticsViewModel(ICommonInfo dimensionService)
        {
            Stores = dimensionService.GetStoreList();
            QueryCriteria = new UncashedCommisionStatisticsQueryCriteria();

            QueryCommand = new AsyncDelegateCommand(OnQuery);
            ExportCommand = new AsyncDelegateCommand(OnExport);
        }

        private async void OnExport()
        {
            if (StatisticsDtos == null || !StatisticsDtos.Any())
            {
                await MvvmUtility.ShowMessageAsync("没有要导出的佣金明细");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<UncashedCommissionStatisticsDto>> 
            {
                new ColumnDefinition<UncashedCommissionStatisticsDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("专柜名称",dto => dto.SectionName),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("专柜码",dto => dto.SectionCode),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("迷你银账号",dto => dto.MiniSilverNo),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("联系方式",dto => dto.Contact),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("未提现金额",dto => dto.NoPickUpAmount),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("不可提现金额",dto => dto.LockedPickUpAmount),
                new ColumnDefinition<UncashedCommissionStatisticsDto>("申请中金额",dto => dto.ApplicationPickUpAmount)
            };
            ExcelUtility.Export(StatisticsDtos, columnDefinitions, "未提取佣金明细");
        }

        private void OnQuery()
        {
            StatisticsDtos = _service.QueryAll(QueryCriteria);

            MvvmUtility.WarnIfEmpty(StatisticsDtos, "佣金明细");
        }
    }
}
