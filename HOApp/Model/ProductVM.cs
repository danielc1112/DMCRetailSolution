using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class ProductVM : VMBase
    {
        public Product TheEntity
        {
            get
            {
                return (Product)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public ProductVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Product();
        }

    }
}
