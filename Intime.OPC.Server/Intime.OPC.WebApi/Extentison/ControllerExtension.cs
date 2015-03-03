// ***********************************************************************
// Assembly         : 03_Intime.OPC.WebApi
// Author           : Liuyh
// Created          : 03-27-2014 22:57:31
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 23:25:30
// ***********************************************************************
// <copyright file="ControllerExtension.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Intime.OPC.Common.Logger;
using Intime.OPC.Domain.Exception;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace System.Web.Http
{
    /// <summary>
    /// Class ControllerExtension.
    /// </summary>
    public static class ControllerExtension
    {
      
        /// <summary>
        /// 获得当前用户
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>System.Nullable{System.Int32}.</returns>
        /// <exception cref="Intime.OPC.Domain.Exception.UserIdConverException"></exception>
        public static int? GetCurrentUserID(this ApiController controller)
        {
            if (controller.ActionContext.Request.Properties.ContainsKey(AccessTokenConst.UseridPropertiesName))
            {
                string userid =
                    controller.ActionContext.Request.Properties[AccessTokenConst.UseridPropertiesName].ToString();
                int id = -1;
                bool bl= int.TryParse(userid,out id);
                if (bl)
                {
                    return id;
                }
                else
                {
                    throw new UserIdConverException(userid);
                
                }
            }
            return null;
        }

       

      

    }
}