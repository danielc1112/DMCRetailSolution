using DataAccess.Entity.Entities;

namespace HOApp.Model
{
    class POlineVM : VMBase
    {
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

        public POlineVM(ProductVM viewModel)
        {
            IsNew = true;

            Description = viewModel.TheEntity.Description;

            TheEntity = new POline
            {
                ProductID = viewModel.TheEntity.ProductID,
                Cost = viewModel.TheEntity.Cost
            };
        }

        public int Quantity
        {
            get => TheEntity.Quantity;
            set
            {
                TheEntity.Quantity = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LineAmount));
            }
        }

        public float Cost => TheEntity.Cost;

        public float LineAmount => TheEntity.LineAmount;

        public string Description { get; }
    }
}
