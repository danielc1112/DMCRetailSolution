using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    class POlineVM : VMBase
    {
        public float Cost
        {
            get => TheEntity.Cost;
            set
            {
                TheEntity.Cost = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Lineamount));
            }
        }

        public int Quantity
        {
            get => TheEntity.Quantity;
            set
            {
                TheEntity.Quantity = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Lineamount));
            }
        }

        public float Lineamount => Cost * Quantity;

        public string Description { get; set; }

        public POline TheEntity
        {
            get
            {
                return (POline)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }

        public POlineVM()
        {
            // Initialise the entity or inserts will fail
            TheEntity = new POline();
        }


        public POlineVM SetProduct(ProductVM viewModel)
        {
            TheEntity.ProductID = viewModel.TheEntity.Id;
            Description = viewModel.TheEntity.Description;
            Cost = viewModel.TheEntity.Cost;
            return this;
        }
    }
}
