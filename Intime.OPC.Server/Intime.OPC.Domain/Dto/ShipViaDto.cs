using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Intime.OPC.Domain.Dto
{
    public class ShipViaDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonIgnore]
        public int Status { get; set; }

        public bool IsEnabled
        {
            get { return Status == 1; }
            set
            {
                if (value)
                {
                    Status = 1;
                }
                else
                {
                    Status = -1;
                }
            }
        }
    }
}
