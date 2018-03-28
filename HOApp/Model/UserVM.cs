using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class UserVM : VMBase
    {
        public User TheEntity
        {
            get
            {
                return (User)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public UserVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new User();
        }

    }
}
