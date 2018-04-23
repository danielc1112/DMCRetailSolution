using DataAccess.Entity.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HOApp.Model
{
    class POVM : VMBase
    {
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

        public POVM()
        {
            TheEntity = new PO();
            TheEntity.POlines = new List<POline>();
        }

        public POVM SetSupplier(SupplierVM viewModel)
        {
            if (TheEntity.SupplierID == viewModel.TheEntity.Id)
                return this;

            POlines = new ObservableCollection<POlineVM>();

            TheEntity.SupplierID = viewModel.TheEntity.Id;

            return this;
        }

        public POVM SetStore(StoreVM viewModel)
        {
            TheEntity.StoreID = viewModel.TheEntity.Id;
            return this;
        }

        public POVM SetEmployee(EmployeeVM viewModel)
        {
            TheEntity.EmployeeID = viewModel.TheEntity.Id;
            return this;
        }

        public POlineVM AddProductOrderLine()
        {
            var viewModel = new POlineVM();

            viewModel.TheEntity.POID = TheEntity.Id;

            POlines.Add(viewModel);

            TheEntity.POlines.Add(viewModel.TheEntity);

            return viewModel;
        }
    }
}
