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

namespace DBibliaTec.PagesClient
{
    /// <summary>
    /// Логика взаимодействия для MainPageClient.xaml
    /// </summary>
    public partial class MainPageClient : Page
    {
        public MainPageClient()
        {
            InitializeComponent();
            UpdateMain();
        }
        public void UpdateMain()
        {
            var mains = App.Context.News.ToList();

            LView.ItemsSource = mains;
        }

        private void BInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PagesClient.OBibliaInfoPage());
        }

        private void BSpisokBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PagesClient.BookClientPage());
        }


        private void AutorizationButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Others.Avtorization());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMain();
        }
    }
}
