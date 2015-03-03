using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Services
{
    /// <summary>
    /// 开店申请审核Service
    /// </summary>
    public interface IStoreApplicationService : IService<ApplicationInfo>
    {
        /// <summary>
        /// 批准申请
        /// </summary>
        /// <param name="applicationInfo">申请信息</param>
        void Approve(ApplicationInfo applicationInfo);
    }
}
