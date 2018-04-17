using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class StoreProductTranVM : VMBase
    {
        public StoreProductTran TheEntity
        {
            get
            {
                return (StoreProductTran)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public StoreProductTranVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new StoreProductTran();
        }

    }
}
