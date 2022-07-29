using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace SubtitleTranslator.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.Resources["connString"] = GetConnectionString();
        }
        private string GetConnectionString()
        {
            // pobranie connection stringa
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var dataDir = $@"{di.Parent?.Parent?.Parent?.FullName}\Database";
            var connString = ConfigurationManager.ConnectionStrings["FileDbConnectionStr"].ConnectionString;
            return connString.Replace($"|DataDirectory|", dataDir);
        }
    }

}
