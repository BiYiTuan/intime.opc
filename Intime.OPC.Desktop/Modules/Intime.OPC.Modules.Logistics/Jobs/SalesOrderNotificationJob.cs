using System;
using System.ComponentModel.Composition;
using Quartz;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Modules.Logistics.Models;
using Intime.OPC.Infrastructure.Job;
using Intime.OPC.Infrastructure.Mvvm.Toast;
using Intime.OPC.Infrastructure.Events;
using Intime.OPC.Modules.Logistics.Events;

namespace Intime.OPC.Modules.Logistics.Jobs
{
    [Export(typeof(IJob))]
    [JobHook(MatchedAuthorizedViewName = "PrintInvoice",Interval = 5)]
    public class SalesOrderNotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var service = AppEx.Container.GetInstance<ILogisticsService>();
            var queryCriteria = new Invoice4Get();
            var queryString = BuildQueryString(queryCriteria);

            try
            {
                var salesOrders = service.Search(queryString, EnumSearchSaleStatus.CompletePrintSearchStatus);
                if (salesOrders == null || salesOrders.TotalCount == 0)
                {
                    return;
                }

                var viewMenu = context.MergedJobDataMap["AuthorizedMenu"] as OPC_AuthMenu;
                if (viewMenu == null) return;

                ToastManager.ShowToast(string.Format("今天尚有 {0} 个销售单未处理。", salesOrders.TotalCount), () => PublishNavigatingEvent(viewMenu, queryCriteria));
            }
            catch (Exception exception)
            {
                ToastManager.ShowToast(string.Format("查询销售单时发生未知错误。\n{0}", exception.Message));
            }
        }

        private string BuildQueryString(Invoice4Get queryCriteria)
        {
            return string.Format("startdate={0}&enddate={1}&orderno={2}&saleorderno={3}&pageIndex={4}&pageSize={5}",
                queryCriteria.StartSellDate.ToShortDateString(),
                queryCriteria.EndSellDate.ToShortDateString(),
                queryCriteria.OrderNo, queryCriteria.SaleOrderNo, 1, 50);
        }

        private void PublishNavigatingEvent(OPC_AuthMenu viewMenu, Invoice4Get queryCriteria)
        {
            var eventAggregator = AppEx.Container.GetInstance<GlobalEventAggregator>();
            eventAggregator.GetEvent<NavigatingToViewEvent>().Publish(new NavigatingToViewEventArgs(viewMenu, () => PublishSalesOrderDetectedEvent(queryCriteria)));
        }

        private void PublishSalesOrderDetectedEvent(Invoice4Get queryCriteria)
        {
            var eventAggregator = AppEx.Container.GetInstance<GlobalEventAggregator>();
            eventAggregator.GetEvent<UnhandledSalesOrderDetectedEvent>().Publish(queryCriteria);
        }
    }
}
