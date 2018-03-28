using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for TenderTypesView.xaml
    /// </summary>
    public partial class TenderTypesView : UserControl
    {
        public TenderTypesView()
        {
            InitializeComponent();
            this.DataContext = new TenderTypesViewModel();
        }
    }
}
