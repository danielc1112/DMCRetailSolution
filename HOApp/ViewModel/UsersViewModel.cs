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
    public class UsersViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public UsersViewModel() : base() { }

        private UserVM selectedUser;
        public UserVM SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private UserVM editVM;
        public UserVM EditVM
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
        public ObservableCollection<UserVM> Users { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<UserVM> _Users = new ObservableCollection<UserVM>();
            List<User> users;
            if (filter != "")
            {
                users = await (from p in db.Users
                               where p.UserCode.Contains(filter)
                               orderby p.UserCode
                               select p).ToListAsync();
            }
            else
            {
                users = await (from p in db.Users
                               orderby p.UserCode
                               select p).ToListAsync();
            }
            foreach (User entity in users)
            {
                _Users.Add(new UserVM { IsNew = false, TheEntity = entity });
            }
            Users = _Users;
            RaisePropertyChanged("Users");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedUser;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new UserVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Users.Add(EditVM);
                    db.Users.Add(EditVM.TheEntity);
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
            if (SelectedUser != null)
            {
                db.Users.Remove(SelectedUser.TheEntity);
                db.SaveChanges();
                Users.Remove(SelectedUser);
                RaisePropertyChanged("Users");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No User selected to delete";
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
            int id = EditVM.TheEntity.UserID;
            SelectedUser = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedUser = Users.Where(e => e.TheEntity.UserID == id).FirstOrDefault();
                SelectedUser.TheEntity = SelectedUser.TheEntity;
                SelectedUser.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
