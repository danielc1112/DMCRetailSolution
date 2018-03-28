using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entity.Entities;
using DataAccess;
using HOApp.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using HOApp.Messages;

namespace HOApp.ViewModel
{
    public class ProductsViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public ProductsViewModel() : base() {}

        private ProductVM selectedProduct;
        public ProductVM SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private ProductVM editVM;
        public ProductVM EditVM
        {
            get
            {
                return editVM;
            }
            set
            {
                editVM = value;
                editEntity = editVM.TheEntity;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<ProductVM> Products { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<ProductVM> _products = new ObservableCollection<ProductVM>();
            List<Product> products;
            if (filter != "")
            {
                products = await (from p in db.Products
                                      where p.Description.Contains(filter)
                                      orderby p.Description
                                      select p).ToListAsync();
            }
            else
            {
                products = await (from p in db.Products
                                      orderby p.Description
                                      select p).ToListAsync();
            }
            foreach (Product prod in products)
            {
                _products.Add(new ProductVM { IsNew = false, TheEntity = prod });
            }
            Products = _products;
            RaisePropertyChanged("Products");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedProduct;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new ProductVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Products.Add(EditVM);
                    db.Products.Add(EditVM.TheEntity);
                    UpdateDB();
                }
                else if (db.ChangeTracker.HasChanges())
                {
                    UpdateDB();
                }
                else
                {
                    ShowUserMessage("No changes to save");
                }
            }
            else
            {
                ShowUserMessage("There are problems with the data entered");
            }
        }

        protected override void DeleteCurrent()
        {
            UserMessage msg = new UserMessage();
            if (SelectedProduct != null)
            {
                int NumSaleLines = NumberOfSaleLines(SelectedProduct.TheEntity.ProductID);
                int NumGrnLines = NumberOfSaleLines(SelectedProduct.TheEntity.ProductID);
                if ((NumSaleLines > 0) || (NumGrnLines > 0))
                {
                    if (NumSaleLines > 0)
                        msg.Message = string.Format("Cannot delete - there are {0} Sale Lines for this Product", NumSaleLines);
                    else if (NumGrnLines > 0)
                        msg.Message = string.Format("Cannot delete - there are {0} Grn Lines for this Product", NumGrnLines);
                }
                else
                {
                    db.Products.Remove(SelectedProduct.TheEntity);
                    db.SaveChanges();
                    Products.Remove(SelectedProduct);
                    RaisePropertyChanged("Products");
                    msg.Message = "Deleted";
                }
            }
            else
            {
                msg.Message = "No Product selected to delete";
            }
            Messenger.Default.Send<UserMessage>(msg);
        }

        private async void UpdateDB()
        {
            try
            {
                await db.SaveChangesAsync();
                ShowUserMessage("Database Updated");
            }
            catch (Exception e)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    ErrorMessage = e.InnerException.GetBaseException().ToString();
                }
                ShowUserMessage("There was a problem updating the database");
            }
            ReFocusRow();
        }
        protected override void Quit()
        {
            if (!EditVM.IsNew)
            {
                ReFocusRow();
            }
            else
                IsInEditMode = false;
        }
        private async void ReFocusRow(bool withReload = true)
        {
            int id = EditVM.TheEntity.ProductID;
            SelectedProduct = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedProduct = Products.Where(e => e.TheEntity.ProductID == id).FirstOrDefault();
                SelectedProduct.TheEntity = SelectedProduct.TheEntity;
                SelectedProduct.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }
        private int NumberOfSaleLines(int ProdId)
        {
            var prod = db.Products.Find(ProdId);
            // Count how many SaleLines there are for the Product
            int linesCount = db.Salelines
                               .Where(sl => sl.ProductID == prod.ProductID)
                               .Count();
            return linesCount;
        }

        private int NumberOfGrnLines(int ProdId)
        {
            var prod = db.Products.Find(ProdId);
            // Count how many SaleLines there are for the Product
            int linesCount = db.Grnlines
                               .Where(gl => gl.ProductID == prod.ProductID)
                               .Count();
            return linesCount;
        }

    }
}
