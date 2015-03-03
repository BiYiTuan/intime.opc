
namespace Intime.OPC.Job.Product.ProductSync.Models
{
    public enum ChannelMapType
    {
        Unknow = 0,
        Sku = 1,
        Brand = 2,
        CategoryId = 3,
        StoreNo = 4,
        SectionId = 5,
        PropertyId = 6,
        PropertyValueId = 7,
        ColorId = 8,
        SizeId = 9,
        ProductCode = 10,
        ProductId = 11,
        ProductPic = 12, //本地保存花色的id -> 远程图片的地址
        ProductId4Color = 13,
        ProductId4Size = 14,
    }
}
