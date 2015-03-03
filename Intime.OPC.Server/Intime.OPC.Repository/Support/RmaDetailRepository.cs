using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class RmaDetailRepository : BaseRepository<OPC_RMADetail>, IRmaDetailRepository
    {
        #region IRmaDetailRepository Members

        public PageResult<RmaDetail> GetByRmaNo(string rmaNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query =
                    db.OPC_RMADetails.Where(t => t.RMANo == rmaNo)
                        .Join(db.OrderItems, t => t.OrderItemId, o => o.Id, (t, o) => new {RmaDetail = t, OrderItem = o})
                        .Join(db.Brands, t => t.OrderItem.BrandId, o => o.Id,
                            (t, o) => new {t.OrderItem, t.RmaDetail, BrandName = o.Name});
                var lst = query.ToList();
                var lstDto = new List<RmaDetail>();
                foreach (var o in lst)
                {
                    var d = Mapper.Map<OPC_RMADetail, RmaDetail>(o.RmaDetail);
                    d.BrandName = o.BrandName;
                    d.SizeValueName = o.OrderItem.SizeValueName;
                    d.ColorValueName = o.OrderItem.ColorValueName;
                    d.StoreItemNo = o.OrderItem.StoreItemNo;
                    d.LablePrice = o.OrderItem.UnitPrice.HasValue?d.LablePrice:o.OrderItem.ItemPrice;
                    d.Discount = o.OrderItem.UnitPrice.HasValue
                        ? d.LablePrice
                        : o.OrderItem.ItemPrice - o.OrderItem.ItemPrice;
                    
                    lstDto.Add(d);
                }
                return new PageResult<RmaDetail>(lstDto,lstDto.Count);
            }
        }

        public List<OPC_RMADetail> GetListByRmano(string rmano)
        {
            using (var db = new YintaiHZhouContext())
            {
                var rmadetails = db.OPC_RMADetails;

                return rmadetails.Where(v => v.RMANo == rmano).ToList();
            }
        }

        #endregion
    }
}