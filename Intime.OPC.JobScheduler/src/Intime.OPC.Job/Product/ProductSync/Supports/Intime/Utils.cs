
namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime
{
    public static class Utils
    {
        public static string GetProductProprtyMapKey(int productId, string channelPropertyValueId)
        {
            return string.Format("{0}/{1}", productId, channelPropertyValueId);
        }
    }
}
