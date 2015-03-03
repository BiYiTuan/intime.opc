using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intime.O2O.ApiClient.Domain
{
    [DataContract]
    public class OrderStatusResult
    {
        //返回值  成功 1
        [DataMember(Name = "ret")]
        public string Ret { get; set; }

        //执行状态，中文
        [DataMember(Name = "desc")]
        public string Desc { get; set; }
        //
        [DataMember(Name = "posno")]
        public string Posno { get; set; }
        //
        [DataMember(Name = "flag")]
        public int  Flag { get; set; }
        //
        [DataMember(Name = "error")]
        public string Error { get; set; }
        //
        [DataMember(Name = "status")]
        public string Status { get; set; }

        //
        [DataMember(Name = "postime")]
        public string PosTime { get; set; }

        //
        [DataMember(Name = "posseqno")]
        public string PosSeqNo { get; set; }

        public string Id { get; set; }
    }
}
