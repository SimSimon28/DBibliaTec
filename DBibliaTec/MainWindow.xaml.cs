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
using System.Windows.Threading;

namespace DBibliaTec
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan sessionTime = new TimeSpan(0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();
            Timer();
            MainFrame.Navigate(new Pages.Others.Avtorization());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void Timer()
        {
            var timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GlTimer.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
            sessionTime += new TimeSpan(0, 0, 1);
            SessionTimer.Text = $"Текущая сессия {sessionTime}";
        }
    }
}
