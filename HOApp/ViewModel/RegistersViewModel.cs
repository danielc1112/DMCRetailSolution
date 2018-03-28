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
    public class RegistersViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public RegistersViewModel() : base() { }

        private RegisterVM selectedRegister;
        public RegisterVM SelectedRegister
        {
            get
            {
                return selectedRegister;
            }
            set
            {
                selectedRegister = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private RegisterVM editVM;
        public RegisterVM EditVM
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
        public ObservableCollection<RegisterVM> Registers { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<RegisterVM> _Registers = new ObservableCollection<RegisterVM>();
            List<Register> registers;
            registers = await (from p in db.Registers
                                orderby p.RegisterID
                                select p).ToListAsync();
            foreach (Register entity in registers)
            {
                _Registers.Add(new RegisterVM { IsNew = false, TheEntity = entity });
            }
            Registers = _Registers;
            RaisePropertyChanged("Registers");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedRegister;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new RegisterVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Registers.Add(EditVM);
                    db.Registers.Add(EditVM.TheEntity);
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
            if (SelectedRegister != null)
            {
                db.Registers.Remove(SelectedRegister.TheEntity);
                db.SaveChanges();
                Registers.Remove(SelectedRegister);
                RaisePropertyChanged("Registers");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Register selected to delete";
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
            int id = EditVM.TheEntity.RegisterID;
            SelectedRegister = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedRegister = Registers.Where(e => e.TheEntity.RegisterID == id).FirstOrDefault();
                SelectedRegister.TheEntity = SelectedRegister.TheEntity;
                SelectedRegister.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
