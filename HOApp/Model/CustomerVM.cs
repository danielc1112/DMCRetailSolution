using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class CustomerVM : VMBase
    {
        public Customer TheEntity
        {
            get
            {
                return (Customer)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public CustomerVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Customer();
            TheEntity.Address = new Address();
        }

    }
}
