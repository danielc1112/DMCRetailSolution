using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class TenderTypeVM : VMBase
    {
        public TenderType TheEntity
        {
            get
            {
                return (TenderType)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public TenderTypeVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new TenderType();
        }

    }
}
