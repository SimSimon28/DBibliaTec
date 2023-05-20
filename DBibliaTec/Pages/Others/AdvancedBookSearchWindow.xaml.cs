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

namespace DBibliaTec.Pages.Others
{
    public partial class AdvancedBookSearchWindow : Window
    {
        public AdvancedBookSearchWindow()
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

            DataContext = this;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboNal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        private void ComboSortBy3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpBook();
        }

        private void TboxSAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }

        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }

        private void TboxSGenre_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpBook();
        }
    }
}
