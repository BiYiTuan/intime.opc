using System.Linq;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.GoodsReturn.Common;
using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Customer;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof(MiniIntimeReturnConsignmentViewModel))]
    public class MiniIntimeReturnConsignmentViewModel : BaseReturnGoodsSearchCommonWithRma
    {
        [Import]
        private IMiniIntimeReturnService _service;

        public MiniIntimeReturnConsignmentViewModel()
        {
            CustomReturnGoodsUserControlViewModel.IsReturnGoodsConsignedButtonVisible = true;
        }

        public override void QueryRma()
        {
            var queryCriteria = new MiniIntimeQueryCriteria 
            {
                OrderNo = ReturnGoodsCommonSearchDto.OrderNo,
                StartDate = ReturnGoodsCommonSearchDto.StartDate,
                EndDate = ReturnGoodsCommonSearchDto.EndDate
            };
            CustomReturnGoodsUserControlViewModel.RmaList = _service.QueryAll(queryCriteria);
        }
    }
}
