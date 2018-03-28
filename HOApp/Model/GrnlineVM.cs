using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    class GrnlineVM : VMBase
    {
        public Grnline TheEntity
        {
            get
            {
                return (Grnline)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public GrnlineVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Grnline();
        }

    }
}
