using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HOApp.Model
{
    class POVM : VMBase
    {
        public POVM() : base()
        {
            TheEntity = new PO();
            TheEntity.POlines = new List<POline>();
        }

        public ObservableCollection<POlineVM> POlines { get; set; } = new ObservableCollection<POlineVM>();

        public PO TheEntity
        {
            get
            {
                return (PO)base.theEntity;
            }
            set
            {
                theEntity = value;
                RaisePropertyChanged();
            }
        }

        public POVM SetSupplier(SupplierVM viewModel)
        {
            if (TheEntity.SupplierID == viewModel.TheEntity.SupplierID)
                return this;

            // Cannot gaurentee Supplilers will offer same products, so changing providers resets order.
            POlines = new ObservableCollection<POlineVM>();

            TheEntity.SupplierID = viewModel.TheEntity.SupplierID;

            return this;
        }

        public POVM SetStore(StoreVM viewModel)
        {
            TheEntity.StoreID = viewModel.TheEntity.StoreID;
            return this;
        }

        public POlineVM AddProductOrderLine(ProductVM product)
        {
            var viewModel = new POlineVM(product);

            // Add PO to POlIne
            viewModel.TheEntity.POID = TheEntity.POID;

            // Add POline to PO
            TheEntity.POlines.Add(viewModel.TheEntity);

            // Add POlineVM to POVM
            POlines.Add(viewModel);

            return viewModel;
        }
    }
}
