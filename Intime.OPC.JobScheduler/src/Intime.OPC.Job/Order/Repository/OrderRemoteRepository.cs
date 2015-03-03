using Common.Logging;
using Intime.O2O.ApiClient;
using Intime.O2O.ApiClient.Domain;
using Intime.O2O.ApiClient.Request;
using Intime.OPC.Domain.Models;
using System.Linq;
using Intime.OPC.Job.Order.DTO;

namespace Intime.OPC.Job.Order.Repository
{
    public class OrderRemoteRepository : IOrderRemoteRepository
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IApiClient _apiClient;

        static OrderRemoteRepository()
        {
            AutoMapper.Mapper.CreateMap<OrderStatusResult, OrderStatusResultDto>();
        }

        public OrderRemoteRepository()
        {
            _apiClient = new DefaultApiClient();
        }

        public OrderRemoteRepository(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public OrderStatusResultDto GetOrderStatusById(OPC_Sale saleOrder)
        {
            string storeno;
            using (var db = new YintaiHZhouContext())
            {
                storeno =
                    db.Map4Store.Where(x => x.Channel == "intime")
                        .Join(db.Sections.Where(x => x.Id == saleOrder.SectionId), m => m.StoreId, s => s.StoreId,
                            (m, s) => m.ChannelStoreId)
                        .FirstOrDefault();
            }

            if (string.IsNullOrEmpty(storeno))
            {
                throw new StockNotExistsException(string.Format(""));
            }

            var result = _apiClient.Post(new GetOrderStatusByIdRequest()
            {
                Data = new
                {
                    id = saleOrder.SaleOrderNo,
                    storeno
                }
            }, true);

            if (result == null)
            {
                Log.Error(string.Join(";", _apiClient.ErrorList()));
                return null;
            }

            if (!result.Status)
            {
                Log.ErrorFormat("查询订单信息失败,message:{0}", result.Message);
                return null;
            }
            return AutoMapper.Mapper.Map<OrderStatusResult, OrderStatusResultDto>(result.Data);
        }

        public OrderStatusResultDto GetRMAStatusById(OPC_RMA saleRMA)
        {
            OPC_Stock opcStock; ;

            using (var db = new YintaiHZhouContext())
            {
                opcStock = db.OPC_Stock.FirstOrDefault(a => a.SectionId == saleRMA.SectionId);
            }

            if (opcStock == null)
            {
                throw new StockNotExistsException(string.Format(""));
            }

            var result = _apiClient.Post(new GetOrderStatusByIdRequest()
            {
                Data = new
                {
                    id = saleRMA.RMANo,
                    storeno = opcStock.StoreCode
                }
            });

            if (!result.Status)
            {
                Log.ErrorFormat("查询订单信息失败,message:{0}", result.Message);
                return null;
            }
            return AutoMapper.Mapper.Map<OrderStatusResult, OrderStatusResultDto>(result.Data);

        }
    }
}
