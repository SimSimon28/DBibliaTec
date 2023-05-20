using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace DBibliaTec.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для AddBooksPage.xaml
    /// </summary>
    public partial class AddBooksPage : Page
    {
        private DB.Book _currentBook = null;
        private byte[] _imageBooks = null;
        public AddBooksPage()
        {
            InitializeComponent();
            DataContext = this;
            CboxCategory.ItemsSource = App.Context.Categories.ToList();
            CboxGenre.ItemsSource = App.Context.Genres.ToList();
        }

        public AddBooksPage(DB.Book book)
        {
            InitializeComponent();
            DataContext = this;

            _currentBook = book;
            Title = "Редактировать данные о книге";
            TboxNameBook.Text = _currentBook.Name.ToString();
            CboxCategory.Text = _currentBook.Category.ToString();
            CboxGenre.Text = _currentBook.Genre.ToString();
            TboxNAuthor.Text = _currentBook.NAuthor.ToString();
            TboxOAuthor.Text = _currentBook.OAuthor.ToString();
            TboxFAthor.Text = _currentBook.FAuthor.ToString();
            TboxCount.Text = _currentBook.Count.ToString();
            DPDateVihoda.Text = _currentBook.ToString();

            CboxCategory.ItemsSource = App.Context.Books.ToList();
            CboxGenre.ItemsSource = App.Context.Books.ToList();

            CboxCategory.ItemsSource = App.Context.Categories.ToList();
            CboxGenre.ItemsSource = App.Context.Genres.ToList();

            if (_currentBook.ImageBook != null)
                BooksImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_currentBook.ImageBook);

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
                if (_currentBook == null)
                {
                    var book = new DB.Book
                    {
                        Category = Convert.ToInt32(CboxCategory.SelectedValue),
                        Genre = Convert.ToInt32(CboxGenre.SelectedValue),
                        Name = TboxNameBook.Text,
                        FAuthor = TboxFAthor.Text,
                        OAuthor = TboxOAuthor.Text,
                        NAuthor = TboxNAuthor.Text,
                        Count = int.Parse(TboxCount.Text),
                        Date_Vipusk = DateTime.Parse(DPDateVihoda.Text),
                        ImageBook = _imageBooks
                    };
                    App.Context.Books.Add(book);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentBook.Category = Convert.ToInt32(CboxCategory.SelectedValue);
                    _currentBook.Genre = Convert.ToInt32(CboxGenre.SelectedValue);
                    _currentBook.Name= TboxNameBook.Text;
                    _currentBook.FAuthor = TboxFAthor.Text;
                    _currentBook.NAuthor = TboxNAuthor.Text;
                    _currentBook.OAuthor = TboxOAuthor.Text;
                    _currentBook.Count = int.Parse(TboxCount.Text);
                    _currentBook.Date_Vipusk = DateTime.Parse(DPDateVihoda.Text);

                    if (_imageBooks != null)
                        _currentBook.ImageBook = _imageBooks;

                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
    }
        private void BtnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _imageBooks = File.ReadAllBytes(ofd.FileName);
                BooksImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_imageBooks);
            }
        }

        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TboxNameBook.Text))
                errorBuilder.AppendLine("Название книги обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(CboxCategory.Text))
                errorBuilder.AppendLine("Категория обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(CboxGenre.Text))
                errorBuilder.AppendLine("Выбор жанра обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(TboxFAthor.Text))
                errorBuilder.AppendLine("Фамилия автора обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(TboxNAuthor.Text))
                errorBuilder.AppendLine("Имя автора обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TboxCount.Text))
                errorBuilder.AppendLine("Отчество автора обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(DPDateVihoda.Text))
                errorBuilder.AppendLine("Дата выпуска книги обязательна для заполнения");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
