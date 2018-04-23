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
    public class TenderTypesViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public TenderTypesViewModel() : base() { }

        private TenderTypeVM selectedTenderType;
        public TenderTypeVM SelectedTenderType
        {
            get
            {
                return selectedTenderType;
            }
            set
            {
                selectedTenderType = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private TenderTypeVM editVM;
        public TenderTypeVM EditVM
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
        public ObservableCollection<TenderTypeVM> TenderTypes { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<TenderTypeVM> _TenderTypes = new ObservableCollection<TenderTypeVM>();
            List<TenderType> tenderTypes;
            if (filter != "")
            {
                tenderTypes = await (from p in db.TenderTypes
                                   where p.Description.Contains(filter)
                                   orderby p.Description
                                   select p).ToListAsync();
            }
            else
            {
                tenderTypes = await (from p in db.TenderTypes
                                   orderby p.Description
                                   select p).ToListAsync();
            }
            foreach (TenderType entity in tenderTypes)
            {
                _TenderTypes.Add(new TenderTypeVM { IsNew = false, TheEntity = entity });
            }
            TenderTypes = _TenderTypes;
            RaisePropertyChanged("TenderTypes");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedTenderType;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new TenderTypeVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    TenderTypes.Add(EditVM);
                    db.TenderTypes.Add(EditVM.TheEntity);
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
            if (SelectedTenderType != null)
            {
                db.TenderTypes.Remove(SelectedTenderType.TheEntity);
                db.SaveChanges();
                TenderTypes.Remove(SelectedTenderType);
                RaisePropertyChanged("TenderTypes");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No TenderType selected to delete";
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
            SelectedTenderType = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedTenderType = TenderTypes.Where(e => e.TheEntity.Id == id).FirstOrDefault();
                SelectedTenderType.TheEntity = SelectedTenderType.TheEntity;
                SelectedTenderType.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
