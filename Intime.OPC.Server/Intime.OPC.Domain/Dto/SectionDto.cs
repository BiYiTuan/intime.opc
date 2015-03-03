 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Management.Instrumentation;
using Newtonsoft.Json;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 专柜
    /// </summary>
    public class SectionDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "名称请不要超过200个字符", MinimumLength = 1)]
        public string Name { get; set; }

        //读
        //OPC_CHANNELMAP
        //MAPTYPE=5
        //INVALUE=SectionId

        //ChannelValue

        /// <summary>
        /// 只能新增，不能修改
        /// </summary>
        [StringLength(20, ErrorMessage = "专柜码请不要超过20个字符")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "ContactPhoneNumber")]
        public string ContactPhone { get; set; }

        public int? Status { get; set; }

        /// <summary>
        /// Repealed or not
        /// </summary>
        [Required(ErrorMessage = "是否撤柜必须填写")]
        public bool Repealed
        {
            get
            {
                if (Status != null && Status.Value == 0)
                {
                    return true;
                }

                return false;
            }
            set
            {
                Status = value ? 0 : 1;
            }
        }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public bool? Enabled
        //{
        //    get
        //    {
        //        if (Status == null)
        //        {
        //            return null;
        //        }

        //        return Status == 1;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            Status = null;

        //        }
        //        else
        //        {
        //            if (value == true)
        //            {
        //                Status = 1;
        //            }
        //            else
        //            {
        //                Status = -1;
        //            }
        //        }



        //    }
        //}

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<BrandDto> Brands { get; set; }

        [Required(ErrorMessage = "必须提供门店Id")]
        public int StoreId { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StoreDto Store { get; set; }
    }
}