using DBibliaTec.DB;
using DBibliaTec.Pages.Lists;
using DBibliaTec.Pages.Others;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DBibliaTec.Pages.Add
{
    public partial class AddFormularPage : Page, INotifyPropertyChanged
    {
        private DB.Formular _currentFormular = null;


        private ObservableCollection<Book> selectedBooks;
        public ObservableCollection<Book> SelectedBooks { get => selectedBooks; set => Set(ref selectedBooks, value); }

        private Book selectedBookL;
        public Book SelectedBookL { get => selectedBookL; set => Set(ref selectedBookL, value); }

        private Book selectedBookR;
        public Book SelectedBookR { get => selectedBookR; set => Set(ref selectedBookR, value); }



        public AddFormularPage()
        {
            InitializeComponent();
            DataContext = this;

            LBoxView.ItemsSource = App.Context.Books.ToList();
            CboxReader.ItemsSource = App.Context.Clients.ToList();
            CboxId_personala.ItemsSource = App.Context.Personals.ToList();

            SelectedBooks = new ObservableCollection<Book>();
        }

        public AddFormularPage(DB.Formular formular)
        {
            InitializeComponent();
            DataContext = this;

            _currentFormular = formular;
            Title = "Редактировать данные о формуляре";

            CboxReader.Text = _currentFormular.ID_Clients.ToString();
            CboxId_personala.Text = _currentFormular.ID_Personals.ToString();
            TboxDateVid.Text = _currentFormular.Date_Vidachi.ToString();
            TboxDateSdachi.Text = _currentFormular.Date_Sdachi.ToString();

            CboxReader.ItemsSource = App.Context.Clients.ToList();
            CboxId_personala.ItemsSource = App.Context.Personals.ToList();

            CboxReader.SelectedValue = _currentFormular.ID_Clients;
            CboxId_personala.SelectedValue = _currentFormular.ID_Personals;

            LBoxView.ItemsSource = App.Context.Books.ToList();


            var books = _currentFormular.Books.ToList();

            SelectedBooks = new ObservableCollection<Book>(books); 
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
                if (_currentFormular == null)
                {
                    var formular = new DB.Formular
                    {
                        ID_Clients = Convert.ToInt32(CboxReader.SelectedValue),
                        ID_Personals = Convert.ToInt32(CboxId_personala.SelectedValue),
                        Date_Vidachi = DateTime.Parse(TboxDateVid.Text),
                        Date_Sdachi = DateTime.Parse(TboxDateSdachi.Text),
                        Books = SelectedBooks,
                    };

                    App.Context.Formulars.Add(formular);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentFormular.ID_Clients = Convert.ToInt32(CboxReader.SelectedValue);
                    _currentFormular.ID_Personals = Convert.ToInt32(CboxId_personala.SelectedValue);
                    _currentFormular.Date_Vidachi = DateTime.Parse(TboxDateVid.Text);
                    _currentFormular.Date_Sdachi = DateTime.Parse(TboxDateSdachi.Text);
                    _currentFormular.Books = SelectedBooks;
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }

        }

        private string ErrorCheck()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CboxReader.Text))
                errorBuilder.AppendLine("Читатель обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(CboxId_personala.Text))
                errorBuilder.AppendLine("Выбор персонала обязателен для заполнения");
            if (string.IsNullOrWhiteSpace(TboxDateSdachi.Text))
                errorBuilder.AppendLine("Дата сдачи обязательна для заполнения");
            if (string.IsNullOrWhiteSpace(TboxDateVid.Text))
               errorBuilder.AppendLine("Дата выдачи обязательна для заполнения");
            if (SelectedBooks.Count == 0)
                errorBuilder.AppendLine("Выберите минимум 1 книгу");
            if (errorBuilder.Length > 0)

                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedBooks.Count > 0)
                {
                    bool equal = false;
                    foreach (var item in SelectedBooks.ToList())
                    {                      
                        if (item.ID == SelectedBookL.ID)
                        {
                            equal = true;
                            break;
                        }
                    }
                    if (!equal)
                        SelectedBooks.Add(SelectedBookL);
                }
                else SelectedBooks.Add(SelectedBookL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedBooks.Remove(SelectedBookR);
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var DesForm = new Pages.Others.AdvancedBookSearchWindow(SelectedBooks);
            DesForm.ShowDialog();
        }

        private void ButtonSpisokBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokBook());
        }

        private void ButtonSpisokClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Lists.SpisokClients());
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
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
