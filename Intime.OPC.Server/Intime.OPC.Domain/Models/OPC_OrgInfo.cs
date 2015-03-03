using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_OrgInfo : IEntity
    {
        public string OrgID { get; set; }
        public string OrgName { get; set; }
        public string ParentID { get; set; }
        public int? StoreOrSectionID { get; set; }
        public string StoreOrSectionName { get; set; }
        public int? OrgType { get; set; }
        public bool? IsDel { get; set; }



        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}