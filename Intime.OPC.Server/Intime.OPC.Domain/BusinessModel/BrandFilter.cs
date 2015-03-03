using Intime.OPC.Domain.Enums.SortOrder;

namespace Intime.OPC.Domain.BusinessModel
{
    public class BrandFilter : PageFilter
    {
        public string NamePrefix { get; set; }

        public int? Status { get; set; }

        /// <summary>
        /// 专柜
        /// </summary>
        public int? CounterId { get; set; }

        public string Name { get; set; }

        public BrandSortOrder? SortOrder { get; set; }
    }
}