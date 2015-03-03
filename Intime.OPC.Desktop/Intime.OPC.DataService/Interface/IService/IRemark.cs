using Intime.OPC.Domain.Enums;

namespace Intime.OPC.DataService.IService
{
    public interface IRemark
    {
        void ShowRemarkWin(string id, EnumSetRemarkType type);
    }
}