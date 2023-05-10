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
    public partial class Avtorization : Page
    {
        public Avtorization()
        {
            InitializeComponent();
        }

        private void Avtoriz_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.Context.Personals
            .FirstOrDefault(p => p.Login == Login.Text && p.Password == Password.Password);

            if (currentUser != null)
            {
                App.CurrentUser = currentUser;
                NavigationService.Navigate(new Pages.Others.MainPage());
            }
            else
            {
                MessageBox.Show("Пользователь с данными не найден или данные были введены неверно, пожалуйста, проверьте свой логин и пароль.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Avtoria_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Others.MainPage());
        }
    }
}
