using System.Windows.Controls;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            this.DataContext = new UsersViewModel();
        }
    }
}
