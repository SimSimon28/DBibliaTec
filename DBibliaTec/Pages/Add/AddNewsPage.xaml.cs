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

namespace DBibliaTec.Pages.Add
{
    public partial class AddNewsPage : Page
    {
        private DB.News _currentNews = null;
        public AddNewsPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        public AddNewsPage(DB.News news)
        {
            InitializeComponent();

            _currentNews = news;
            Title = "Редактировать данные о клиенте";

            TboxTitle.Text = _currentNews.Title.ToString();
            TboxTheme.Text = _currentNews.Theme.ToString();
            TboxDescription.Text = _currentNews.Description.ToString();
            TboxDateAdd.Text = _currentNews.DateAdd.ToString();
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
                if (_currentNews == null)
                {
                    var news = new DB.News
                    {
                        Title = TboxTitle.Text,
                        Theme = TboxTheme.Text,
                        Description = TboxDescription.Text,
                        DateAdd = DateTime.Parse(TboxDateAdd.Text),
                    };
                    App.Context.News.Add(news);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentNews.Title = TboxTitle.Text;
                    _currentNews.Theme = TboxTheme.Text;
                    _currentNews.Description = TboxDescription.Text;
                    _currentNews.DateAdd = DateTime.Parse(TboxDateAdd.Text);

                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }

        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TboxTitle.Text))
                errorBuilder.AppendLine("Название обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TboxTheme.Text))
                errorBuilder.AppendLine("Тема обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(TboxDescription.Text))
                errorBuilder.AppendLine("Описание обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TboxDateAdd.Text))
                errorBuilder.AppendLine("Дата обязателена для заполнения");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
