using System;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Infrastructure.Events
{
    public class NavigatingToViewEventArgs
    {
        private readonly Tuple<OPC_AuthMenu, Action> tuple;

        public NavigatingToViewEventArgs(OPC_AuthMenu authorizedMenu, Action navigatedCallback)
        {
            tuple = new Tuple<OPC_AuthMenu, Action>(authorizedMenu, navigatedCallback);
        }

        public OPC_AuthMenu AuthorizedMenu
        {
            get
            {
                return tuple.Item1;
            }
        }

        public Action NavigatedCallback
        {
            get
            {
                return tuple.Item2;
            }
        }
    }
}
