using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.DataService.Impl.Info
{
    [Export(typeof (ICommonInfo))]
    public class CommonInfo : ICommonInfo
    {
        #region ICommonInfo Members

        public List<ShipVia> GetShipViaList()
        {
            try
            {
                return new List<ShipVia>(RestClient.Get<ShipVia>("shipvia/getall"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<KeyValue> GetStoreList()
        {
            try
            {
                List<KeyValue> lst = RestClient.Get<Store>("store/getall").Select<Store, KeyValue>(t =>
                {
                    var kv = new KeyValue();
                    kv.Key = t.Id;
                    kv.Value = t.Name;
                    return kv;
                }).Distinct().ToList();
                lst.Insert(0, new KeyValue(-1, "全部"));
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<KeyValue> GetBrandList()
        {
            try
            {
                List<KeyValue> lst = RestClient.Get<Brand>("brand/getall").Select<Brand, KeyValue>(t =>
                {
                    var kv = new KeyValue();
                    kv.Key = t.Id;
                    kv.Value = t.Name;
                    return kv;
                }).Distinct().ToList();
                lst.Insert(0, new KeyValue(-1, "全部"));
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<KeyValue> GetOrderStatus()
        {

            try
            {
                return RestClient.Get<KeyValue>("trans/GetOrderStatusEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        /*物流公司*/
        public IList<KeyValue> GetRmaSaleStatus()
        {
            try
            {
                return RestClient.Get<KeyValue>("trans/GetRmaStatusEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<KeyValue> GetSaleOrderStatus()
        {
            try
            {
                return RestClient.Get<KeyValue>("trans/GetSaleStatusEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<KeyValue> GetOutGoodsMehtod()
        {
            try
            {
                return RestClient.Get<KeyValue>("trans/GetShippingTypeEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<KeyValue> GetSectionList()
        {
            try
            {
                List<KeyValue> lst = RestClient.Get<Section>("Section/getall").Select<Section, KeyValue>(t =>
                {
                    var kv = new KeyValue();
                    kv.Key = t.Id;
                    kv.Value = t.Name;
                    return kv;
                }).Distinct().ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<KeyValue> GetPayMethod()
        {
            /*GetPayTypeEnums*/
            try
            {
                return RestClient.Get<KeyValue>("trans/GetPayTypeEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public IList<KeyValue> GetReturnDocStatusList()
        {
            try
            {
                return RestClient.Get<KeyValue>("trans/GetRmaStatusEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //退货类型
        public IList<KeyValue> GetFinancialTypeList()
        {
            try
            {
                return RestClient.Get<KeyValue>("trans/GetFinancialEnums").ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}