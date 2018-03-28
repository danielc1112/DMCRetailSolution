using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Help;
using System.Windows;

namespace HOApp.ViewModel
{
    public class DataVMBase : NotifyUIBase
    {
        private Visibility throbberVisible = Visibility.Visible;
        public Visibility ThrobberVisible
        {
            get { return throbberVisible; }
            set
            {
                throbberVisible = value;
                RaisePropertyChanged();
            }
        }

        protected virtual void GetData(string filter = "")
        {
        }

        protected DataVMBase()
        {
            GetData();
        }
    }
}
