using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class BrandDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string EnglishName { get; set; }
     
        public string Description { get; set; }

        [JsonIgnore]
        public int Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled
        {
            get { return Status == 1; }
            set
            {
                if (value == true)
                {
                    Status = 1;
                }
                else
                {
                    Status = -1;
                }
            }
        }


        [JsonProperty(PropertyName = "Counters", NullValueHandling = NullValueHandling.Ignore)]
        public List<SectionDto> Sections { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SupplierDto Supplier
        {
            get
            {
                if (Suppliers == null)
                {
                    return null;
                }

                return Suppliers.FirstOrDefault();
            }
            set
            {
                if (Suppliers == null)
                {
                    Suppliers = new List<SupplierDto>();
                }

                Suppliers.Add(value);
            }
        }

        [JsonIgnore]
        public List<SupplierDto> Suppliers
        {
            get;
            set;
        }
    }
}