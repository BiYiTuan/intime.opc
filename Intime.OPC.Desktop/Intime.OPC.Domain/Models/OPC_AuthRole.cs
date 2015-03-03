using System;

namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthRole
    {
        public OPC_AuthRole()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            IsValid = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
    }
}