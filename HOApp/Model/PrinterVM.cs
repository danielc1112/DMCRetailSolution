using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    public class PrinterVM : VMBase
    {
        public Printer TheEntity
        {
            get
            {
                return (Printer)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }
        public PrinterVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new Printer();
        }

    }
}
