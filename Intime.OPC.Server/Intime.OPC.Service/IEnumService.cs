using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Dto;

namespace Intime.OPC.Service
{
    public interface IEnumService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">配置文件名</param>
        /// <returns>IList{EnumItem}.</returns>
        IList<Item> All(string fileName);

        IList<Item> All(Type enumType);
    }
}