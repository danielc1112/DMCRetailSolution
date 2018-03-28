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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using HOApp.Messages;
using System.Windows.Media.Animation;
using HOApp.ViewModel;

namespace HOApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Messenger.Default.Register<NavigateMessage>(this, (action) => ShowUserControl(action));
            Messenger.Default.Register<UserMessage>(this, (action) => ReceiveUserMessage(action));
            this.DataContext = new MainViewModel();
        }
        private Visibility menuVisible = Visibility.Visible;
        public Visibility MenuVisible
        {
            get { return menuVisible; }
            set
            {
                menuVisible = value;
            }
        }
        private void ReceiveInEditMessage(InEdit inEdit)
        {
            //
        }
        private void ReceiveUserMessage(UserMessage msg)
        {
            UIMessage.Opacity = 1;
            UIMessage.Text = msg.Message;
            Storyboard sb = (Storyboard)this.FindResource("FadeUIMessage");
            sb.Begin();
        }
        private void ShowUserControl(NavigateMessage nm)
        {
            MenuVisible = Visibility.Collapsed;
            Holder.Content = nm.View;
        }
        private void btnSales_Click(object sender, RoutedEventArgs e)
        {
            SalesView view = new SalesView();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsView view = new ProductsView();

        }

    }
}
