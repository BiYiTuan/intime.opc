using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using System;
using System.Linq;
/*
 * 去掉channelmapper来映射专柜码是门店ID 
 * 
 * 
 * */
namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// 门店同步处理
    /// </summary>
    public class SectionSyncProcessor : ISectionSyncProcessor
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IRemoteRepository _remoteRepository;
        private readonly IStoreSyncProcessor _storeSyncProcessor;

        public SectionSyncProcessor(IRemoteRepository remoteRepository, IStoreSyncProcessor storeSyncProcessor, IChannelMapper channelMapper)
        {
            _remoteRepository = remoteRepository;
            _storeSyncProcessor = storeSyncProcessor;
        }

        public Section Sync(string channelSectionId, string channelStoreNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                // 检查专柜所属门店信息
                var storeExt = _storeSyncProcessor.Sync(channelStoreNo);
                if (storeExt == null)
                {
                    Log.ErrorFormat("同步专柜时发生错误,门店storeNo:{0}同步失败", channelStoreNo);
                    return null;
                }

                var channelSection = _remoteRepository.GetSectionById(channelSectionId, channelStoreNo);
                if (channelSection == null)
                {
                    Log.ErrorFormat("远程获取专柜信息出错，counterId:{0},storeNo:{1}", channelSectionId, channelStoreNo);
                    return null;
                }
                var section =
                    db.Sections.FirstOrDefault(x => x.SectionCode == channelSectionId && x.StoreId == storeExt.Id);
                if (section == null)
                {
                    section = db.Sections.Add(new Section()
                    {
                        CreateDate = DateTime.Now,
                        CreateUser = SystemDefine.SystemUser,
                        Location = string.Empty,
                        Name = channelSection.Name,
                        ContactPhone = string.Empty,
                        SectionCode = channelSection.CounterId,
                        StoreId = storeExt.Id,
                        Status = 1,
                        UpdateDate = DateTime.Now,
                        UpdateUser = SystemDefine.SystemUser,
                    });
                }
                else
                {
                    if (channelSection.Status.ToUpper() != "Y")
                    {
                        section.Status = -1;
                        section.UpdateDate = DateTime.Now;
                        section.UpdateUser = SystemDefine.SystemUser;
                    }
                    else
                    {
                        if (section.Status != 1)
                        {
                            section.Status = 1;
                            section.UpdateDate = DateTime.Now;
                            section.UpdateUser = SystemDefine.SystemUser;
                        }
                    }
                }
                db.SaveChanges();
                return section;
            }
        }
    }
}
