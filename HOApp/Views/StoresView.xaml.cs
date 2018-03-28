using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for StoresView.xaml
    /// </summary>
    public partial class StoresView : UserControl
    {
        public StoresView()
        {
            InitializeComponent();
            this.DataContext = new StoresViewModel();
        }
    }
}
