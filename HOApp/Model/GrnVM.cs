using DataAccess.Entity.Entities;
using System.Collections.ObjectModel;

namespace HOApp.Model
{
    class GrnVM : VMBase
    {
        private ObservableCollection<GrnlineVM> _grnlines = new ObservableCollection<GrnlineVM>();
        public ObservableCollection<GrnlineVM> Grnlines
        {
            get { return _grnlines; }
        }

        public Grn TheEntity
        {
            get
            {
                return (Grn)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public GrnVM()
        {
            TheEntity = new Grn();
        }

    }
}
