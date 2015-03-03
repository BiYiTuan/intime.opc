
namespace Intime.OPC.Domain.Models
{
    public partial class Department 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public int SortOrder { get; set; }
    }
}
