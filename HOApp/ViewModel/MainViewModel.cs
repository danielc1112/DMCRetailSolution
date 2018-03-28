using System.Collections.ObjectModel;
using DataAccess.Help;
using HOApp.Help;
using HOApp.Views;

namespace HOApp.ViewModel
{
    public class MainViewModel : NotifyUIBase
    {
        public ObservableCollection<ViewVM> MenuItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {            
            ObservableCollection<ViewVM> menuItems = new ObservableCollection<ViewVM>
            {
                new ViewVM { Header = "Inventory",
                   MenuItems = new ObservableCollection<ViewVM>
                   {
                       new ViewVM{ Header="Products", ViewType = typeof(ProductsView), ViewModelType = typeof(ProductsViewModel)},
                       new ViewVM{ Header="Suppliers", ViewType = typeof(SuppliersView), ViewModelType = typeof(SuppliersViewModel)},
                       new ViewVM{ Header="Create Purchase Order", ViewType = typeof(POEntryView), ViewModelType = typeof(POEntryViewModel)}
                   }
                },
                new ViewVM { Header = "User/Employee",
                   MenuItems = new ObservableCollection<ViewVM>
                   {
                       new ViewVM{ Header="Users", ViewType = typeof(UsersView), ViewModelType = typeof(UsersViewModel)},
                       new ViewVM{ Header="Employees", ViewType = typeof(EmployeesView), ViewModelType = typeof(EmployeesViewModel)}
                   }
                },
                new ViewVM { Header = "Accounting",
                   MenuItems = new ObservableCollection<ViewVM>
                   {
                       new ViewVM{ Header="Bank Accounts", ViewType = typeof(BankAccountsView), ViewModelType = typeof(BankAccountsViewModel)}
                   }
                },
                new ViewVM { Header = "POS",
                   MenuItems = new ObservableCollection<ViewVM>
                   {
                       new ViewVM{ Header="Stores", ViewType = typeof(StoresView), ViewModelType = typeof(StoresViewModel)},
                       new ViewVM{ Header="Registers", ViewType = typeof(RegistersView), ViewModelType = typeof(RegistersViewModel)},
                       new ViewVM{ Header="Printers", ViewType = typeof(PrintersView), ViewModelType = typeof(PrintersViewModel)},
                       new ViewVM{ Header="Customers", ViewType = typeof(CustomersView), ViewModelType = typeof(CustomersViewModel)},
                       new ViewVM{ Header="TenderTypes", ViewType = typeof(TenderTypesView), ViewModelType = typeof(TenderTypesViewModel)}
                   }
                },
                new ViewVM { Header = "Enquiry",
                   MenuItems = new ObservableCollection<ViewVM>
                   {
                       new ViewVM{ Header="Sales", ViewType = typeof(SalesView), ViewModelType = typeof(SalesViewModel)},
                       new ViewVM{ Header="Goods Received", ViewType = typeof(GrnsView), ViewModelType = typeof(GrnsViewModel)}
                   }
                }
            };
            MenuItems = menuItems;
            RaisePropertyChanged("Views");
            MenuItems[0].NavigateExecute();
        }
    }
}