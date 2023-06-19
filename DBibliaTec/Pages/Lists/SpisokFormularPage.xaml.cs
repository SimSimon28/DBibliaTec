using DBibliaTec.DB;
using DBibliaTec.Pages.Others;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Schema;

namespace DBibliaTec.Pages.Lists
{
    public partial class SpisokFormularPage : Page
    {
        public SpisokFormularPage()
        {
            InitializeComponent();
        }

        private void UpdateFormular()
        {
            var formular = App.Context.Formulars.ToList();

            formular = formular.Where(p => p.Client.Familiya.ToLower().Contains(TboxSerch.Text.ToLower()) 
            || p.Client.Name.ToLower().Contains(TboxSerch.Text.ToLower()) 
            || p.Client.Otchestvo.ToLower().Contains(TboxSerch.Text.ToLower())).ToList();

            DGFormulars.ItemsSource = formular;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddFormularPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var listFormProsr = App.Context.Formulars.Where(p => p.Date_Sdachi < DateTime.Now).ToList();

            foreach (var formular in listFormProsr)
            {
                formular.Status = 2;
            }
            App.Context.SaveChanges();

            UpdateFormular();
        }

        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFormular();
        }

        private void MainClickDel_Click(object sender, RoutedEventArgs e)
        {
            var currentFormular = (sender as MenuItem).DataContext as DB.Formular;

            if (MessageBox.Show($"Вы уверены что хотите удалить формуляр: номер {currentFormular.ID} ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                foreach (var book in currentFormular.Books)
                {
                    book.Count += 1;
                }    
                App.Context.Formulars.Remove(currentFormular);
                App.Context.SaveChanges();
                UpdateFormular();
            }
        }

        private void MainClickEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentFormular = (sender as MenuItem).DataContext as DB.Formular;
            NavigationService.Navigate(new Pages.Add.AddFormularPage(currentFormular));
        }

        private void MainClickDescription_Click(object sender, RoutedEventArgs e)
        {
            var currentFormular = (sender as MenuItem).DataContext as DB.Formular;
            var DesForm = new DiscriptionFormular(currentFormular);
            DesForm.ShowDialog();
        }
    }
}
