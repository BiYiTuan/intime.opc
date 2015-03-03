using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intime.O2O.ApiClient.Domain
{

    [DataContract]
    public class OrderStatus
    {
        //返回值  成功 1
        [DataMember(Name = "RET")]
        public string ret { get; set; }

       //执行状态，中文
        [DataMember(Name = "DESC")]
        public string desc { get; set; }

        public string Id { get; set; }
    }
}
