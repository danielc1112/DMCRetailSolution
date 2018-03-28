using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for SuppliersView.xaml
    /// </summary>
    public partial class SuppliersView : UserControl
    {
        public SuppliersView()
        {
            InitializeComponent();
            this.DataContext = new SuppliersViewModel();
        }
    }
}
