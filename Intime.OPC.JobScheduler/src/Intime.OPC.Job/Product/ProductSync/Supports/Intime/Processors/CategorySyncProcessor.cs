using System;
using System.Linq;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// 分类同步处理器
    /// </summary>
    public class CategorySyncProcessor : ICategorySyncProcessor
    {
        private readonly IChannelMapper _channelMapper;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public CategorySyncProcessor(IChannelMapper channelMapper)
        {
            _channelMapper = channelMapper;
        }

        public Category Sync(string channelCategoryId, string channelCategoryName)
        {
            if (string.IsNullOrWhiteSpace(channelCategoryId) || string.IsNullOrWhiteSpace(channelCategoryName))
            {
                Log.WarnFormat("分类参数错误，categoryId:{0},categoryName:{1}", channelCategoryId, channelCategoryName);
                return null;
            }

            using (var db = new YintaiHZhouContext())
            {
                // 查找分类映射关系
                var categoryMapExt = _channelMapper.GetMapByChannelValue(channelCategoryId, ChannelMapType.CategoryId);

                if (categoryMapExt == null)
                {
                    var newCategory = new Category()
                    {
                        ExCatCode = string.Empty,
                        Name = channelCategoryName,
                        Status = 1,
                        UpdateDate = DateTime.Now
                    };

                    db.Categories.Add(newCategory);
                    db.SaveChanges();

                    // 添加映射关系
                    var newChannelMap = new ChannelMap()
                    {
                        LocalId = newCategory.Id,
                        ChannnelValue = channelCategoryId,
                        MapType = ChannelMapType.CategoryId
                    };

                    _channelMapper.CreateMap(newChannelMap);

                    return newCategory;
                }

                var categoryExt = db.Categories.FirstOrDefault(b => b.Id == categoryMapExt.LocalId);

                // 更新品牌
                if (categoryExt != null)
                {
                    categoryExt.Name = channelCategoryName ?? string.Empty;
                    db.SaveChanges();
                }

                return categoryExt;
            }
        }
    }
}
