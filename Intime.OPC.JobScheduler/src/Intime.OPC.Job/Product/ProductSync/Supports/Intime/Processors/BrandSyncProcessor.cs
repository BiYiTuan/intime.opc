using System;
using System.Linq;

using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// 品牌同步处理器
    /// </summary>
    public class BrandSyncProcessor : IBrandSyncProcessor
    {
        private readonly IRemoteRepository _remoteRepository;
        private readonly IChannelMapper _channelMapper;

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public BrandSyncProcessor(IRemoteRepository remoteRepository, IChannelMapper channelMapper)
        {
            _remoteRepository = remoteRepository;
            _channelMapper = channelMapper;
        }

        public Brand SyncBrand(string channelBrandId, string channelBrandName)
        {
            if (string.IsNullOrWhiteSpace(channelBrandId))
            {

                Log.WarnFormat("品牌Id参数错误，brandId:{0}", channelBrandId);
                return null;
            }

            /**
            * 品牌同步策略:
            * 查找是否存在记录，如果不存在记录进行本地创建，存在进行更新
            */
            using (var db = new YintaiHZhouContext())
            {

                // 查找Brand映射关系
                var brandMapExt = _channelMapper.GetMapByChannelValue(channelBrandId, ChannelMapType.Brand);

                var brandSource = _remoteRepository.GetBrandById(channelBrandId);

                if (brandSource == null)
                {
                    Log.ErrorFormat("远程获取品牌接口失败，brandId:{0}", channelBrandId);
                    return null;
                }


                if (brandMapExt == null)
                {
                    /**
                     * 品牌映射关系不存在，
                     * 
                     * 1. 创建新品牌
                     * 2. 保存映射关系
                     */

                    var newBrand = new Brand()
                    {
                        ChannelBrandId = brandSource.BrandId,
                        Name = brandSource.Name,
                        EnglishName = string.Empty,
                        Logo = string.Empty,
                        Status = 1,
                        Description = string.Empty,
                        Group = string.Empty,
                        WebSite = string.Empty,
                        CreatedDate = DateTime.Now,
                        CreatedUser = SystemDefine.SystemUser,
                        UpdatedDate = DateTime.Now,
                        UpdatedUser = SystemDefine.SystemUser,
                    };

                    // 添加品牌并保持
                    db.Brands.Add(newBrand);
                    db.SaveChanges();

                    // 添加映射关系
                    var newChannelMap = new ChannelMap()
                    {
                        LocalId = newBrand.Id,
                        ChannnelValue = channelBrandId,
                        MapType = ChannelMapType.Brand
                    };

                    _channelMapper.CreateMap(newChannelMap);

                    return newBrand;
                }


                /**
                 * 品牌映射关系存在
                 * 1. 根据本地Id查找品牌
                 * 2. 更新品牌名
                 */
                var brandExt = db.Brands.FirstOrDefault(b => b.Id == brandMapExt.LocalId);

                // 更新品牌
                if (brandExt != null)
                {
                    brandExt.Name = brandSource.Name;
                    brandExt.Status = 1;
                    db.SaveChanges();
                }

                return brandExt;
            }
        }
    }
}
