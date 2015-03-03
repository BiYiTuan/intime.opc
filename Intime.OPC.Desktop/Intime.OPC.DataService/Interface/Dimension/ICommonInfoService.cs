using System.Collections.Generic;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.DataService.Interface.Trans
{
    public interface ICommonInfo
    {
        List<ShipVia> GetShipViaList();
        IList<KeyValue> GetStoreList();
        IList<KeyValue> GetBrandList();

        IList<KeyValue> GetOrderStatus();
        IList<KeyValue> GetPayMethod();
        IList<KeyValue> GetOutGoodsMehtod();
        IList<KeyValue> GetSectionList();
        IList<KeyValue> GetReturnDocStatusList();
        IList<KeyValue> GetFinancialTypeList();

        IList<KeyValue> GetSaleOrderStatus();
        IList<KeyValue> GetRmaSaleStatus();
    }
}