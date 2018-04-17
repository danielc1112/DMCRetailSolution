using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataAccess;
using DataAccess.Entity.Entities;
using DataAccess.Help;
using HOApp.Model;

namespace HOApp.ViewModel
{
    class POEntryViewModel : DataVMBase
    {
        public RetailDbContext db = new RetailDbContext();

        public POVM PO { get; set; }
        public ObservableCollection<SupplierVM> Suppliers { get; set; }
        public ObservableCollection<StoreVM> Stores { get; set; }
        public ObservableCollection<ProductVM> Products { get; set; }

        private SupplierVM selectedSupplier;
        public SupplierVM SelectedSupplier
        {
            get
            {
                return selectedSupplier;
            }
            set
            {
                selectedSupplier = value;
                RaisePropertyChanged();
            }
        }

        private StoreVM selectedStore;
        public StoreVM SelectedStore
        {
            get
            {
                return selectedStore;
            }
            set
            {
                selectedStore = value;
                RaisePropertyChanged();
            }
        }

        public POEntryViewModel() : base() { }

        protected async override void GetData(string filter = "")
        {
            //TODO: The throbber in each screen works, but it's covered in the UI
            ThrobberVisible = Visibility.Visible;

            //Supplier dropdown TODO: Should only look at active suppliers
            ObservableCollection<SupplierVM> _suppliers = new ObservableCollection<SupplierVM>();
            var suppliers = await (from s in db.Suppliers
                                   orderby s.Description
                                   select s).ToListAsync();
            foreach (Supplier supplier in suppliers)
            {
                _suppliers.Add(new SupplierVM { IsNew = false, TheEntity = supplier });
            }
            Suppliers = _suppliers;
            RaisePropertyChanged("Suppliers");

            //Store dropdown TODO: Should only look at active stores
            ObservableCollection<StoreVM> _stores = new ObservableCollection<StoreVM>();
            var stores = await (from s in db.Stores
                                   orderby s.Description
                                   select s).ToListAsync();
            foreach (Store store in stores)
            {
                _stores.Add(new StoreVM { IsNew = false, TheEntity = store });
            }
            Stores = _stores;
            RaisePropertyChanged("Stores");

            //Product dropdown TODO: Should only look at products with an active status
            ObservableCollection<ProductVM> _products = new ObservableCollection<ProductVM>();
            var products = await (from s in db.Products
                                orderby s.Description
                                select s).ToListAsync();
            foreach (Product product in products)
            {
                _products.Add(new ProductVM { IsNew = false, TheEntity = product });
            }
            Products = _products;
            RaisePropertyChanged("Products");

            //Create new PO in memory TODO: Add it in memory to db.PO.Add()?? Yes, I think so
            POVM _PO = new POVM();
            PO = _PO;
            RaisePropertyChanged("PO");

            ThrobberVisible = Visibility.Collapsed;
        }


    }
}
