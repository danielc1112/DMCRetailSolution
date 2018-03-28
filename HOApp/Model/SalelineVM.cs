using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    class SalelineVM : VMBase
    {
        public Saleline TheEntity
        {
            get
            {
                return (Saleline)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public SalelineVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Saleline();
        }

    }
}
