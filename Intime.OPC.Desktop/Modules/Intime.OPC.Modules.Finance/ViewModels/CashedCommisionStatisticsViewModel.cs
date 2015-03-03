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
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Modules.Finance.Criteria;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export(typeof(CashedCommisionStatisticsViewModel))]
    public class CashedCommisionStatisticsViewModel : ViewModelBase
    {
        [Import]
        private IService<CashedCommissionStatisticsDto> _service;

        private IList<CashedCommissionStatisticsDto> _statisticsDtos;

        public IList<KeyValue> Stores { get; set; }

        public IList<Bank> Banks { get; set; }

        public CashedCommisionStatisticsQueryCriteria QueryCriteria { get; set; }

        public IList<CashedCommissionStatisticsDto> StatisticsDtos
        {
            get { return this._statisticsDtos; }
            set { SetProperty(ref this._statisticsDtos, value); }
        }

        public ICommand QueryCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        [ImportingConstructor]
        public CashedCommisionStatisticsViewModel(ICommonInfo dimensionService, IService<Bank> bankService)
        {
            Stores = dimensionService.GetStoreList();
            Banks = bankService.QueryAll(new QueryAll());
            QueryCriteria = new CashedCommisionStatisticsQueryCriteria();

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

            var columnDefinitions = new List<ColumnDefinition<CashedCommissionStatisticsDto>> 
            {
                new ColumnDefinition<CashedCommissionStatisticsDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<CashedCommissionStatisticsDto>("专柜名称",dto => dto.SectionName),
                new ColumnDefinition<CashedCommissionStatisticsDto>("专柜码",dto => dto.SectionCode),
                new ColumnDefinition<CashedCommissionStatisticsDto>("迷你银账号",dto => dto.MiniSilverNo),
                new ColumnDefinition<CashedCommissionStatisticsDto>("提取时间",dto => dto.PickUpDate),
                new ColumnDefinition<CashedCommissionStatisticsDto>("提取金额",dto => dto.PickUpAmount),
                new ColumnDefinition<CashedCommissionStatisticsDto>("提取人",dto => dto.PickUpPerson),
                new ColumnDefinition<CashedCommissionStatisticsDto>("提取人联系方式",dto => dto.Contact),
                new ColumnDefinition<CashedCommissionStatisticsDto>("所属银行",dto => dto.BankName),
                new ColumnDefinition<CashedCommissionStatisticsDto>("银行卡号",dto => dto.BankCardNo),
                new ColumnDefinition<CashedCommissionStatisticsDto>("手续费",dto => dto.Fee),
                new ColumnDefinition<CashedCommissionStatisticsDto>("税金",dto => dto.Taxes)
            };
            ExcelUtility.Export(StatisticsDtos, columnDefinitions, "已提取佣金明细");
        }

        private void OnQuery()
        {
            StatisticsDtos = _service.QueryAll(QueryCriteria);

            MvvmUtility.WarnIfEmpty(StatisticsDtos, "佣金明细");
        }
    }
}
