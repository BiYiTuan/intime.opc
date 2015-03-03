using System;
using System.Web.Http;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;

namespace Intime.OPC.WebApi.Controllers
{
    /// <summary>
    ///     账户相关接口
    /// </summary>
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        ///     设定用户的角色
        /// </summary>
        /// <param name="roleUserDto">The role user dto.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult SetUsers([FromBody] RoleUserDto roleUserDto, [UserId] int uid)
        {
            try
            {
                _roleService.SetUsers(roleUserDto, uid);
                return Ok();
            }
            catch (Exception ex)
            {
                this.GetLog().Error(ex);
            }
            return InternalServerError();

        }

        [HttpPost]
        public IHttpActionResult AddRole([FromBody] OPC_AuthRole role)
            //public IHttpActionResult AddUser()
        {
            //TODO:check params
            if (_roleService.Add(role))
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpPut]
        public IHttpActionResult UpdateRole([FromBody] OPC_AuthRole role)
        {
            //TODO:check params

            try
            {
                if (_roleService.Update(role))
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                this.GetLog().Error(ex);
            }

            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult SetMenus([FromBody] RoleMenuDto menuDto,[UserId] int userId)
        {
            bool bl = _roleService.SetMenus(menuDto.RoleID,userId,menuDto.MenuList);
            if (bl)
                return Ok();
            return InternalServerError();
        }

        [HttpPut]
        public IHttpActionResult DeleteRole([FromBody]int roleId)
        {
            //TODO:check params
            if (_roleService.DeleteById(roleId))
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult SelectRole()
        {
            //TODO:check params
            return Ok(_roleService.Select(0,10000).Result);
        }


        [HttpPut]
        public IHttpActionResult Enable([FromBody] OPC_AuthRole role)
        {
            if (role==null)
            {
                return BadRequest("角色为空");
            }
            if (_roleService.IsStop(role.Id, role.IsValid==false))
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}