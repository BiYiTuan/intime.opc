using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    public class ProductPropertySyncHandler : IProductPropertySyncHandler
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IChannelMapper _channelMapper;

        public ProductPropertySyncHandler(IChannelMapper channelMapper)
        {
            _channelMapper = channelMapper;
        }
        public ProductPropertyValue SyncColor(int productId, ProductDto productDto)
        {
            using (var db = new YintaiHZhouContext())
            {
                // 检查当前商品是否存在颜色属性
                var propertyExt =
                    db.ProductProperties.FirstOrDefault(
                        p => p.ProductId == productId && p.IsColor.HasValue && p.IsColor.Value);

                if (propertyExt == null)
                {
                    // 没有映射关系进行创建属性 
                    propertyExt = new ProductProperty()
                    {
                        IsColor = true,
                        IsSize = false,
                        ChannelPropertyId = 0,
                        ProductId = productId,
                        PropertyDesc = "颜色",
                        SortOrder = 0,
                        Status = 1,
                        UpdateDate = DateTime.Now,
                        UpdateUser = SystemDefine.SystemUser
                    };

                    db.ProductProperties.Add(propertyExt);
                    db.SaveChanges();

                }
                else
                {
                    var ppvMapKey = string.Format("{0}/{1}", productId, productDto.ColorId);
                    var ppvMap = _channelMapper.GetMapByChannelValue(ppvMapKey, ChannelMapType.ProductId4Color);
                    if (ppvMap == null)
                    {
                        var newProductPropertyValue = new ProductPropertyValue()
                        {
                            PropertyId = propertyExt.Id,
                            CreateDate = DateTime.Now,
                            Status = 1,
                            UpdateDate = DateTime.Now,
                            ValueDesc = productDto.ColorId
                        };

                        db.ProductPropertyValues.Add(newProductPropertyValue);
                        // 保存属性值
                        db.SaveChanges();

                        // 保存映射关系
                        var newChannelMap = new ChannelMap()
                        {
                            LocalId = newProductPropertyValue.Id,
                            ChannnelValue = ppvMapKey,
                            MapType = ChannelMapType.ProductId4Color
                        };

                        _channelMapper.CreateMap(newChannelMap);
                        return newProductPropertyValue;
                    }
                    var ppv = db.ProductPropertyValues.FirstOrDefault(x => x.Id == ppvMap.LocalId);
                    if (ppv == null) return null;
                    if (ppv.ValueDesc != productDto.ColorId)
                    {
                        ppv.ValueDesc = productDto.ColorId;
                        ppv.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    return ppv;
                }
            }

            return null;
        }

        public ProductPropertyValue SyncSize(int productId, ProductDto productDto)
        {
            using (var db = new YintaiHZhouContext())
            {
                // 检查当前商品是否存在尺码属性
                var propertyExt =
                    db.ProductProperties.FirstOrDefault(
                        p => p.ProductId == productId && p.IsSize.HasValue && p.IsSize.Value);

                if (propertyExt == null)
                {
                    // 创建属性 
                    propertyExt = new ProductProperty()
                    {
                        IsColor = false,
                        IsSize = true,
                        ChannelPropertyId = 0,
                        ProductId = productId,
                        PropertyDesc = "尺码",
                        SortOrder = 0,
                        Status = 1,
                        UpdateDate = DateTime.Now,
                        UpdateUser = SystemDefine.SystemUser
                    };

                    db.ProductProperties.Add(propertyExt);
                    db.SaveChanges();

                }
                else
                {
                    var ppvMapKey = productDto.ProductId;
                    var ppvMap = _channelMapper.GetMapByChannelValue(ppvMapKey, ChannelMapType.ProductId4Size);
                    if (ppvMap == null)
                    {
                        var newProductPropertyValue = new ProductPropertyValue()
                        {
                            PropertyId = propertyExt.Id,
                            CreateDate = DateTime.Now,
                            Status = 1,
                            UpdateDate = DateTime.Now,
                            ValueDesc = productDto.ColorId
                        };

                        db.ProductPropertyValues.Add(newProductPropertyValue);
                        // 保存属性值
                        db.SaveChanges();

                        // 保存映射关系
                        var newChannelMap = new ChannelMap()
                        {
                            LocalId = newProductPropertyValue.Id,
                            ChannnelValue = ppvMapKey,
                            MapType = ChannelMapType.ProductId4Size
                        };

                        _channelMapper.CreateMap(newChannelMap);
                        return newProductPropertyValue;
                    }
                    var ppv = db.ProductPropertyValues.FirstOrDefault(x => x.Id == ppvMap.LocalId);
                    if (ppv == null) return null;
                    if (ppv.ValueDesc != productDto.SizeId)
                    {
                        ppv.ValueDesc = productDto.SizeId;
                        ppv.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    return ppv;
                }
            }

            return null;
        }
    }
}
