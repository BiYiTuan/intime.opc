namespace Intime.OPC.Service.Support
{
    public class ConnectProduct : IConnectProduct
    {
        #region IConnectProduct Members

        public string GetCashNo(string orderNo, string rmaNo, decimal money)
        {
            //todo获得收银流水号
            return "a12345678";
        }

        #endregion
    }
}