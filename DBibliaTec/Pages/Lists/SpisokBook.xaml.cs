using DBibliaTec.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DBibliaTec.Pages.Lists
{
    public partial class SpisokBook : Page
    {
        public SpisokBook()
        {
            InitializeComponent();


            List<Category> categ = new List<Category>
            {
                new Category { ID = 9999, Name = "Все" }
            };
            categ.AddRange(App.Context.Categories.ToList());

            ComboSortBy3.ItemsSource = categ;

            ComboSortBy.SelectedIndex = 0;
            ComboSortBy2.SelectedIndex = 0;
        }

        // Строка для прогрузки страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpBook();
        }

        private void UpBook()
        {
            var book = App.Context.Books.ToList();


            // Сортировка по названию книг

            if (ComboSortBy.SelectedIndex == 1)
                book = book.OrderBy(p => p.Name).ToList();
            if (ComboSortBy.SelectedIndex == 2)
                book = book.OrderByDescending(p => p.Name).ToList();

            if (ComboSortBy3.SelectedIndex != 0)
            {
                book = book.Where(p => p.Category1.ID == (int)ComboSortBy3.SelectedValue).ToList();
            }

            //Сортировка по жанру

            if (ComboSortBy2.SelectedIndex == 1)
                book = book.OrderBy(p => p.Genre).ToList();
            if (ComboSortBy2.SelectedIndex == 2)
                book = book.OrderByDescending(p => p.Genre).ToList();

            book = book.Where(p => p.Name.ToLower().Contains(TboxSerch.Text.ToLower())).ToList();

            LView.ItemsSource = book;
        }

        // Кнопка редактирования
        private void ChengeButton_Click(object sender, RoutedEventArgs e)
        {
            var currentBook = (sender as Button).DataContext as DB.Book;
            NavigationService.Navigate(new Pages.Add.AddBooksPage(currentBook));
        }

        // Кнопка удаления
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentBook = (sender as Button).DataContext as DB.Book;

            if (MessageBox.Show($"Вы уверены что хотите удалить книгу?: ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Books.Remove(currentBook);
                App.Context.SaveChanges();
                UpBook();
            }
        }

        // Кнопка добавление контрактов
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var currentBook = (sender as Button).DataContext as DB.Book;
            NavigationService.Navigate(new Pages.Add.AddBooksPage());
        }

        // Комбобокс сортировки цены
        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        // Комбобокс сортировки групп
        private void ComboSortBy2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        private void ComboSortBy3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        // Поиск 
        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }



        
    }
}
