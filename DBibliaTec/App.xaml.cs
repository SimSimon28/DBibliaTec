using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DBibliaTec
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DB.BIbliaEntities Context
        { get; } = new DB.BIbliaEntities();

        public static DB.Personal CurrentUser = null;
    }
}
