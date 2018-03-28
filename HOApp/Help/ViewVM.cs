using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HOApp.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace HOApp.Help
{
    public class ViewVM
    {
        public Type ViewType { get; set; }
        public Type ViewModelType { get; set; }
        public UserControl View { get; set; }
        public RelayCommand Navigate { get; set; }
        public string Header { get; set; }
        public ObservableCollection<ViewVM> MenuItems { get; set; }

        public ViewVM()
        {
            Navigate = new RelayCommand(NavigateExecute);
        }
        public void NavigateExecute()
        {
            if (View == null && ViewType != null)
            {
                View = (UserControl)Activator.CreateInstance(ViewType);
            }
            var msg = new NavigateMessage { View = View, ViewModelType = ViewModelType, ViewType = ViewType };
            Messenger.Default.Send<NavigateMessage>(msg);
        }
    }
}
