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
        private RetailDbContext db = new RetailDbContext();

        public POVM PO { get; set; }
        public ObservableCollection<SupplierVM> Suppliers { get; set; }

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

        public POEntryViewModel() : base() { }

        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;

            //Supplier dropdown
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

            //Create new PO in memory
            POVM _PO = new POVM();
            PO = _PO;
            RaisePropertyChanged("PO");

            ThrobberVisible = Visibility.Collapsed;
        }


    }
}
