using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SubtitleTranslator.DAL;
using Microsoft.EntityFrameworkCore;
using SubtitleTranslator.Model;



namespace SubtitleTranslator.WpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy DbLoadWindow.xaml
    /// </summary>
    public partial class DbLoadWindow : Window
    {

        private readonly ApplicationDbContext _db;
        public Translation tr2 { get; set; }

        public Translation getDb()
        {
                return tr2;
        }

        public DbLoadWindow()
        {
            var connString = Application.Current.Resources["connString"] as string; 

            _db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connString!)
            .Options);

            

            InitializeComponent();


            SetGrid(trDataGrid, _db.Translations.ToList());
            trDataGrid.AutoGenerateColumns = false;

            

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (trDataGrid.SelectedItem != null)
            {
                var tr = trDataGrid.SelectedItem as Translation;

                if(tr != null)
                {
                    Translation trDelete = _db.Translations.FirstOrDefault(a => a.TranslationId == tr.TranslationId);

                    if (trDelete != null)
                    {
                        _db.Translations.Remove(trDelete);



                        _db.SaveChanges();
                        SetGrid(trDataGrid, _db.Translations.ToList());

                    }
                }

            }

        }


        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(trDataGrid.SelectedItem != null)
            {
                var tr = trDataGrid.SelectedItem as Translation;

                

                foreach (var item in _db.Translations.ToList())
                {
                    if(tr.TranslationId == item.TranslationId)
                    {
                        tr2 = item;
                    }
                    
                }
                foreach (var line in _db.SrtLines.ToList()) { }

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Select translation first");
               
            }
            


            
            
        }

        private static void SetGrid<T>(DataGrid dataGrid, IList<T> list) where T : new()
        {
            dataGrid.Columns.Clear();
            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = prop.Name.ToUpper(), Binding = new Binding(prop.Name) });
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = list;
            dataGrid.Items.Refresh();
        }
    }
}
