using System.Windows;
using HOApp.ViewModel;
using DataAccess.Entity.Entities;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for StoreProductsView.xaml
    /// </summary>
    public partial class StoreProductsView : Window
    {
        public StoreProductsView(Product prod)
        {
            InitializeComponent();
            this.DataContext = new StoreProductsViewModel(prod);
        }
    }
}
