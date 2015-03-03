using System.Linq;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IBrandRepository _brandRepository;
        public SupplierController(ISupplierRepository supplierRepository, IBrandRepository brandRepository)
        {
            _supplierRepository = supplierRepository;
            _brandRepository = brandRepository;
        }

        private static OpcSupplierInfo CheckModel(OpcSupplierInfo model)
        {
            if (model == null)
            {
                return null;
            }
            model.Address = model.Address ?? String.Empty;
            model.Contact = model.Contact ?? String.Empty;
            model.Contract = model.Contract ?? String.Empty;
            model.Corporate = model.Corporate ?? String.Empty;
            model.FaxNo = model.FaxNo ?? String.Empty;
            model.Memo = model.Memo ?? String.Empty;
            model.SupplierName = model.SupplierName ?? String.Empty;
            model.SupplierNo = model.SupplierNo ?? String.Empty;
            model.TaxNo = model.TaxNo ?? String.Empty;
            model.Telephone = model.Telephone ?? String.Empty;

            return model;
        }

        //[HttpGet]
        [Route("{id:int}")]

        //[ActionName("details")]
        public IHttpActionResult Get(int id)
        {
            var model = _supplierRepository.GetItem(id);


            var dto = Mapper.Map<OpcSupplierInfo, SupplierDto>(model);

            return RetrunHttpActionResult(dto);
        }

        // [ActionName("list")]/Supplier?page=1,2&sortorder=0&prekeyname=11&storeid=1&status=2
        [Route("")]
        public IHttpActionResult GetList([FromUri]SupplierFilter filter, [UserId] int userId)
        {
            int total;

            if (filter == null)
            {
                filter = new SupplierFilter();
            }

            var pagerRequest = new PagerRequest(filter.Page ?? 1, filter.PageSize ?? 0);

            var model = _supplierRepository.GetPagedList(pagerRequest, out total, filter, filter.SortOrder ?? SupplierSortOrder.Default);

            var dto = Mapper.Map<List<OpcSupplierInfo>, List<SupplierDto>>(model);

            var pagerdto = new PagerInfo<SupplierDto>(pagerRequest, total);
            pagerdto.Datas = dto;

            return RetrunHttpActionResult(pagerdto);
        }

        [ModelValidationFilter]
        [Route("{id:int}")]
        //[ActionName("details")] api/Supplier/1
        public IHttpActionResult Put(int id, [FromBody]SupplierDto dto, [UserId] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = _supplierRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            var createDate = item.CreatedDate;
            var createUser = item.CreatedUser;

            dto.Id = id;
            Mapper.Map<SupplierDto, OpcSupplierInfo>(dto, item);
            item.UpdatedUser = userId;
            item.UpdatedDate = DateTime.Now;
            item.CreatedDate = createDate;
            item.CreatedUser = createUser;

            item.Brands = _brandRepository.GetByIds(dto.Brands.Select(v => v.Id).ToArray()).ToList();

            item = CheckModel(item);

            ((IOPCRepository<int, OpcSupplierInfo>)_supplierRepository).Update(item);

            return Get(id);
        }

        //[ActionName("details")]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id, [UserId] int userId)
        {
            var item = _supplierRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            //var createDate = item.CreatedDate;
            //var createUser = item.CreatedUser;
            //逻辑删除
            item.Status = -1;
            //关系有删除
            item.Brands = null;
            item.UpdatedUser = userId;
            item.UpdatedDate = DateTime.Now;

            item.Brands = null;

            //item.CreatedDate = createDate;
            //item.CreatedUser = createUser;

            _supplierRepository.Update(item);

            return RetrunHttpActionResult("OK");
        }

        [ModelValidationFilter]
        [Route("")]
        //[ActionName("details")]
        public IHttpActionResult Post([FromBody]SupplierDto dto, [UserId] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var model = Mapper.Map<SupplierDto, OpcSupplierInfo>(dto);
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.CreatedUser = userId;
            model.UpdatedUser = userId;

            if (dto.Brands != null && dto.Brands.Count > 0)
            {
                model.Brands = _brandRepository.GetByIds(dto.Brands.Select(v => v.Id).ToArray()).ToList();
            }
            
            model = CheckModel(model);
            var item = _supplierRepository.Insert(model);

            //item.Brands = _brandRepository.GetByIds(dto.Brands.Select(v => v.Id).ToArray()).ToList();
            //_supplierRepository.Update(item);
            

            var resultdto = Mapper.Map<OpcSupplierInfo, SupplierDto>(item);

            return RetrunHttpActionResult(resultdto);
        }
    }

}
