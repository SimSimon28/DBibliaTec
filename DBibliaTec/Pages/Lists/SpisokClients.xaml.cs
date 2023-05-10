using DBibliaTec.DB;
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

namespace DBibliaTec.Pages.Lists
{
    public partial class SpisokClients : Page
    {
        public SpisokClients()
        {
            InitializeComponent();
        }

        private void UpdateClients()
        {
            var clients = App.Context.Clients.ToList();

            clients = clients.Where(p => p.Familiya.ToLower().Contains(TboxSerch.Text.ToLower())
            || p.Name.ToLower().Contains(TboxSerch.Text.ToLower())
            || p.Otchestvo.ToLower().Contains(TboxSerch.Text.ToLower())).ToList();


            DGFormulars.ItemsSource = clients;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddClients());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateClients();
        }

        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateClients();
        }

        private void MainClickDel_Click(object sender, RoutedEventArgs e)
        {
            var currentClients = (sender as MenuItem).DataContext as DB.Client;

            if (MessageBox.Show($"Вы уверены что хотите удалить клиента: {currentClients.Familiya} {currentClients.Name} {currentClients.Otchestvo} ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Clients.Remove(currentClients);
                App.Context.SaveChanges();
                UpdateClients();
            }
        }

        private void MainClickEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentClients = (sender as MenuItem).DataContext as DB.Client;
            NavigationService.Navigate(new Pages.Add.AddClients(currentClients));
        }
    }

}
