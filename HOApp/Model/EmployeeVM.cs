using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class EmployeeVM : VMBase
    {
        public Employee TheEntity
        {
            get
            {
                return (Employee)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public EmployeeVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Employee();
            TheEntity.Address = new Address();
        }

    }
}
