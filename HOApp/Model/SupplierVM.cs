using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class SupplierVM : VMBase
    {
        public Supplier TheEntity
        {
            get
            {
                return (Supplier)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public SupplierVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Supplier();
            TheEntity.Address = new Address();
        }

    }
}
