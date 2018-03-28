using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for POEntryView.xaml
    /// </summary>
    public partial class POEntryView : UserControl
    {
        public POEntryView()
        {
            InitializeComponent();
            this.DataContext = new POEntryViewModel();
        }
    }
}
