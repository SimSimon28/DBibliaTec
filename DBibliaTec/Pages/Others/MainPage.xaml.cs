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

namespace DBibliaTec.Pages.Others
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateMain();
        }

        public void UpdateMain()
        {
            var mains = App.Context.News.ToList();

            LView.ItemsSource = mains;
        }

        private void BSpisokFormulars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokFormularPage());
        }

        private void BSpisokClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokClients());
        }

        private void BSpisokBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokBook());
        }

        private void BClientsAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddClients());
        }

        private void BBooksAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddBooksPage());
        }

        private void BFormularsAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddFormularPage());
        }

        private void BInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Others.OBibliaInfo());
        }

        private void BFIzdatAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           NavigationService.Navigate(new Pages.Add.AddIzdatPage());
        }

        private void BSpisokIzdat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokIzdatPage());
        }

        private void BNewsAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddNewsPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentNews = (sender as Button).DataContext as DB.News;

            if (MessageBox.Show($"Вы уверены что хотите удалить новость?: ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.News.Remove(currentNews);
                App.Context.SaveChanges();
                UpdateMain();
            }
        }

        private void ChengeButton_Click(object sender, RoutedEventArgs e)
        {
            var currentNews = (sender as Button).DataContext as DB.News;
            NavigationService.Navigate(new Pages.Add.AddNewsPage(currentNews));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMain();
        }
    }
}
