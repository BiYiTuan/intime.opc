using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto
{
    public class BankDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string StatusName { get; set; }
    }
}
