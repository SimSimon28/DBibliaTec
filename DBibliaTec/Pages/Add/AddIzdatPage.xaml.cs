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

namespace DBibliaTec.Pages.Add
{
    public partial class AddIzdatPage : Page
    {
        private DB.Izdatel _currentIzdatel = null;
        public AddIzdatPage()
        {
            InitializeComponent();
            DataContext = this;

            CboxClassific.ItemsSource = App.Context.ClassificIzdats.ToList();
            CboxVidDeit.ItemsSource = App.Context.VidDeitIzdats.ToList();
            CboxVidIzdat.ItemsSource = App.Context.VidIzdats.ToList();
        }
        public AddIzdatPage(DB.Izdatel izdatel)
        {
            InitializeComponent();
            DataContext = this;

            _currentIzdatel = izdatel;

            Title = "Редактировать данные о клиенте";

            TboxName.Text = _currentIzdatel.Name_izdat.ToString();
            TboxAddres.Text = _currentIzdatel.Adress.ToString();
            TboxPhone.Text = _currentIzdatel.Phone.ToString();

            CboxClassific.ItemsSource = App.Context.ClassificIzdats.ToList();
            CboxVidDeit.ItemsSource = App.Context.VidDeitIzdats.ToList();
            CboxVidIzdat.ItemsSource = App.Context.VidIzdats.ToList();
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
                if (_currentIzdatel == null)
                {
                    var izdatel = new DB.Izdatel
                    {
                        Name_izdat = TboxName.Text,
                        Adress = TboxAddres.Text,
                        Phone = TboxPhone.Text,
                        ClassificIzdat = Convert.ToInt32(CboxClassific.SelectedValue),
                        VidDeitIzdat = Convert.ToInt32(CboxVidDeit.SelectedValue),
                        VidIzdatel = Convert.ToInt32(CboxVidIzdat.SelectedValue),
                    };
                    App.Context.Izdatels.Add(izdatel);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentIzdatel.Name_izdat = TboxName.Text;
                    _currentIzdatel.Adress = TboxAddres.Text;
                    _currentIzdatel.Phone = TboxPhone.Text;
                    _currentIzdatel.ClassificIzdat = Convert.ToInt32(CboxClassific.SelectedValue);
                    _currentIzdatel.VidIzdatel = Convert.ToInt32(CboxVidIzdat.SelectedValue);
                    _currentIzdatel.VidDeitIzdat = Convert.ToInt32(CboxVidDeit.SelectedValue);

                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }

        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TboxName.Text))
                errorBuilder.AppendLine("Наименование обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(CboxClassific.Text))
                errorBuilder.AppendLine("Классификация обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(CboxVidDeit.Text))
                errorBuilder.AppendLine("Вид деятельности издательства обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(CboxVidIzdat.Text))
                errorBuilder.AppendLine("Вид издательства обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(TboxPhone.Text))
                errorBuilder.AppendLine("Телефон обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(TboxAddres.Text))
                errorBuilder.AppendLine("Адрес обязателен для заполнения");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
