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
    public class CustomersViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public CustomersViewModel() : base() { }

        private CustomerVM selectedCustomer;
        public CustomerVM SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private CustomerVM editVM;
        public CustomerVM EditVM
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
        public ObservableCollection<CustomerVM> Customers { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<CustomerVM> _Customers = new ObservableCollection<CustomerVM>();
            List<Customer> customers;
            if (filter != "")
            {
                customers = await (from p in db.Customers
                                   where (p.FirstName + p.LastName).Contains(filter)
                                   orderby (p.FirstName + p.LastName)
                                   select p).Include(a => a.Address).ToListAsync();
            }
            else
            {
                customers = await (from p in db.Customers
                                   orderby (p.FirstName + p.LastName)
                                   select p).Include(a => a.Address).ToListAsync();
            }
            foreach (Customer entity in customers)
            {
                _Customers.Add(new CustomerVM { IsNew = false, TheEntity = entity });
            }
            Customers = _Customers;
            RaisePropertyChanged("Customers");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedCustomer;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new CustomerVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Customers.Add(EditVM);
                    db.Customers.Add(EditVM.TheEntity);
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
            if (SelectedCustomer != null)
            {
                db.Addresses.Remove(SelectedCustomer.TheEntity.Address);
                db.Customers.Remove(SelectedCustomer.TheEntity);                
                db.SaveChanges();
                Customers.Remove(SelectedCustomer);
                RaisePropertyChanged("Customers");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Customer selected to delete";
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
            SelectedCustomer = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedCustomer = Customers.Where(e => e.TheEntity.Id == id).FirstOrDefault();
                SelectedCustomer.TheEntity = SelectedCustomer.TheEntity;
                SelectedCustomer.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
