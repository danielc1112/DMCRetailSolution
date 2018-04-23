using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class StoreVM : VMBase
    {
        public Store TheEntity
        {
            get
            {
                return (Store)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public StoreVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Store();
            TheEntity.Address = new Address();
        }

    }
}
