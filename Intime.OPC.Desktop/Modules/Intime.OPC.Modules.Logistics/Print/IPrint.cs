using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Logistics.Print
{
    public interface IPrint
    {
        void Print(string xsdName, string rdlcName, PrintModel dtList, bool isPrint = false);
        void PrintExpress(string rdlcName, PrintExpressModel dtList, bool isPrint = false);
        void PrintDeliveryOrder(string rdlcName, Order order, IList<OPC_Sale> opcSales, IList<OPC_SaleDetail> listOpcSaleDetails, bool isPrint = false);
    }
}