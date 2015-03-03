using System;
using System.Runtime.Serialization;

namespace Intime.OPC.Job.Order.DTO
{
    public class OrderStatusResultDto
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
        public int Flag { get; set; }
        //
        [DataMember(Name = "error")]
        public string Error { get; set; }
        //
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "product_salecode")]
        public string Products_SaleCodes { get; set; }
        //
        [DataMember(Name = "postime")]
        public DateTime PosTime { get; set; }

        //
        [DataMember(Name = "posseqno")]
        public string PosSeqNo { get; set; }


        public string Id { get; set; }

    }
}
