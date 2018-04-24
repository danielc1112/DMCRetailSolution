using System.Windows.Controls;
using HOApp.ViewModel;
using System.Windows;
using HOApp.Model;
using DataAccess;

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

        private POEntryViewModel ViewModel => (POEntryViewModel)DataContext;

        //Rather than a button to insert, it would be better if the grid started with an empty line
        //and inside the productID column cells, there was a product dropdown.
        //The product dropdown will only show product descriptions but the
        //POLine table only contains the ProductID. (So the POLine in memory will have to store the productID)
        //So, maybe it's best to combine the id/descriptions columns to
        //a single column with a dropdown in it.

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Clicked");
            ViewModel.PO.AddProductOrderLine(ViewModel.SelectedProduct);
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new RetailDbContext())
            {
                db.Pos.Add(ViewModel.PO.TheEntity);
                db.SaveChanges();
            }

            DataContext = new POEntryViewModel();
        }
    }
}
