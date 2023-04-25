using System;
using DBibliaTec.DB;
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

namespace DBibliaTec.Pages.Add
{
    public partial class AddClients : Page
    {
        private DB.Client _currentClient = null;
        public AddClients()
        {
            InitializeComponent();
            DataContext = this;
        }

        public AddClients(DB.Client client)
        {
            InitializeComponent();
            DataContext = this;

            _currentClient = client;
            Title = "Редактировать данные о клиенте";
            TboxName.Text = _currentClient.Name.ToString();
            TboxFamiliya.Text = _currentClient.Familiya.ToString();
            TboxOtchestvo.Text = _currentClient.Otchestvo.ToString();
            TboxPhone.Text = _currentClient.Phone.ToString();
        }

        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TboxName.Text))
                errorBuilder.AppendLine("Фамилия обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(TboxFamiliya.Text))
                errorBuilder.AppendLine("Имя обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TboxOtchestvo.Text))
                errorBuilder.AppendLine("Отчество обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TboxPhone.Text))
                errorBuilder.AppendLine("Телефон обязателен для заполнения");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = ErrorCheck();

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_currentClient == null)
                {
                    var client = new DB.Client
                    {
                        Name = TboxName.Text,
                        Familiya = TboxFamiliya.Text,
                        Otchestvo = TboxOtchestvo.Text,
                        Phone = TboxPhone.Text,
                    };
                    App.Context.Clients.Add(client);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentClient.Familiya = TboxFamiliya.Text;
                    _currentClient.Name = TboxName.Text;
                    _currentClient.Otchestvo = TboxOtchestvo.Text;
                    _currentClient.Phone = TboxPhone.Text;

                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }
    }
}
