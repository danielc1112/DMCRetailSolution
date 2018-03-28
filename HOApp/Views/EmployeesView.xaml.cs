using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
            this.DataContext = new EmployeesViewModel();
        }
    }
}
