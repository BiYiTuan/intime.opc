using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class WX_Menu
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public string AppId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public Nullable<int> ActionType { get; set; }
        public Nullable<int> Pos { get; set; }
        public string WKey { get; set; }
    }
}
