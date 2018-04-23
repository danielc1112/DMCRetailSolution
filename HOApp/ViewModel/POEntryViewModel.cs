using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using DataAccess;
using HOApp.Model;

namespace HOApp.ViewModel
{
    internal class POEntryViewModel : DataVMBase
    {
        public POEntryViewModel() : base()
        {
            _selectableProducts = new List<ProductVM>();
            PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedSupplier):
                {
                    var suppliersId = _selectedSupplier?.TheEntity.Id ?? -1;

                    using (var db = new RetailDbContext())
                    {
                        _selectableProducts = db
                            .Products
                            .Where(product => product.SupplierID == suppliersId)
                            .OrderBy(n => n.Description)
                            .Select(n => new ProductVM { IsNew = false, TheEntity = n })
                            .ToList();
                    }

                    RaisePropertyChanged(nameof(SelectableProducts));
                    PO.SetSupplier(SelectedSupplier);
                    RaisePropertyChanged(nameof(PO));
                    break;
                }
                case nameof(SelectedStore):
                {
                    PO.SetStore(SelectedStore);
                    RaisePropertyChanged(nameof(PO));
                    break;
                }
            }
        }

        public POVM PO { get; set; }

        public ObservableCollection<SupplierVM> Suppliers { get; set; }
        public ObservableCollection<StoreVM> Stores { get; set; }

        private SupplierVM _selectedSupplier;
        private StoreVM _selectedStore;
        private List<ProductVM> _selectableProducts;

        public SupplierVM SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                if (_selectedSupplier != value)
                {
                    _selectedSupplier = value;
                    RaisePropertyChanged();
                }
            }
        }

        public StoreVM SelectedStore
        {
            get => _selectedStore;
            set
            {
                if(_selectedStore != value)
                {
                    _selectedStore = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IReadOnlyList<ProductVM> SelectableProducts => _selectableProducts;

        public ProductVM SelectedProduct { get; set; }

        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;

            using (var db = new RetailDbContext())
            {
                Suppliers = new ObservableCollection<SupplierVM>
                (
                    await db.Suppliers
                    .OrderBy(n => n.Description)
                    .Select(n => new SupplierVM { IsNew = false, TheEntity = n })
                    .ToListAsync()
                    .ConfigureAwait(false)
                );

                Stores = new ObservableCollection<StoreVM>
                (
                    await db.Stores
                    .OrderBy(n => n.Description)
                    .Select(n => new StoreVM { IsNew = false, TheEntity = n })
                    .ToListAsync()
                    .ConfigureAwait(false)
                );
            }

            RaisePropertyChanged(nameof(Suppliers));
            RaisePropertyChanged(nameof(Stores));

            //Create new PO in memory TODO: Add it in memory to db.PO.Add()?? Yes, I think so
            PO = new POVM();

            RaisePropertyChanged(nameof(PO));

            ThrobberVisible = Visibility.Collapsed;
        }
    }
}
