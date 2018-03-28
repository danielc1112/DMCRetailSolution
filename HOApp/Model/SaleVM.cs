using DataAccess.Entity.Entities;
using System.Collections.ObjectModel;

namespace HOApp.Model
{
    class SaleVM : VMBase
    {
        private ObservableCollection<SalelineVM> _salelines = new ObservableCollection<SalelineVM>();
        public ObservableCollection<SalelineVM> Salelines
        {
            get { return _salelines; }
        }

        public Sale TheEntity
        {
            get
            {
                return (Sale)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public SaleVM()
        {
            TheEntity = new Sale();
        }

    }
}
