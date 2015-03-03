using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Intime.OPC.Job.IMS
{
    public class ImsInitializeJob : IJob
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        public void Execute(IJobExecutionContext context)
        {
            int total = 0;
            int succeedCount = 0;
            DoQuery(requests => total = requests.Count());

            int cursor = 0;

            JobDataMap data = context.JobDetail.JobDataMap;
            int size = data.ContainsKey("pageSize") ? data.GetInt("pageSize") : 10;

            while (true)
            {
                List<IMS_InviteCodeRequest> oneTimeList = null;
                DoQuery(requests => oneTimeList = requests.OrderBy(x => x.Id).Skip(cursor).Take(size).ToList());
                if (!oneTimeList.Any())
                {
                    break;
                }
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
                var result = oneTimeList.AsParallel().Select(x => Task.Factory.StartNew(() => Initialize(x))).ToArray();
                Task.WaitAll(result);
                succeedCount += result.Count(x => x.Result);
                cursor += size;
            }
        }

        private bool Initialize(IMS_InviteCodeRequest request)
        {
            using (var ts = new TransactionScope())
            using (var db = new YintaiHZhouContext())
            {
                var user = db.Set<User>().FirstOrDefault(x => x.Id == request.UserId);
                if (user == null)
                {
                    Logger.ErrorFormat("Invalid request {0}, user not exists", request.Id);
                    return false;
                }

                var requestEntity = db.Set<IMS_InviteCodeRequest>().FirstOrDefault(x => x.Id == request.Id);
                if (requestEntity == null)
                {
                    Logger.ErrorFormat("Invalid request! id {0}",request.Id);
                    return false;
                }

                if (requestEntity.Approved.HasValue)
                {
                    Logger.ErrorFormat("Entity has been approved! {0}", request.Id);
                    return false;
                }

                requestEntity.Approved = true;
                requestEntity.ApprovedDate = DateTime.Now;
                requestEntity.ApprovedBy = SystemDefine.SystemUser;
                requestEntity.UpdateDate = DateTime.Now;
                requestEntity.UpdateUser = SystemDefine.SystemUser;
                requestEntity.Status = 10;
                db.Entry(requestEntity).State = EntityState.Modified;

                if (user.UserLevel != 4)
                {
                    user.UserLevel = 4;
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedUser = SystemDefine.SystemUser;
                    user.Logo = ConfigurationManager.AppSettings["IMS_Default_LOGO"];
                    user.Mobile = request.ContactMobile;
                    db.Entry(user).State = EntityState.Modified;
                }

                var associate = db.Set<IMS_Associate>().FirstOrDefault(x => x.UserId == request.UserId);
                if (associate == null)
                {
                    associate = CreateAssociate(request);
                }
                else
                {
                    //1:giftcard;2:systemproduct;4:self product
                    associate.OperateRight = request.RequestType == 2 ? 7 : 3;
                    db.Entry(associate).State = EntityState.Modified;
                }

                var initialBrands = db.Set<IMS_SectionBrand>().Where(x => x.SectionId == associate.SectionId);
                foreach (var brand in initialBrands)
                {
                    if(db.Set<IMS_AssociateBrand>().Any(x=>x.AssociateId == associate.Id && x.BrandId == brand.BrandId && x.Status == 1))
                        continue;
                    db.Set<IMS_AssociateBrand>().Add(new IMS_AssociateBrand
                    {
                        AssociateId = associate.Id,
                        BrandId = brand.BrandId,
                        CreateDate = DateTime.Now,
                        CreateUser = SystemDefine.SystemUser,
                        Status = 1,
                        UserId = request.UserId
                    });
                }

                var initialSaleCodes = db.Set<IMS_SalesCode>().Where(x => x.SectionId == associate.SectionId);
                foreach (var saleCode in initialSaleCodes)
                {
                    if(db.Set<IMS_AssociateSaleCode>().Any(x=>x.AssociateId == associate.Id && x.Code == saleCode.Code && x.Status == 1))
                        continue;
                    db.Set<IMS_AssociateSaleCode>().Add(new IMS_AssociateSaleCode
                    {
                        AssociateId = associate.Id,
                        Code = saleCode.Code,
                        CreateDate = DateTime.Now,
                        CreateUser = SystemDefine.SystemUser,
                        Status = 1,
                        UserId = request.UserId

                    });
                }
                if (!db.Set<IMS_AssociateItems>().Any(x => x.AssociateId == associate.Id && x.Status == 1))
                {
                    var giftCards = db.Set<IMS_GiftCard>().FirstOrDefault(x => x.Status == 1);
                    if (giftCards != null)
                    {
                        db.Set<IMS_AssociateItems>().Add(new IMS_AssociateItems
                        {
                            AssociateId = associate.Id,
                            CreateDate = DateTime.Now,
                            CreateUser = SystemDefine.SystemUser,
                            ItemId = giftCards.Id,
                            ItemType = 1,
                            Status = 1,
                            UpdateDate = DateTime.Now,
                            UpdateUser = request.UserId
                        });
                    }
                }
                
                db.SaveChanges();
                ts.Complete();
            }
            return true;
        }

        private IMS_Associate CreateAssociate(IMS_InviteCodeRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                var section =
                    db.Set<Section>()
                        .FirstOrDefault(x => x.SectionCode == request.SectionCode && x.StoreId == request.StoreId);
                if (section == null)
                {
                    return null;
                }

                var associate = new IMS_Associate()
                {
                    CreateDate = DateTime.Now,
                    CreateUser = request.UserId,
                    OperateRight = request.RequestType == 2 ? 7 : 3,
                    SectionId = section.Id,
                    Status = 1,
                    StoreId = request.StoreId,
                    UserId =  request.UserId,
                    TemplateId = int.Parse(ConfigurationManager.AppSettings["IMS_Default_Template"]),
                    OperatorCode = request.OperatorCode,

                };

                associate = db.Set<IMS_Associate>().Add(associate);
                db.SaveChanges();
                return associate;
            }
        }


        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Error(e);
            e.SetObserved();
        }


        private void DoQuery(Action<IQueryable<IMS_InviteCodeRequest>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq = context.Set<IMS_InviteCodeRequest>().Where(x => !x.Approved.HasValue);
                if (callback != null)
                {
                    callback(linq);
                }
            }
        }
    }
}
