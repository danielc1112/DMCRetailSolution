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
    public class StoresViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public StoresViewModel() : base() { }

        private StoreVM selectedStore;
        public StoreVM SelectedStore
        {
            get
            {
                return selectedStore;
            }
            set
            {
                selectedStore = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private StoreVM editVM;
        public StoreVM EditVM
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
        public ObservableCollection<StoreVM> Stores { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<StoreVM> _Stores = new ObservableCollection<StoreVM>();
            List<Store> stores;
            if (filter != "")
            {
                stores = await (from p in db.Stores
                                   where p.Description.Contains(filter)
                                   orderby p.Description
                                   select p).Include(a => a.Address).ToListAsync();
            }
            else
            {
                stores = await (from p in db.Stores
                                   orderby p.Description
                                   select p).Include(a => a.Address).ToListAsync();
            }
            foreach (Store entity in stores)
            {
                _Stores.Add(new StoreVM { IsNew = false, TheEntity = entity });
            }
            Stores = _Stores;
            RaisePropertyChanged("Stores");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedStore;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new StoreVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Stores.Add(EditVM);
                    db.Stores.Add(EditVM.TheEntity);
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
            if (SelectedStore != null)
            {
                db.Addresses.Remove(SelectedStore.TheEntity.Address);
                db.Stores.Remove(SelectedStore.TheEntity);
                db.SaveChanges();
                Stores.Remove(SelectedStore);
                RaisePropertyChanged("Stores");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Store selected to delete";
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
            int id = EditVM.TheEntity.StoreID;
            SelectedStore = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedStore = Stores.Where(e => e.TheEntity.StoreID == id).FirstOrDefault();
                SelectedStore.TheEntity = SelectedStore.TheEntity;
                SelectedStore.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
