using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto.Custom
{
    public class PackageVerifyRequest
    {
        public PackageVerifyRequest()
        {
            RmaNos = new List<string>();
        }

        public IList<string> RmaNos { get; set; }
        public bool Pass { get; set; }
    }
}