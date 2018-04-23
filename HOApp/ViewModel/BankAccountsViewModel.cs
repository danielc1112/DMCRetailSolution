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
    public class BankAccountsViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public BankAccountsViewModel() : base() { }

        private BankAccountVM selectedBankAccount;
        public BankAccountVM SelectedBankAccount
        {
            get
            {
                return selectedBankAccount;
            }
            set
            {
                selectedBankAccount = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private BankAccountVM editVM;
        public BankAccountVM EditVM
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
        public ObservableCollection<BankAccountVM> BankAccounts { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<BankAccountVM> _BankAccounts = new ObservableCollection<BankAccountVM>();
            List<BankAccount> bankAccounts;
            if (filter != "")
            {
                bankAccounts = await (from p in db.BankAccounts
                               where (p.BankName + p.AccountName).Contains(filter)
                               orderby p.AccountName
                               select p).ToListAsync();
            }
            else
            {
                bankAccounts = await (from p in db.BankAccounts
                               orderby p.AccountName
                               select p).ToListAsync();
            }
            foreach (BankAccount entity in bankAccounts)
            {
                _BankAccounts.Add(new BankAccountVM { IsNew = false, TheEntity = entity });
            }
            BankAccounts = _BankAccounts;
            RaisePropertyChanged("BankAccounts");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedBankAccount;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new BankAccountVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    BankAccounts.Add(EditVM);
                    db.BankAccounts.Add(EditVM.TheEntity);
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
            if (SelectedBankAccount != null)
            {
                db.BankAccounts.Remove(SelectedBankAccount.TheEntity);
                db.SaveChanges();
                BankAccounts.Remove(SelectedBankAccount);
                RaisePropertyChanged("BankAccounts");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No BankAccount selected to delete";
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
            SelectedBankAccount = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedBankAccount = BankAccounts.Where(e => e.TheEntity.Id == id).FirstOrDefault();
                SelectedBankAccount.TheEntity = SelectedBankAccount.TheEntity;
                SelectedBankAccount.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
