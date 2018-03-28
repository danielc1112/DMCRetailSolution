using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for PrintersView.xaml
    /// </summary>
    public partial class PrintersView : UserControl
    {
        public PrintersView()
        {
            InitializeComponent();
            this.DataContext = new PrintersViewModel();
        }
    }
}
