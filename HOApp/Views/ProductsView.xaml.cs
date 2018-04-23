using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
            this.DataContext = new ProductsViewModel();
        }

        private void QtyOnHand_Click(object sender, RoutedEventArgs e)
        {
            ProductsViewModel pvm = (ProductsViewModel)this.DataContext;
            if (pvm.GotSomethingSelected())
            {
                StoreProductsView spv = new StoreProductsView(pvm.SelectedProduct.TheEntity);
                spv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                spv.Show();
            }
        }

        private void QtyOnHandHistory_Click(object sender, RoutedEventArgs e)
        {
            ProductsViewModel pvm = (ProductsViewModel)this.DataContext;
            if (pvm.GotSomethingSelected())
            {
                StoreProductsViewModel spv = new StoreProductsViewModel(pvm.SelectedProduct.TheEntity);
                //spv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //spv.Show();
            }
        }
    }
}
