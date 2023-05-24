using DBibliaTec.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace DBibliaTec.Pages.Lists
{
    public partial class SpisokIzdatPage : Page
    {
        //Конструктор
        public SpisokIzdatPage()
        {
            InitializeComponent();

            // Для вкладки "Все" чтобы отображать все классификации
            List<ClassificIzdat> classific = new List<ClassificIzdat>
            {
                new ClassificIzdat { ID = 9999, Name = "Все" }
            };
            classific.AddRange(App.Context.ClassificIzdats.ToList());

            // Для вкладки "Все" чтобы отображать все виды
            List<VidIzdat> vid = new List<VidIzdat>
            {
                new VidIzdat { ID = 9999, Name = "Все" }
            };
            vid.AddRange(App.Context.VidIzdats.ToList());

            // Для вкладки "Все" чтобы отображать все виды деятельности
            List<VidDeitIzdat> vidDeit = new List<VidDeitIzdat>
            {
                new VidDeitIzdat { ID = 9999, Name = "Все" }
            };
            vidDeit.AddRange(App.Context.VidDeitIzdats.ToList());

            CBClassific.ItemsSource = classific;
            CBVidDeit.ItemsSource = vidDeit;
            CBVid.ItemsSource = vid;

            CBClassific.SelectedIndex = 0;
            CBVid.SelectedIndex = 0;
            CBVidDeit.SelectedIndex = 0;


            UpdateIzdat();
        }
        private void UpdateIzdat()
        {
            //для вывода из базы
            var izdat = App.Context.Izdatels.ToList();

            if (CBClassific.SelectedIndex != 0 && CBClassific.SelectedIndex != -1)
            {
                izdat = izdat.Where(p => p.ClassificIzdat1.ID == (int)CBClassific.SelectedValue).ToList();
            }

            if (CBVid.SelectedIndex != 0 && CBVid.SelectedIndex != -1)
            {
                izdat = izdat.Where(p => p.VidIzdat.ID == (int)CBVid.SelectedValue).ToList();
            }

            if (CBVidDeit.SelectedIndex != 0 && CBVidDeit.SelectedIndex != -1)
            {
                izdat = izdat.Where(p => p.VidDeitIzdat1.ID == (int)CBVidDeit.SelectedValue).ToList();
            }

            //Поиск по имени
            izdat = izdat.Where(p => p.Name_izdat.ToLower().Contains(TboxSerch.Text.ToLower())).ToList();



            //для вывода из базы
            DGFormulars.ItemsSource = izdat;
        }

        //Контекст меню, добавить
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Add.AddIzdatPage());
        }

        //Обновление страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateIzdat();
        }

        private void TboxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateIzdat();
        }

        private void MainClickDel_Click(object sender, RoutedEventArgs e)
        {
            var currentIzdat = (sender as MenuItem).DataContext as DB.Izdatel;

            if (MessageBox.Show($"Вы уверены что хотите удалить издателя: {currentIzdat.Name_izdat}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Izdatels.Remove(currentIzdat);
                App.Context.SaveChanges();
                UpdateIzdat();
            }
        }

        private void MainClickEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentIzdat = (sender as MenuItem).DataContext as DB.Izdatel;
            NavigationService.Navigate(new Pages.Add.AddIzdatPage(currentIzdat));
        }

        private void CBVid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateIzdat();
        }

        private void CBClassific_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateIzdat();
        }

        private void CBVidDeit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateIzdat();
        }
    }

}
