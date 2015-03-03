using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Intime.OPC.Infrastructure.Print;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Logistics
{
    [ModuleExport(typeof (LogisticsModule))]
    public class LogisticsModule : IModule
    {
        [Import]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            Mapper.CreateMap<OPC_Comment, OPC_SaleComment>(e =>
                new OPC_SaleComment
                {
                    Id = e.Id,
                    SaleOrderNo = e.RelationId,
                    Content = e.Content,
                    CreateDate = e.CreateDate,
                    CreateUser = e.CreateUser
                }
                );

            Mapper.CreateMap<OPC_SaleComment, OPC_Comment>(e =>
                new OPC_Comment
                {
                    Id = e.Id,
                    RelationId = e.SaleOrderNo,
                    Content = e.Content,
                    CreateDate = e.CreateDate,
                    CreateUser = e.CreateUser
                }
                );


            Mapper.CreateMap<OPC_Comment, OPC_SaleDetailsComment>(e =>
                new OPC_SaleDetailsComment
                {
                    Id = e.Id,
                    SaleDetailId = e.RelationId,
                    Content = e.Content,
                    CreateDate = e.CreateDate,
                    CreateUser = e.CreateUser
                }
                );

            Mapper.CreateMap<OPC_SaleDetailsComment, OPC_Comment>(e =>
                new OPC_Comment
                {
                    Id = e.Id,
                    RelationId = e.SaleDetailId,
                    Content = e.Content,
                    CreateDate = e.CreateDate,
                    CreateUser = e.CreateUser
                }
                );
        }
    }
}