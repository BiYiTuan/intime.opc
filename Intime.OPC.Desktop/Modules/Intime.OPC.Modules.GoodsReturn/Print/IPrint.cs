using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.GoodsReturn.Print
{
    public interface IPrint
    {
        void Print(string xsdName, string rdlcName, PrintRMAModel dtList, bool isPrint = false);
        void PrintExpress(string rdlcName, PrintExpressModel dtList, bool isPrint = false);
        void PrintDeliveryOrder(string rdlcName, Order order, IList<OPC_RMA> opcRma, IList<OPC_RMADetail> listOpcRmaDetails, bool isPrint = false);
        void PrintReturnGoods(string xsdName, string rdlcName, ReturnGoodsPrintModel dtList, bool isPrint = false);

    }
}