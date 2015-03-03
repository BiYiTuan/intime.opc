using System;
using System.Collections.Generic;
using Intime.O2O.ApiClient.Domain;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository
{
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IRemoteRepository
    {
        /// <summary>
        /// 根据Id获取供应商信息 
        /// </summary>
        /// <param name="id">供应商Id</param>
        /// <returns>供应商信息</returns>
        SupplierDto GetSupplierById(int id);

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>供应商列表</returns>
        IEnumerable<SupplierDto> GetSupplierList(int pageIndex, int pageSize);

        /// <summary>
        /// 根据Id获取店铺信息
        /// </summary>
        /// <param name="id">店铺Id</param>
        /// <returns>店铺信息</returns>
        StoreDto GetStoreById(string id);

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>门店信息列表</returns>
        IEnumerable<StoreDto> GetStoreList(int pageIndex, int pageSize);

        /// <summary>
        /// 根据Id获取品牌信息
        /// </summary>
        /// <param name="id">品牌id</param>
        /// <returns>品牌信息</returns>
        BrandDto GetBrandById(string id);

        /// <summary>
        /// 根据更新时间获取品牌列表
        /// </summary>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="lastUpdateDateTime">最后更新时间</param>
        /// <returns>品牌列表</returns>
        IEnumerable<BrandDto> GetBrandList(int pageIndex, int pageSize, DateTime lastUpdateDateTime);

        /// <summary>
        /// 根据更新时间获取商品列表
        /// </summary>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="lastUpdateDateTime">最后更新时间</param>
        /// <returns>商品列表</returns>
        IEnumerable<ProductDto> GetProductList(int pageIndex, int pageSize, DateTime lastUpdateDateTime);

        /// <summary>
        /// 根据Id获取专柜信息
        /// </summary>
        /// <param name="counterId">专柜Id</param>
        /// <param name="storeNo">门店Id</param>
        /// <returns>专柜信息</returns>
        SectionDto GetSectionById(string counterId, string storeNo);

        /// <summary>
        /// 根据更新时间获取专柜列表
        /// </summary>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="lastUpdateDateTime">最后更新时间</param>
        /// <returns>商品列表</returns>
        IEnumerable<SectionDto> GetSectionList(int pageIndex, int pageSize, DateTime lastUpdateDateTime);

        /// <summary>
        /// 根据更新时间获取商品图片列表
        /// </summary>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="lastUpdateDateTime">最后更新时间</param>
        /// <returns>商品图片列表</returns>
        IEnumerable<ProductImageDto> GetProudctImages(int pageIndex, int pageSize, DateTime lastUpdateDateTime);

        /// <summary>
        /// 获取商品属性
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="lastUpdateDateTime"></param>
        /// <returns></returns>
        IEnumerable<PropertyValue> GetProductProperties(int pageIndex, int pageSize, DateTime lastUpdateDateTime);

        /// <summary>
        /// 获取商品属性，聚合
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="lastUpDateTime"></param>
        /// <returns></returns>
        IEnumerable<PropertyValueRaw> GetPropertyValueRaws(int pageIndex, int pageSize, DateTime lastUpDateTime);
    }
}
