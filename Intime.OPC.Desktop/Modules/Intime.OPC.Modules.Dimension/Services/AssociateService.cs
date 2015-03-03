using System.ComponentModel.Composition;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Services
{
    /// <summary>
    /// The associate service.
    /// </summary>
    [Export(typeof(IAssociateService))]
    public class AssociateService : ServiceBase<Associate>, IAssociateService
    {
        /// <summary>
        /// The demote.
        /// </summary>
        /// <param name="associate">
        /// The associate.
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        public void Demote(Associate associate)
        {
            string uri = string.Format("{0}/{1}/demotion", UriName, associate.Id);
            Update(uri);
        }

        /// <summary>
        /// The notify.
        /// </summary>
        /// <param name="associate">
        /// The associate.
        /// </param>
        /// <param name="notificationTimes">
        /// The notification times.
        /// </param>
        public void Notify(Associate associate, int notificationTimes = 1)
        {
            string uri = string.Format("{0}/{1}/notify", UriName, associate.Id);
            Update(uri, new { Times = notificationTimes });
        }
    }
}
