using System;

namespace Intime.OPC.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public string Mo { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string Operation { get; set; }
    }
}