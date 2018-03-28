using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class BankAccountVM : VMBase
    {
        public BankAccount TheEntity
        {
            get
            {
                return (BankAccount)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public BankAccountVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new BankAccount();
        }

    }
}
