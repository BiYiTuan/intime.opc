using System;
using System.Globalization;
using System.Linq;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Mapper
{
    public class ChannelMapper : IChannelMapper
    {
        public Models.ChannelMap GetMapByChannelValue(string channelValue, Models.ChannelMapType mapType)
        {
            using (var db = new YintaiHZhouContext())
            {
                var channeMap = db.OPC_ChannelMap.FirstOrDefault(c => c.ChannelValue == channelValue && c.MapType == (int)mapType && c.Channel == SystemDefine.IntimeChannel);
                if (channeMap == null)
                {
                    return null;
                }

                return new ChannelMap()
                {
                    LocalId = Convert.ToInt32(channeMap.InnerValue),
                    ChannnelValue = channeMap.ChannelValue,
                    MapType = channeMap.MapType != null ? (ChannelMapType)channeMap.MapType : ChannelMapType.Unknow
                };
            }
        }

        public Models.ChannelMap GetMapByLocal(string localValue, Models.ChannelMapType mapType)
        {
            using (var db = new YintaiHZhouContext())
            {
                var channeMap = db.OPC_ChannelMap.FirstOrDefault(c => c.InnerValue == localValue && c.MapType == (int)mapType && c.Channel == SystemDefine.IntimeChannel);
                if (channeMap == null)
                {
                    return null;
                }

                return new ChannelMap()
                {
                    LocalId = Convert.ToInt32(channeMap.InnerValue),
                    ChannnelValue = channeMap.ChannelValue,
                    MapType = channeMap.MapType != null ? (ChannelMapType)channeMap.MapType : ChannelMapType.Unknow
                };
            }
        }

        public bool CreateMap(Models.ChannelMap channelMap)
        {

            using (var db = new YintaiHZhouContext())
            {
                db.OPC_ChannelMap.Add(new OPC_ChannelMap()
                {
                    InnerValue = channelMap.LocalId.ToString(CultureInfo.InvariantCulture),
                    ChannelValue = channelMap.ChannnelValue,
                    MapType = Convert.ToInt32(channelMap.MapType),
                    Channel = SystemDefine.IntimeChannel
                });
                db.SaveChanges();

                return true;
            }
        }

        public void RemoveMap(ChannelMap channelMap)
        {
            using (var db = new YintaiHZhouContext())
            {
                var innverValue = channelMap.LocalId.ToString();
                var map =
                    db.OPC_ChannelMap.FirstOrDefault(
                        x =>
                            x.Channel == SystemDefine.IntimeChannel && x.ChannelValue == channelMap.ChannnelValue &&
                            x.MapType == (int)channelMap.MapType && x.InnerValue == innverValue);
                if (map != null)
                {
                    db.OPC_ChannelMap.Remove(map);
                    db.SaveChanges();
                }
            }
        }

        public bool UpdateMapByLocal(string localValue, ChannelMapType mapType, string channelValue)
        {
            using (var db = new YintaiHZhouContext())
            {
                var channeMap = db.OPC_ChannelMap.FirstOrDefault(c => c.InnerValue == localValue && c.MapType == (int)mapType && c.Channel == SystemDefine.IntimeChannel);
                if (channeMap == null)
                {
                    return false;
                }

                channeMap.ChannelValue = channelValue;

                db.SaveChanges();

                return true;
            }
        }
    }
}
