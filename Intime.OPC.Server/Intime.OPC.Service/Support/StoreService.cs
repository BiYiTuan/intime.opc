using System;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using System.Collections.Generic;
using PagerRequest = Intime.OPC.Domain.PagerRequest;

namespace Intime.OPC.Service.Support
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository repository)
            : base(repository)
        {
            _storeRepository = repository;
        }

        public IList<Store> GetAll()
        {
            return _repository.GetAll(0, 1000).Result;
        }

        public PagerInfo<StoreDto> GetPagedList(StoreRequest storeRequest)
        {
            var storeFilter = Mapper.Map<StoreRequest, StoreFilter>(storeRequest);

            int totalCount;
            var list = _storeRepository.GetPagedList(storeRequest.PagerRequest, out totalCount, storeFilter);

            var dto = Mapper.Map<List<Store>, List<StoreDto>>(list);
            var page = new PagerInfo<StoreDto>(storeRequest.PagerRequest, totalCount) { Datas = dto };

            return page;
        }

        public StoreDto Insert(StoreDto dto, int userId)
        {
            var entity = Mapper.Map<StoreDto, Store>(dto);
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUser = userId;
            entity.CreatedUser = userId;

            var rst = _storeRepository.Insert(entity);

            return Mapper.Map<Store, StoreDto>(rst);
        }

        public void Update(StoreDto dto, int userId)
        {
            var entity = _storeRepository.GetItem(dto.Id);
            var newentity = Mapper.Map<Store, Store>(entity);
            Mapper.Map(dto, entity);
            entity.CreatedUser = newentity.CreatedUser;
            entity.CreatedDate = newentity.CreatedDate;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUser = userId;

            _storeRepository.Update(entity);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        public void Delete(int storeId, int userId)
        {
            var entity = _storeRepository.GetItem(storeId);
            entity.Status = -1;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUser = userId;

            _storeRepository.Update(entity);
        }

        public StoreDto GetItem(int storeId)
        {
            var entity = _storeRepository.GetItem(storeId);

            return Mapper.Map<Store, StoreDto>(entity);
        }
    }
}
