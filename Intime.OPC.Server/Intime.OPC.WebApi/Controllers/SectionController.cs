using System;
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
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/counters")]
    public class SectionController : BaseController
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        private static Section CheckModel(Section model)
        {
            if (model == null)
            {
                return null;
            }

            //model.Location = model.Location ?? String.Empty;
            model.Name = model.Name ?? String.Empty;
            //model.StoreCode = model.StoreCode ?? String.Empty;
            model.ContactPhone = model.ContactPhone ?? String.Empty;
            model.SectionCode = model.SectionCode ?? String.Empty;

            model.SectionCode = model.SectionCode.Trim();
            model.Name = model.Name.Trim();



            return model;
        }

        [NonAction]
        private IHttpActionResult GetSectionList(SectionFilter filter, int userId, UserProfile userProfile)
        {
            if (filter == null)
            {
                filter = new SectionFilter();
            }

            var result = CheckRole4Store(userProfile, null);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            filter.AuthUserId = userId;
            filter.DataRoleStores = userProfile.StoreIds.ToList();
            
            filter.ArrangeParams();

            int total;

            var pagerRequest = new PagerRequest(filter.Page ?? 1, filter.PageSize ?? 0);

            var model = _sectionRepository.GetPagedList(pagerRequest, out total, filter,
                                                        filter.SortOrder ?? SectionSortOrder.Default);

            var dto = Mapper.Map<List<Section>, List<SectionDto>>(model);

            var pagerdto = new PagerInfo<SectionDto>(pagerRequest, total);
            pagerdto.Datas = dto;

            return RetrunHttpActionResult(pagerdto);
        }

        [Route("all")]
        //[ActionName("all")]
        public IHttpActionResult GetAll([UserProfile] UserProfile userProfile)
        {
            var result = CheckRole4Store(userProfile, null);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }


            var filter = new SectionFilter()
            {
                DataRoleStores = userProfile.StoreIds.ToList()
            };

            int total;
            var model = _sectionRepository.GetPagedList(new PagerRequest(1, 100000, 100000), out total, filter,
                                                        SectionSortOrder.Default);
            var dto = Mapper.Map<List<Section>, List<SectionDto>>(model);

            return RetrunHttpActionResult(dto);
        }

        [HttpGet]
        [Route("{id:int}")]

        //[ActionName("details")]
        public IHttpActionResult GetSection(int id, [UserProfile] UserProfile userProfile)
        {
            var model = _sectionRepository.GetItem(id);

            var result = CheckRole4Store(userProfile, model.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var dto = Mapper.Map<Section, SectionDto>(model);

            return RetrunHttpActionResult(dto);
        }

        [HttpPost]
        [Route("list")]
        //[ActionName("list")]
        public IHttpActionResult PostSectionList([FromBody] SectionFilter filter, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            return GetSectionList(filter, userId, userProfile);
        }

        // [ActionName("list")]/section?page=1,2&sortorder=0&prekeyname=11&storeid=1&status=2
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri]SectionFilter filter, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            return GetSectionList(filter, userId, userProfile);
        }

        [ModelValidationFilter]
        [Route("{id:int}")]
        //[ActionName("details")] api/section/1
        public IHttpActionResult Put(int id, [FromBody]SectionDto dto, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = _sectionRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            //原 权限
            var result = CheckRole4Store(userProfile, item.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            // 目的 权限
            var result2 = CheckRole4Store(userProfile, dto.StoreId);
            if (!result2.Result)
            {
                return BadRequest(result.Error);
            }

            var createDate = item.CreateDate;
            var createUser = item.CreateUser;


            dto.Id = id;
            Mapper.Map<SectionDto, Section>(dto, item);
            item.UpdateDate = DateTime.Now;
            item.UpdateUser = userId;
            item.CreateDate = createDate;
            item.CreateUser = createUser;

            item = CheckModel(item);
            ((IOPCRepository<int, Section>)_sectionRepository).Update(item);

            return GetSection(id, userProfile);
        }

        //[ActionName("details")]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            var item = _sectionRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }


            //权限
            var result = CheckRole4Store(userProfile, item.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            item.Status = -1;
            item.UpdateDate = DateTime.Now;
            item.UpdateUser = userId;

            //关系
            item.Brands = null;

            ((IOPCRepository<int, Section>)_sectionRepository).Update(item);

            return RetrunHttpActionResult("ok");
        }

        [ModelValidationFilter]
        [Route("")]
        //[ActionName("details")]
        public IHttpActionResult Post([FromBody]SectionDto dto, [UserId] int userId, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //权限
            var result = CheckRole4Store(userProfile, dto.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var model = Mapper.Map<SectionDto, Section>(dto);
            model.CreateDate = DateTime.Now;
            model.CreateUser = userId;
            model.UpdateDate = DateTime.Now;
            model.UpdateUser = userId;

            model.Status = 1;
            model = CheckModel(model);
            var item = _sectionRepository.Insert(model);

            return GetSection(item.Id, userProfile);
        }
    }
}
