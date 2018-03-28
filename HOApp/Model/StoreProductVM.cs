using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class StoreProductVM : VMBase
    {
        public StoreProduct TheEntity
        {
            get
            {
                return (StoreProduct)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public StoreProductVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new StoreProduct();
        }

    }
}
