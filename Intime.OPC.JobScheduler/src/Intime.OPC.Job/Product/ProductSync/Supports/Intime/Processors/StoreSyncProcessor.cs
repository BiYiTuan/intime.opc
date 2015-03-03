using System;
using System.Linq;

using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// 门店同步处理器
    /// </summary>
    public class StoreSyncProcessor : IStoreSyncProcessor
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IRemoteRepository _remoteRepository;
        private readonly IChannelMapper _channelMapper;

        public StoreSyncProcessor(IRemoteRepository remoteRepository, IChannelMapper channelMapper)
        {
            _remoteRepository = remoteRepository;
            _channelMapper = channelMapper;
        }

        public Store Sync(string channelStoreNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                /**
                 * 门店同步逻辑
                 * 1. 查找是否存在映射关系
                 * 2. 更新或创建本地门店信息
                 */

                // 获取映射关系
                var storeNoMap = _channelMapper.GetMapByChannelValue(channelStoreNo, ChannelMapType.StoreNo);

                //获取远程接口的门店信息
                var storeSource = _remoteRepository.GetStoreById(channelStoreNo);

                if (storeSource == null)
                {
                    Log.ErrorFormat("获取远程接口门店信息出错,storeNo:{0}", channelStoreNo);
                    return null;
                }

                if (storeNoMap == null)
                {
                    var newStore = new Store()
                    {
                        Description = string.Empty,
                        Location = string.Empty,
                        Name = storeSource.Name ?? string.Empty,
                        RMAAddress = storeSource.Address ?? string.Empty,
                        RMAPerson = string.Empty,
                        RMAPhone = storeSource.Tel ?? string.Empty,
                        RMAZipCode = string.Empty,
                        Status = 1,
                        Tel = storeSource.Tel ?? string.Empty,
                        CreatedDate = DateTime.Now,
                        CreatedUser = SystemDefine.SystemUser,
                        UpdatedDate = DateTime.Now,
                        UpdatedUser = SystemDefine.SystemUser,
                    };

                    db.Stores.Add(newStore);
                    db.SaveChanges();

                    Log.TraceFormat("创建新门店信息,storeNo:{0}", channelStoreNo);

                    // 保存映射关系
                    var channalMap = new ChannelMap()
                    {
                        LocalId = newStore.Id,
                        ChannnelValue = channelStoreNo,
                        MapType = ChannelMapType.StoreNo
                    };

                    _channelMapper.CreateMap(channalMap);

                    Log.TraceFormat("创建门店映射关系,localNo:[{0}],channelNo:[{1}]", newStore.Id, channelStoreNo);

                    return newStore;
                }

                var storeExt = db.Stores.FirstOrDefault(s => s.Id == storeNoMap.LocalId);

                if (storeExt != null)
                {
                    storeExt.UpdatedDate = DateTime.Now;
                    storeExt.UpdatedUser = SystemDefine.SystemUser;
                    storeExt.Name = storeSource.Name;
                    if (!string.IsNullOrEmpty(storeSource.Address))
                    {
                        storeExt.RMAAddress = storeSource.Address;
                    }
                    if (!string.IsNullOrEmpty(storeSource.Tel))
                    {
                        storeExt.Tel = storeSource.Tel;
                    }

                    db.SaveChanges();
                }

                return storeExt;
            }
        }
    }
}
