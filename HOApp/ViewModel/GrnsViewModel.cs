using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using System.Collections.ObjectModel;
using HOApp.Model;
using System.Windows;
using System.Data.Entity;

namespace HOApp.ViewModel
{
    class GrnsViewModel : DataVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public GrnsViewModel() : base() { }

        public ObservableCollection<GrnVM> Grns { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<GrnVM> _grns = new ObservableCollection<GrnVM>();

            var grns = await (from s in db.Grns
                               orderby s.TransactionTime
                               select s).ToListAsync();
            foreach (Grn grn in grns)
            {
                _grns.Add(new GrnVM { IsNew = false, TheEntity = grn });
            }
            Grns = _grns;
            RaisePropertyChanged("Grns");
            ThrobberVisible = Visibility.Collapsed;
        }

    }
}
