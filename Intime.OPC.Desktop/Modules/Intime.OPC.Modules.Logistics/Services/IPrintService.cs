namespace Intime.OPC.Modules.Logistics.Services
{
    /// <summary>
    /// 封装的打印接口,从界面上取得数值，打印时不与后台交互
    /// </summary>
    public interface IPrintService
    {
        void ViewAndPrint(int type);

        void FastPrint(int type);
    } 
}