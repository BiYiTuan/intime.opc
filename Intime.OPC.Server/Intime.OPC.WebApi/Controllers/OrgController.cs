using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.WebApi.Controllers
{
    public class OrgController : BaseController
    {
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof (OrgController));
        private readonly IOrgService _orgServiceService;

        public OrgController(IOrgService orgService)
        {
            _orgServiceService = orgService;
        }

        //TODO:多集团支持后 这个接口的内容需要修改下，根据当前用户的权限获取集团信息

        [HttpPost]
        public IHttpActionResult GetAll([UserProfile] UserProfile userProfile)
        {
            //只能获取当前集团信息
            return DoFunction(() =>
                {
                    PageResult<OPC_OrgInfo> lst;
                    if (userProfile.IsSystem)
                    {
                        lst = _orgServiceService.GetAll(1, 10000);
                    }
                    else
                    {
                        var list = userProfile.OrgIds;

                        var t = _orgServiceService.GetPagedList(new PagerRequest(1, 20000, 20000), list.ToList(),
                                                              default(EnumOrgType?));

                        lst = new PageResult<OPC_OrgInfo>(t.Datas, t.TotalCount);
                    }

                    return lst.Result;
                }, "获得组织结构信息失败");
        }

        [HttpPost]
        public IHttpActionResult AddOrg([FromBody]OPC_OrgInfo orgInfo, [UserProfile] UserProfile userProfile)
        {
            var t = CheckAndReturnMsg(orgInfo, userProfile);

            if (!String.IsNullOrEmpty(t))
            {
                return BadRequest(t);
            }

            return DoFunction(() =>
            {
                return _orgServiceService.AddOrgInfo(orgInfo);

            }, "添加组织机构失败");
        }

        private string CheckAndReturnMsg(OPC_OrgInfo orgInfo, UserProfile userProfile)
        {
            if (orgInfo == null)
            {
                return "组织机构获取失败";
            }

            if (userProfile == null)
            {
                return "用户信息获取失败";
            }

            //非管理员不能添加顶级节点，只能增加子节点
            if (String.IsNullOrEmpty(orgInfo.ParentID) && !userProfile.IsSystem)
            {
                return "父节ID不能为空";
            }

            //验证
            if (!userProfile.IsSystem && !String.IsNullOrEmpty(orgInfo.ParentID))
            {
                var o = _orgServiceService.GetOrgInfoByOrgID(orgInfo.ParentID);

                if (o == null)
                {
                    return "父节点不能为空";
                }
            }

            return null;
        }

        [HttpPut]
        public IHttpActionResult UpdateOrg([FromBody]OPC_OrgInfo orgInfo, [UserProfile] UserProfile userProfile)
        {
            var t = CheckAndReturnMsg(orgInfo, userProfile);

            if (!String.IsNullOrEmpty(t))
            {
                return BadRequest(t);
            }

            return DoFunction(() =>
            {
                bool bl = _orgServiceService.Update(orgInfo);
                return bl;
            }, "更新组织机构失败");
        }

        [HttpPut]
        public IHttpActionResult DeleteOrg([FromBody]int orgInfoId, [UserProfile] UserProfile userProfile)
        {
            var orgInfo = _orgServiceService.Get(orgInfoId);

            var t = CheckAndReturnMsg(orgInfo, userProfile);

            if (!String.IsNullOrEmpty(t))
            {
                return BadRequest(t);
            }

            return DoFunction(() =>
            {
                bool bl = _orgServiceService.DeleteById(orgInfoId);
                return bl;
            }, "删除组织机构失败");
        }
    }
}