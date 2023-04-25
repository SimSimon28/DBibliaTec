using DBibliaTec.DB;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DBibliaTec.Pages.Others
{
    public partial class DiscriptionFormular : Window, INotifyPropertyChanged
    {
        private Formular currentFormular;
        public Formular CurrentFormular { get => currentFormular; set => Set(ref currentFormular, value); }

        private bool isReadOnly;
        public bool IsReadOnly { get => isReadOnly; set => Set(ref isReadOnly, value); }

        public DiscriptionFormular(Formular formular)
        {
            InitializeComponent();
            DataContext = this;

            isReadOnly = true;

            CurrentFormular = formular;
            Title = "Редактировать данные о формуляре";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены что хотите удалить формуляр: номер {CurrentFormular.ID} ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Formulars.Remove(CurrentFormular);
                App.Context.SaveChanges();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            IsReadOnly = false;
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
