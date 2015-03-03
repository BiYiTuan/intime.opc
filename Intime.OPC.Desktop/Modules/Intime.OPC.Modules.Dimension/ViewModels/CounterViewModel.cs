using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(CounterViewModel))]
    public class CounterViewModel : ModalDialogViewModel<Counter>
    {
        private IQueryCriteria _brandQueryCriteria;
        private ObservableCollection<Brand> _brands;
        private IList<Store> _stores;
        private Store[] _storeArray;
        private bool _isFiltingStore;

        public CounterViewModel()
        {
            LoadDimesionsCommand = new AsyncDelegateCommand(OnDimesionsLoad);
            LoadBrandsCommand = new AsyncDelegateCommand<string>(OnBrandsLoad);
            LoadMoreBrandsCommand = new AsyncDelegateCommand(OnMoreBrandsLoad);
            SelectBrandCommand = new DelegateCommand<int?>(OnBrandSelect);
            FilterStoreCommand = new DelegateCommand<string>(OnStoreFilter);

            Brands = new ObservableCollection<Brand>();
        }

        #region Properties

        [Import]
        public IService<Brand> BrandService { get; set; }

        [Import]
        public IService<Store> StoreService { get; set; }

        public ObservableCollection<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }

        public IList<Store> Stores
        {
            get { return _stores; }
            set { SetProperty(ref _stores, value); }
        }

        public bool IsFilteringStore
        {
            get { return _isFiltingStore; }
            set { SetProperty(ref _isFiltingStore, value); }
        }

        #endregion

        #region Commands

        public ICommand FilterStoreCommand { get; set; }

        public ICommand LoadDimesionsCommand { get; set; }

        public ICommand LoadBrandsCommand { get; set; }

        public ICommand SelectBrandCommand { get; set; }

        public ICommand LoadMoreBrandsCommand { get; set; }

        #endregion

        #region Command handlers

        private void OnBrandSelect(int? brandID)
        {
            if (brandID == null) return;

            if (Model.Brands == null) Model.Brands = new List<Brand>();
            var brand = Brands.Where(b => b.Id == brandID.Value).FirstOrDefault();
            if (brand.IsSelected)
            {
                Model.Brands.Add(brand);
            }
            else
            {
                Model.Brands.Remove(b => b.Id == brand.Id);
            }
        }

        private void OnBrandsLoad(string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
            {
                _brandQueryCriteria = new QueryAll { PageIndex =1, PageSize = 100};
            }
            else
            {
                _brandQueryCriteria = new QueryByName { PageIndex = 1, PageSize = 100, Name = brandName };
            }

            var result = BrandService.Query(_brandQueryCriteria);
            var brands = new ObservableCollection<Brand>(result.Data);
            FilterBrands(brands);
            Brands = new ObservableCollection<Brand>(brands);

            if (Model.Brands == null) Model.Brands = new List<Brand>();

            Model.Brands.ForEach(brand =>
            {
                brand.IsSelected = true;
                int index = 0;
                PerformActionOnUIThread(() => Brands.Insert(index++, brand));
            });
        }

        private void OnMoreBrandsLoad()
        {
            if (_brandQueryCriteria == null) return;

            _brandQueryCriteria.PageIndex++;
            var brands = BrandService.Query(_brandQueryCriteria).Data;
            FilterBrands(brands);
            AppendBrands(brands);
        }

        private void OnStoreFilter(string storeName)
        {
            if (!string.IsNullOrEmpty(storeName))
            {
                var stores = _storeArray.Where(store => store.Name.Contains(storeName)).ToList();
                Stores = stores;
            }
            else
            {
                Stores = new List<Store>(_storeArray);
            }

            IsFilteringStore = true;
        }

        private void OnDimesionsLoad()
        {
            OnBrandsLoad(null);

            LoadStores();
        }

        #endregion

        private void LoadStores()
        {
            Stores = StoreService.QueryAll(new QueryAll());
            _storeArray = new Store[Stores.Count];
            Stores.CopyTo(_storeArray, 0);

            if (Model.Store == null) return;

            Model.Store = Stores.Where(store => store.Id == Model.StoreId).FirstOrDefault();
        }

        public void AppendBrands(IEnumerable<Brand> brands)
        {
            foreach (var brand in brands)
            {
                PerformActionOnUIThread(() => Brands.Add(brand));
            }
        }

        private void FilterBrands(IList<Brand> brands)
        {
            if (Model.Brands == null || Model.Brands.Count == 0) return;

            brands.ForEach(brand =>
            {
                if (Model.Brands.Contains(b => b.Id == brand.Id))
                {
                    brand.IsSelected = true;
                }
            });

            brands.Remove(brand => brand.IsSelected);
        }
    }
}
