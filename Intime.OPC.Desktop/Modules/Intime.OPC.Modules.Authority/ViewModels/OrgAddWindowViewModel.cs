using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.DataService.Interface;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export("OrgViewModel", typeof (IViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrgAddWindowViewMode : BaseViewModel<OPC_OrgInfo>
    {
        private IList<KeyValue> _storeOrSectionList;

        public OrgAddWindowViewMode()
            : base("OrgView")
        {
            Model = new OPC_OrgInfo();
            OrgTypeList = new List<KeyValue> {new KeyValue(0, "组织机构"), new KeyValue(5, "门店"), new KeyValue(10, "专柜")};
            OrgTypeChangeCommand = new DelegateCommand<int?>(OrgTypeChange);
        }

        public DelegateCommand<int?> OrgTypeChangeCommand { get; set; }
        public List<KeyValue> OrgTypeList { get; set; }

        public IList<KeyValue> StoreOrSectionList
        {
            get { return _storeOrSectionList; }
            set { SetProperty(ref _storeOrSectionList, value); }
        }

        public void OrgTypeChange(int? orgType)
        {
            GetOrgRefreshStoreOrSection(orgType);
        }

        public void GetOrgRefreshStoreOrSection(int? orgType)
        {
            switch (orgType)
            {
                case 5:
                    StoreOrSectionList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
                    break;
                case 10:
                    StoreOrSectionList = AppEx.Container.GetInstance<ICommonInfo>().GetSectionList();
                    break;
                default:
                    StoreOrSectionList = new List<KeyValue>();
                    break;
            }
        }


        protected override IBaseDataService<OPC_OrgInfo> GetDataService()
        {
            return AppEx.Container.GetInstance<IOrganizationService>();
        }
    }
}