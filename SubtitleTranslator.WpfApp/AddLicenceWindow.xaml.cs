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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace SubtitleTranslator.WpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddLicenceWindow.xaml
    /// </summary>
    public partial class AddLicenceWindow : Window
    {
        public AddLicenceWindow()
        {
            InitializeComponent();
        }

        public string getLicence()
        {
            return licenceTb.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(licenceTb.Text, @"^[A-Za-z0-9\-:]{39}$"))
            {
                MessageBox.Show("Invalid licence");
                licenceTb.Text = "";
                return;
            }

            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DbLoadWindow dbLoadWindow = new DbLoadWindow();

            dbLoadWindow.ShowDialog();

            
            var tr1 = dbLoadWindow.getDb();
            if(tr1.Licence != null)
            {
                licenceTb.Text = tr1.Licence;
            } else
            {
                MessageBox.Show("This save doesn't have any licence");
            }

            
        }
    }
}
