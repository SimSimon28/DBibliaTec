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
            CboxIzdat.ItemsSource = App.Context.Izdatels.ToList();
        }

        public AddBooksPage(DB.Book book)
        {
            InitializeComponent();
            DataContext = this;

            CboxCategory.ItemsSource = App.Context.Categories.ToList();
            CboxGenre.ItemsSource = App.Context.Genres.ToList();
            CboxIzdat.ItemsSource = App.Context.Izdatels.ToList();

            _currentBook = book;
            Title = "Редактировать данные о книге";
            TboxNameBook.Text = _currentBook.Name.ToString();
            CboxCategory.SelectedValue = _currentBook.Category;
            CboxGenre.SelectedValue = _currentBook.Genre;
            TboxNAuthor.Text = _currentBook.NAuthor.ToString();
            TboxOAuthor.Text = _currentBook.OAuthor.ToString();
            TboxInventNum.Text = _currentBook.InventNumber.ToString();
            TboxFAthor.Text = _currentBook.FAuthor.ToString();
            TboxCount.Text = _currentBook.Count.ToString();
            DPDateVihoda.Text = _currentBook.ToString();
            CboxIzdat.SelectedValue = _currentBook.Izdatel;



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
                    try
                    {
                    var book = new DB.Book
                    {
                        Category = Convert.ToInt32(CboxCategory.SelectedValue),
                        Genre = Convert.ToInt32(CboxGenre.SelectedValue),
                        Name = TboxNameBook.Text,
                        InventNumber = Convert.ToInt32(TboxInventNum.Text),
                        FAuthor = TboxFAthor.Text,
                        OAuthor = TboxOAuthor.Text,
                        NAuthor = TboxNAuthor.Text,
                        Izdatel = Convert.ToInt32(CboxIzdat.SelectedValue),
                        Count = int.Parse(TboxCount.Text),
                        Date_Vipusk = DateTime.Parse(DPDateVihoda.Text),
                        ImageBook = _imageBooks
                    };
                    App.Context.Books.Add(book);
                    App.Context.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Инвентарный номер");
                    }
                }
                else
                {
                    try
                    {

                    _currentBook.Category = Convert.ToInt32(CboxCategory.SelectedValue);
                    _currentBook.Genre = Convert.ToInt32(CboxGenre.SelectedValue);
                    _currentBook.Name= TboxNameBook.Text;
                    _currentBook.InventNumber = Convert.ToInt32(TboxInventNum.Text);
                    _currentBook.FAuthor = TboxFAthor.Text;
                    _currentBook.NAuthor = TboxNAuthor.Text;
                    _currentBook.OAuthor = TboxOAuthor.Text;
                    _currentBook.Count = int.Parse(TboxCount.Text);
                    _currentBook.Izdatel = Convert.ToInt32(CboxIzdat.SelectedValue);
                    _currentBook.Date_Vipusk = DateTime.Parse(DPDateVihoda.Text);

                    if (_imageBooks != null)
                        _currentBook.ImageBook = _imageBooks;

                    App.Context.SaveChanges();
                    }

                    catch (Exception ex1)
                    {
                        MessageBox.Show(ex1.Message, "Перепроверьте данные");
                    }
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

            if (string.IsNullOrWhiteSpace(TboxInventNum.Text) || !int.TryParse(TboxInventNum.Text, out int i))
                errorBuilder.AppendLine("Инвентарный номер обязателен для заполнения");

            if (string.IsNullOrWhiteSpace(TboxNameBook.Text))
                errorBuilder.AppendLine("Название книги обязательно для заполнения");

            if (string.IsNullOrWhiteSpace(CboxCategory.Text))
                errorBuilder.AppendLine("Категория обязательна для заполнения");

            if (string.IsNullOrWhiteSpace(CboxGenre.Text))
                errorBuilder.AppendLine("Выбор жанра обязателен для заполнения");

            if (string.IsNullOrWhiteSpace(CboxIzdat.Text))
                errorBuilder.AppendLine("Выбор издателя обязателен для заполнения");

            if (string.IsNullOrWhiteSpace(TboxFAthor.Text))
                errorBuilder.AppendLine("Фамилия автора обязательна для заполнения");

            if (string.IsNullOrWhiteSpace(TboxNAuthor.Text))
                errorBuilder.AppendLine("Имя автора обязательно для заполнения");

            if (string.IsNullOrWhiteSpace(TboxCount.Text) || !int.TryParse(TboxCount.Text, out int ui))
                errorBuilder.AppendLine("Отчество автора обязательно для заполнения");

            if (string.IsNullOrWhiteSpace(DPDateVihoda.Text))
                errorBuilder.AppendLine("Дата выпуска книги обязательна для заполнения");

            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        
    }
}
