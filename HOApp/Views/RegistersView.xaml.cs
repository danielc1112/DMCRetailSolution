using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for RegistersView.xaml
    /// </summary>
    public partial class RegistersView : UserControl
    {
        public RegistersView()
        {
            InitializeComponent();
            this.DataContext = new RegistersViewModel();
        }
    }
}
