using DBibliaTec.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DBibliaTec.Pages.Lists
{
    public partial class SpisokBook : Page, INotifyPropertyChanged
    {

        public SpisokBook()
        {
            InitializeComponent();

            // Для вкладки "Все" чтобы отображать все категории
            List<Category> categ = new List<Category>
            {
                new Category { ID = 9999, Name = "Все" }
            };
            categ.AddRange(App.Context.Categories.ToList());

            ComboSortBy3.ItemsSource = categ;
            ComboNal.SelectedIndex = 0;
        }

        // Строка для прогрузки страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpBook();
        }

        private void UpBook()
        {
            var book = App.Context.Books.ToList();

            //Соритировка по категории

            if (ComboSortBy3.SelectedIndex != 0)
            {
                book = book.Where(p => p.Category1.ID == (int)ComboSortBy3.SelectedValue).ToList();
            }

            if (ComboNal.SelectedIndex == 1)
                book = book.Where(p => p.Count > 0).ToList();
            if (ComboNal.SelectedIndex == 2)
                book = book.Where(p => p.Count <= 0).ToList();

            //Поиск по жанру

            book = book.Where(p => p.Genre1.Name.ToLower().Contains(TboxSGenre.Text.ToLower())).ToList();

            //Поиск по ФИО автора

            book = book.Where(p => p.NAuthor.ToLower().Contains(TboxSAuthor.Text.ToLower()) || p.FAuthor.ToLower().Contains(TboxSAuthor.Text.ToLower()) || p.OAuthor.ToLower().Contains(TboxSAuthor.Text)).ToList();

            //Поиск по названию книги

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

        // Чек наличия книг
        private void CheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            UpBook();
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

        private void ComboSortBy3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        // Поиск 
        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }

        private void TboxSAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }

        private void TboxSGenre_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }

        private void ComboNal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }


        #endregion


    }
}
