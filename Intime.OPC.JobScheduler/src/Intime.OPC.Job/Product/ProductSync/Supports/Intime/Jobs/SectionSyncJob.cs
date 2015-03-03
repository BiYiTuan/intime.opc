using System;
using System.Linq;
using System.Threading;
using Common.Logging;
using Intime.O2O.ApiClient;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Mapper;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using Quartz;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs
{
      [DisallowConcurrentExecution]
    public  class SectionSyncJob:IJob
    {
        private const int PageSize = 200;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        RemoteRepository _remoteRepository = new RemoteRepository(new DefaultApiClient());
        private StoreSyncProcessor _storeSyncProcessor;
        private ChannelMapper _channelMapper;

        public SectionSyncJob()
        {
            _channelMapper = new ChannelMapper();
            _storeSyncProcessor = new StoreSyncProcessor(_remoteRepository, _channelMapper);
        }

        public void Sync()
        {
            var pageIndex = 1;
            
            
            while (true)
            {
                var sections =
                    _remoteRepository.GetSectionList(pageIndex++, PageSize, DateTime.Now.AddYears(-2)).ToList();

                if (sections.Count == 0)
                {
                    Log.ErrorFormat("没有可同步的信息,pageIndex:{0},pageSize:{1},lastUpdateDatetime:{2}", pageIndex, PageSize);
                    break;
                }
                foreach (var channelSection in sections)
                {
                    var channelSectionId = channelSection.CounterId;
                    var channelStoreNo = channelSection.StoreNo;
                    var storeExt = _storeSyncProcessor.Sync(channelStoreNo);
                    if (storeExt == null)
                    {
                        continue;
                    }
                    using (var db = new YintaiHZhouContext())
                    {
                        var section =
                            db.Sections.FirstOrDefault(
                                x => x.SectionCode == channelSectionId && x.StoreId == storeExt.Id);
                        if (section != null)
                        {
                            continue;
                        }

                        var newSection = new Section
                        {
                                CreateDate = DateTime.Now,
                                CreateUser = SystemDefine.SystemUser,
                                Location = string.Empty,
                                Name = channelSection.Name,
                                ContactPhone = string.Empty,
                                StoreId = storeExt.Id,
                                SectionCode = channelSectionId,
                                Status = 1,
                                UpdateDate = DateTime.Now,
                                UpdateUser = SystemDefine.SystemUser,
                            };

                            db.Sections.Add(newSection);
                            db.SaveChanges();
                    }
                }
                Thread.Sleep(500);
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            this.Sync();
        }
    }
}
