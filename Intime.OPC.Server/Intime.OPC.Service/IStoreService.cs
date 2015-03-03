using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IStoreService //: IService
    {
        IList<Store> GetAll();

        PagerInfo<StoreDto> GetPagedList(StoreRequest storeRequest);

        StoreDto Insert(StoreDto dto, int userId);

        void Update(StoreDto dto, int userId);

        void Delete(int storeId, int userId);

        StoreDto GetItem(int storeId);
    }
}