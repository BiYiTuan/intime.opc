using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Dto;

namespace Intime.OPC.Modules.Logistics.Print
{
    public class PrintModel
    {
        public List<SaleDto> SaleDT { get; set; }
        public List<OPC_SaleDetail> SaleDetailDT { get; set; }
        public List<Order> OrderDT { get; set; }
    }
}