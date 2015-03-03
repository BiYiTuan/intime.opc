using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class TimeSeed
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Seed { get; set; }
        public string KeySeed { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
