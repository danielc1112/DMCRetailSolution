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
    public class PrintersViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public PrintersViewModel() : base() { }

        private PrinterVM selectedPrinter;
        public PrinterVM SelectedPrinter
        {
            get
            {
                return selectedPrinter;
            }
            set
            {
                selectedPrinter = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private PrinterVM editVM;
        public PrinterVM EditVM
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
        public ObservableCollection<PrinterVM> Printers { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<PrinterVM> _Printers = new ObservableCollection<PrinterVM>();
            List<Printer> printers;
            if (filter != "")
            {
                printers = await (from p in db.Printers
                                   where p.Description.Contains(filter)
                                   orderby p.Description
                                   select p).ToListAsync();
            }
            else
            {
                printers = await (from p in db.Printers
                                   orderby p.Description
                                   select p).ToListAsync();
            }
            foreach (Printer entity in printers)
            {
                _Printers.Add(new PrinterVM { IsNew = false, TheEntity = entity });
            }
            Printers = _Printers;
            RaisePropertyChanged("Printers");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedPrinter;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new PrinterVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Printers.Add(EditVM);
                    db.Printers.Add(EditVM.TheEntity);
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
            if (SelectedPrinter != null)
            {
                db.Printers.Remove(SelectedPrinter.TheEntity);
                db.SaveChanges();
                Printers.Remove(SelectedPrinter);
                RaisePropertyChanged("Printers");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Printer selected to delete";
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
            int id = EditVM.TheEntity.PrinterID;
            SelectedPrinter = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedPrinter = Printers.Where(e => e.TheEntity.PrinterID == id).FirstOrDefault();
                SelectedPrinter.TheEntity = SelectedPrinter.TheEntity;
                SelectedPrinter.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
