using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_SectionBrand
    {
        public int Id { get; set; }

        public int SectionId { get; set; }

        public int BrandId { get; set; }
    }
}
