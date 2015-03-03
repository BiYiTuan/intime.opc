using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Dimension.Services
{
    public interface IAssociateService : IService<Associate>
    {
        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="associate">导购</param>
        /// <param name="reason">原因</param>
        void Demote(Associate associate);

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="associate">导购</param>
        /// <param name="notificationTimes">通知次数</param>
        void Notify(Associate associate, int notificationTimes = 1);
    }
}
