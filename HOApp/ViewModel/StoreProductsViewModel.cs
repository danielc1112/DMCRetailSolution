using DataAccess;
using HOApp.Model;
using System.Collections.ObjectModel;
using DataAccess.Help;
using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HOApp.ViewModel
{
    class StoreProductsViewModel : NotifyUIBase
    {
        private RetailDbContext db = new RetailDbContext();

        public StoreProductsViewModel(Product prod) : base()
        {
            LoadData(prod);
        }

        public ObservableCollection<StoreProductVM> StoreProducts { get; set; }
        public Product Product { get; set; }
        public void LoadData(Product prod)
        {
            Product = prod;
            ObservableCollection<StoreProductVM> _storeProducts = new ObservableCollection<StoreProductVM>();

            List<Store> allStores = (from s in db.Stores
                                     select s).ToList();

            List<StoreProduct> storeProducts = (from s in db.StoreProducts
                                                where s.ProductID.Equals(prod.ProductID)
                                                select s).ToList();
            foreach (Store store in allStores)
            {
                StoreProduct storeProduct = storeProducts.Find(s => s.StoreID == store.StoreID);
                if (storeProduct == null)
                {
                    storeProduct = new StoreProduct() { StoreID = store.StoreID, ProductID = prod.ProductID, QtyOnHand = 0, Store = store };
                }
                _storeProducts.Add(new StoreProductVM { IsNew = false, TheEntity = storeProduct });
            }

            StoreProducts = _storeProducts;
            RaisePropertyChanged("StoreProducts");
        }

    }
}
