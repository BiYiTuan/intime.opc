namespace Intime.OPC.Infrastructure.Mvvm
{
    public class Common
    {
        public static string ViewModelKey(string key)
        {
            return string.Format("{0}ViewModel", key);
        }
    }
}