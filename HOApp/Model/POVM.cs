using DataAccess.Entity.Entities;
using System.Collections.ObjectModel;

namespace HOApp.Model
{
    class POVM : VMBase
    {
        private ObservableCollection<POlineVM> _polines = new ObservableCollection<POlineVM>();
        public ObservableCollection<POlineVM> POlines
        {
            get { return _polines; }
        }

        public PO TheEntity
        {
            get
            {
                return (PO)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public POVM()
        {
            TheEntity = new PO();
        }

    }
}
