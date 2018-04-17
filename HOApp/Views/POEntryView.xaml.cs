using System.Windows.Controls;
using HOApp.ViewModel;
using System.Windows;
using HOApp.Model;

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

        //Rather than a button to insert, it would be better if the grid started with an empty line
        //and inside the productID column cells, there was a product dropdown.
        //The product dropdown will only show product descriptions but the
        //POLine table only contains the ProductID. (So the POLine in memory will have to store the productID)
        //So, maybe it's best to combine the id/descriptions columns to
        //a single column with a dropdown in it.

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            POEntryViewModel pvm = (POEntryViewModel)this.DataContext;
            POlineVM poLine = new POlineVM();

            //insert row into grid

            pvm.PO.POlines.Add(poLine);
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            POEntryViewModel pvm = (POEntryViewModel)this.DataContext;

            //SaveChanges is an entity framework function which actually commits what's in memory (PO/POLine)
            //to the DB
            //pvm.db.SaveChanges();
        }
    }
}
