using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using System.Collections.ObjectModel;
using HOApp.Model;
using System.Windows;
using System.Data.Entity;

namespace HOApp.ViewModel
{
    class SalesViewModel : DataVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public SalesViewModel() : base() { }

        public ObservableCollection<SaleVM> Sales { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<SaleVM> _sales = new ObservableCollection<SaleVM>();
            
            var sales = await (from s in db.Sales
                                  orderby s.TransactionTime
                                  select s)
                                  //.Include(s => s.Salelines) //Eager load the salelines as well
                                  .ToListAsync();
            foreach (Sale sale in sales)
            {
                _sales.Add(new SaleVM { IsNew = false, TheEntity = sale });
            }
            Sales = _sales;
            RaisePropertyChanged("Sales");
            ThrobberVisible = Visibility.Collapsed;
        }

    }
}
