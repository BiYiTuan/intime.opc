using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Domain
{
    public class MenuGroup
    {
        public MenuGroup()
        {
            Items = new List<OPC_AuthMenu>();
        }

        public int Id { get; set; }
        public int Sort { get; set; }

        public string MenuName { get; set; }

        public IList<OPC_AuthMenu> Items { get; set; }
    }
}