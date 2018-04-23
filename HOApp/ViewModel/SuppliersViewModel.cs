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
    public class SuppliersViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public SuppliersViewModel() : base() { }

        private SupplierVM selectedSupplier;
        public SupplierVM SelectedSupplier
        {
            get
            {
                return selectedSupplier;
            }
            set
            {
                selectedSupplier = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private SupplierVM editVM;
        public SupplierVM EditVM
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
        public ObservableCollection<SupplierVM> Suppliers { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<SupplierVM> _Suppliers = new ObservableCollection<SupplierVM>();
            List<Supplier> suppliers;
            if (filter != "")
            {
                suppliers = await (from p in db.Suppliers
                                  where p.Description.Contains(filter)
                                  orderby p.Description
                                  select p).Include(a => a.Address).ToListAsync();
            }
            else
            {
                suppliers = await (from p in db.Suppliers
                                  orderby p.Description
                                  select p).Include(a => a.Address).ToListAsync();
            }
            foreach (Supplier entity in suppliers)
            {
                _Suppliers.Add(new SupplierVM { IsNew = false, TheEntity = entity });
            }
            Suppliers = _Suppliers;
            RaisePropertyChanged("Suppliers");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedSupplier;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new SupplierVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Suppliers.Add(EditVM);
                    db.Suppliers.Add(EditVM.TheEntity);
                    db.Addresses.Add(EditVM.TheEntity.Address);
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
            if (SelectedSupplier != null)
            {
                db.Addresses.Remove(SelectedSupplier.TheEntity.Address);
                db.Suppliers.Remove(SelectedSupplier.TheEntity);
                db.SaveChanges();
                Suppliers.Remove(SelectedSupplier);
                RaisePropertyChanged("Suppliers");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Supplier selected to delete";
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
            int id = EditVM.TheEntity.Id;
            SelectedSupplier = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedSupplier = Suppliers.Where(e => e.TheEntity.Id == id).FirstOrDefault();
                SelectedSupplier.TheEntity = SelectedSupplier.TheEntity;
                SelectedSupplier.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
