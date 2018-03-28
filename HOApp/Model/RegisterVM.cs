using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class RegisterVM : VMBase
    {
        public Register TheEntity
        {
            get
            {
                return (Register)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public RegisterVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Register();
        }

    }
}
