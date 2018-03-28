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
    public class EmployeesViewModel : CrudVMBase
    {
        private RetailDbContext db = new RetailDbContext();

        public EmployeesViewModel() : base() { }

        private EmployeeVM selectedEmployee;
        public EmployeeVM SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                selectedEntity = value;
                RaisePropertyChanged();
            }
        }

        private EmployeeVM editVM;
        public EmployeeVM EditVM
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
        public ObservableCollection<EmployeeVM> Employees { get; set; }
        protected async override void GetData(string filter = "")
        {
            ThrobberVisible = Visibility.Visible;
            ObservableCollection<EmployeeVM> _Employees = new ObservableCollection<EmployeeVM>();
            List<Employee> employees;
            if (filter != "")
            {
                employees = await (from p in db.Employees
                                   where (p.FirstName + p.LastName).Contains(filter)
                                   orderby (p.FirstName + p.LastName)
                                   select p).Include(a => a.Address).ToListAsync();
            }
            else
            {
                employees = await (from p in db.Employees
                                   orderby (p.FirstName + p.LastName)
                                   select p).Include(a => a.Address).ToListAsync();
            }
            foreach (Employee entity in employees)
            {
                _Employees.Add(new EmployeeVM { IsNew = false, TheEntity = entity });
            }
            Employees = _Employees;
            RaisePropertyChanged("Employees");
            ThrobberVisible = Visibility.Collapsed;
        }
        protected override void EditCurrent()
        {
            EditVM = SelectedEmployee;
            IsInEditMode = true;
        }
        protected override void InsertNew()
        {
            EditVM = new EmployeeVM();
            IsInEditMode = true;
        }
        protected override void CommitUpdates()
        {
            if (EditVM.TheEntity.IsValid())
            {
                if (EditVM.IsNew)
                {
                    EditVM.IsNew = false;
                    Employees.Add(EditVM);
                    db.Employees.Add(EditVM.TheEntity);
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
            if (SelectedEmployee != null)
            {
                db.Addresses.Remove(SelectedEmployee.TheEntity.Address);
                db.Employees.Remove(SelectedEmployee.TheEntity);
                db.SaveChanges();
                Employees.Remove(SelectedEmployee);
                RaisePropertyChanged("Employees");
                msg.Message = "Deleted";
            }
            else
            {
                msg.Message = "No Employee selected to delete";
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
            int id = EditVM.TheEntity.EmployeeID;
            SelectedEmployee = null;
            await db.Entry(EditVM.TheEntity).ReloadAsync();
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                SelectedEmployee = Employees.Where(e => e.TheEntity.EmployeeID == id).FirstOrDefault();
                SelectedEmployee.TheEntity = SelectedEmployee.TheEntity;
                SelectedEmployee.TheEntity.ClearErrors();
            }), DispatcherPriority.ContextIdle);
            IsInEditMode = false;
        }

    }
}
