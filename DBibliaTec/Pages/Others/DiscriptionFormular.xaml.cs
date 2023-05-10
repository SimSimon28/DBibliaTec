using DBibliaTec.DB;
using DBibliaTec.Pages.Lists;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DBibliaTec.Pages.Others
{
    public partial class DiscriptionFormular : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Book> selectedBooks;
        public ObservableCollection<Book> SelectedBooks { get => selectedBooks; set => Set(ref selectedBooks, value); }

        private Book selectedBook;
        public Book SelectedBook { get => selectedBook; set => Set(ref selectedBook, value); }

        private Formular currentFormular;
        public Formular CurrentFormular { get => currentFormular; set => Set(ref currentFormular, value); }

        private bool isReadOnly;
        public bool IsReadOnly { get => isReadOnly; set => Set(ref isReadOnly, value); }

        public DiscriptionFormular(Formular formular)
        {
            InitializeComponent();
            DataContext = this;

            isReadOnly = true;

            currentFormular = formular;
            Title = "Редактировать данные о формуляре";

            LBoxView.ItemsSource = App.Context.Books.ToList();


            var books = currentFormular.Books.ToList();
            SelectedBooks = new ObservableCollection<Book>(books);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DelteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены что хотите удалить формуляр: номер {currentFormular.ID} ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Formulars.Remove(CurrentFormular);
                App.Context.SaveChanges();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            IsReadOnly = false;
            CancelButton.Visibility = Visibility.Visible;
            DelteButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            SpisokBooks.Visibility = Visibility.Visible;
            AddBooks.Visibility = Visibility.Visible;
            DeleteBook.Visibility = Visibility.Visible;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsReadOnly = true;
            CancelButton.Visibility = Visibility.Hidden;
            DelteButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;
            SpisokBooks.Visibility = Visibility.Hidden;
            AddBooks.Visibility = Visibility.Hidden;
            DeleteBook.Visibility = Visibility.Hidden;
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



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = ErrorCheck();

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (currentFormular != null)
                {
                    currentFormular.Client.Familiya = TBlockFamiliya.Text;
                    currentFormular.Client.Name = TBlockName.Text;
                    currentFormular.Client.Otchestvo = TBlockOtchestvo.Text;
                    currentFormular.Client.Phone = TBlockPhone.Text;
                    currentFormular.Client.Age = int.Parse(TBlockAge.Text);
                    currentFormular.Client.Date_rojd = DateTime.Parse(TBlockDateRojd.Text);
                    currentFormular.Books = SelectedBooks;
                    App.Context.SaveChanges();
                }
                IsReadOnly = true;
                CancelButton.Visibility = Visibility.Hidden;
                DelteButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Hidden;
                SpisokBooks.Visibility = Visibility.Hidden;
                AddBooks.Visibility = Visibility.Hidden;
                DeleteBook.Visibility = Visibility.Hidden;
                MessageBox.Show("Успешно изменено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }



        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBlockFamiliya.Text))
                errorBuilder.AppendLine("Фамилия обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(TBlockName.Text))
                errorBuilder.AppendLine("Имя для обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TBlockOtchestvo.Text))
                errorBuilder.AppendLine("Отчество обязательно для заполнения");
            if (string.IsNullOrWhiteSpace(TBlockAge.Text))
                errorBuilder.AppendLine("Возраст обязателен для заполнения");
            if (SelectedBooks.Count == 0)
                errorBuilder.AppendLine("Выберите минимум 1 книгу");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void AddBooks_Click(object sender, RoutedEventArgs e)
        {
            SelectedBooks.Add(SelectedBook);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            SelectedBooks.Remove(SelectedBook);
        }
    }
}
