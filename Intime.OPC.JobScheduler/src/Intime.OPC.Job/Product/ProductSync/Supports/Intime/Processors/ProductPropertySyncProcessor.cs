using System;
using System.Linq;

using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    public class ProductPropertySyncProcessor : IProductPropertySyncProcessor
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IChannelMapper _channelMapper;

        public ProductPropertySyncProcessor(IChannelMapper channelMapper)
        {
            _channelMapper = channelMapper;
        }

        public ProductPropertyValue Sync(int productId, string channelPropertyValueName, string channelPropertyValueId, ProductPropertyType productPropertyType)
        {
            using (var db = new YintaiHZhouContext())
            {

                // 是否为颜色
                var isColor = productPropertyType == ProductPropertyType.Color;

                // 映射类型
                var mapType = isColor ? ChannelMapType.ColorId : ChannelMapType.SizeId;


                /**
                 * 映射商品颜色或尺寸的属性值Id
                 * 映射关系{productId}+{propertyValueId}
                 */
                var mapKey = Utils.GetProductProprtyMapKey(productId, channelPropertyValueId);
                var propertyValueMap = _channelMapper.GetMapByChannelValue(mapKey, mapType);
                var valueDesc = string.IsNullOrWhiteSpace(channelPropertyValueName)
                    ? channelPropertyValueId
                    : channelPropertyValueName;
                if (propertyValueMap == null)
                {
                    // 检查当前商品是否存在颜色或尺寸的属性
                    var propertyExt =
                        GetProductProperty(productId, mapType);

                    if (propertyExt == null)
                    {
                        // 没有映射关系进行创建属性 
                        propertyExt = new ProductProperty()
                        {
                            IsColor = isColor,
                            IsSize = !isColor,
                            ChannelPropertyId = 0,
                            ProductId = productId,
                            PropertyDesc = isColor ? "颜色" : "尺寸",
                            SortOrder = 0,
                            Status = 1,
                            UpdateDate = DateTime.Now,
                            UpdateUser = SystemDefine.SystemUser
                        };

                        db.ProductProperties.Add(propertyExt);

                        // 必须先进性保存，获取上面的Last_Update_id
                        db.SaveChanges();
                    }

                    var ppv =
                        db.ProductPropertyValues.FirstOrDefault(
                            p => p.PropertyId == propertyExt.Id && p.ValueDesc == valueDesc);

                    if (ppv == null)
                    {
                        var newProductPropertyValue = new ProductPropertyValue()
                        {
                            PropertyId = propertyExt.Id,
                            CreateDate = DateTime.Now,
                            Status = 1,
                            UpdateDate = DateTime.Now,
                            ValueDesc = valueDesc
                        };

                        db.ProductPropertyValues.Add(newProductPropertyValue);
                        // 保存属性值
                        db.SaveChanges();
                        ppv = newProductPropertyValue;
                        // 保存映射关系
                        var newChannelMap = new ChannelMap()
                        {
                            LocalId = ppv.Id,
                            ChannnelValue = Utils.GetProductProprtyMapKey(productId, channelPropertyValueId),
                            MapType = mapType
                        };

                        _channelMapper.CreateMap(newChannelMap);
                    }

                    return ppv;
                }

                var propertyValueExt = GetProductPropertyValue(productId, propertyValueMap.LocalId, mapType);

                if (propertyValueExt == null)
                {
                    _channelMapper.RemoveMap(propertyValueMap);
                    Log.ErrorFormat("属性数据错误，映射关系存在，数据不存在, property value id {0}", propertyValueMap.LocalId);
                    return null;
                }

                propertyValueExt.ValueDesc = valueDesc;
                propertyValueExt.UpdateDate = DateTime.Now;

                db.SaveChanges();

                return propertyValueExt;
            }

        }

        private ProductProperty GetProductProperty(int productId, ChannelMapType mapType)
        {
            using (var db = new YintaiHZhouContext())
            {
                if (mapType == ChannelMapType.ColorId)
                {
                    return db.ProductProperties.FirstOrDefault(p => p.ProductId == productId && p.IsColor.HasValue && p.IsColor.Value);
                }

                return db.ProductProperties.FirstOrDefault(p => p.ProductId == productId && p.IsSize.HasValue && p.IsSize.Value);
            }
        }

        private ProductPropertyValue GetProductPropertyValue(int productId, int propertyValueId, ChannelMapType mapType)
        {
            using (var db = new YintaiHZhouContext())
            {
                if (mapType == ChannelMapType.ColorId)
                {
                    return db.ProductProperties.Where(p => p.IsColor.HasValue && p.IsColor.Value && p.ProductId == productId)
                    .Join(db.ProductPropertyValues, p => p.Id, p => p.PropertyId, (o, p) => p).FirstOrDefault(p => p.Id == propertyValueId);
                }

                return db.ProductProperties.Where(p => p.IsSize.HasValue && p.IsSize.Value && p.ProductId == productId)
                .Join(db.ProductPropertyValues, p => p.Id, p => p.PropertyId, (o, p) => p).FirstOrDefault(p => p.Id == propertyValueId);
            }
        }
    }
}
