using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for BankAccountsView.xaml
    /// </summary>
    public partial class BankAccountsView : UserControl
    {
        public BankAccountsView()
        {
            InitializeComponent();
            this.DataContext = new BankAccountsViewModel();
        }
    }
}
