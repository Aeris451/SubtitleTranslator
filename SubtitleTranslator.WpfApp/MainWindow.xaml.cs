using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using SubtitleTranslator.Model;
using SubtitleTranslator.DAL;
using DeepL;


namespace SubtitleTranslator.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ApplicationDbContext _dbContext;
        Translation tr1 = new Translation();
        string usageStr;
        public MainWindow()
        {
            InitializeComponent();




            var connString = Application.Current.Resources["connString"] as string; // pobranie cs z resource
                                                                                    // stworzenie obiektu kontekstu
            _dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connString!)// okreslenie providera bazy danych
            .Options);




            DataGridTranslations.Columns.Add(new DataGridTextColumn() { Header = "Number", Binding = new Binding("lineNumber") });
            DataGridTranslations.Columns.Add(new DataGridTextColumn() { Header = "Time stamp", Binding = new Binding("timeStamp") });
            DataGridTranslations.Columns.Add(new DataGridTextColumn() { Header = "Source", Binding = new Binding("text") });
            DataGridTranslations.Columns.Add(new DataGridTextColumn() { Header = "Destination", Binding = new Binding("translatedText") });
            DataGridTranslations.AutoGenerateColumns = false;



        }


        private void sourceLoad(Translation tr1)
        {
            srLangLabel.Content = "Source language: " + tr1.sourceLanguage;
            dstLangLabel.Content = "Destination language: "+ tr1.destinationLanguage;
            nameLabel.Content = "Translation name: " + tr1.Name;
            if (tr1.TranslationId != null)
            {
                idLabel.Content = "Translation id: " + tr1.TranslationId;
            }

            licenceLabel.Content = tr1.Licence;
            DataGridTranslations.ItemsSource = tr1.srtLines;
            DataGridTranslations.Items.Refresh();
        }


        private void trCreateBt_Click(object sender, RoutedEventArgs e)
        {
            
            AddTranslationWindow addTranslation = new AddTranslationWindow();
            addTranslation.ShowDialog();
            //_dbContext.Translations.Add(addTranslation.getTranslation());

            var licence = tr1.Licence;

            tr1 = addTranslation.getTranslation();
            
            tr1.Licence = licence;

            sourceLoad(tr1);

            
            
        }

        private void translateButton_Click(object sender, RoutedEventArgs e)
        {
            if(tr1.Licence == null)
            {
                MessageBox.Show("Enter licence first");
                return;
            }

            if (tr1.srtLines == null)
            {
                MessageBox.Show("Add translation first");
                return;
            }

            
            TranslatorApi.Translator(tr1.Licence, tr1.destinationLanguage, tr1.sourceLanguage, tr1.srtLines);
            MessageBox.Show("Translation started");
            


        }

        private void dbSaveBt_Click(object sender, RoutedEventArgs e)
        {
            if(tr1.TranslationId == null)
            {
                MessageBox.Show("Create new save first");
            }

            Translation trEdit = _dbContext.Translations.FirstOrDefault(a => a.TranslationId == tr1.TranslationId); //Wyciągamy pierwszy napotkany album o tytule "Kill'em All"
            if (trEdit != null) //Sprawdzamy, czy album istnieje
            {
                trEdit.srtLines = tr1.srtLines;
                trEdit.Licence = tr1.Licence;
                _dbContext.Entry(trEdit).State = EntityState.Modified; 
                _dbContext.SaveChanges(); //Zapisujemy zmiany.
            }

            

        }

        private void dbLoadBt_Click(object sender, RoutedEventArgs e)
        {
            DbLoadWindow dbLoad = new DbLoadWindow();
            dbLoad.ShowDialog();
            
            if(dbLoad.getDb() != null)
            {
                tr1 = dbLoad.getDb();
                sourceLoad(tr1);
            }
            

        }

        private void licenceButton_Click(object sender, RoutedEventArgs e)
        {
            AddLicenceWindow addLicence = new AddLicenceWindow(); 

            addLicence.ShowDialog();

            tr1.Licence = addLicence.getLicence();
            licenceLabel.Content = tr1.Licence;
            
        }

        private void saveSrtBt_Click(object sender, RoutedEventArgs e)
        {
            string path;
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Subtitles"; // Default file name
            dialog.DefaultExt = ".srt"; // Default file extension
            dialog.Filter = "SRT subtitles (.srt)|*.srt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                path = dialog.FileName;
                Serializer.Save(tr1, path);
            }
        }

        private void usageButton_Click(object sender, RoutedEventArgs e)
        {
            if(tr1.Licence != null)
            {
                TranslatorApi.GetUsage(tr1.Licence);
            }
            else
            {
                MessageBox.Show("Enter licence first");
            }
            
        }

        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(tr1.Name != null)
            {

                tr1.TranslationId = null;
                foreach (var line in tr1.srtLines)
                {
                    line.Id = null;
                }
                _dbContext.Translations.Add(tr1);
                _dbContext.SaveChanges();
                MessageBox.Show("Saved as: " + tr1.Name);
            }
            else 
            {
                MessageBox.Show("You cannot create an empty record");  
            }

        }

        private void saveAssBtn_Click(object sender, RoutedEventArgs e)
        {
            string path;
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Subtitles"; // Default file name
            dialog.DefaultExt = ".ass"; // Default file extension
            dialog.Filter = "ASS subtitles (.ass)|*.ass"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                path = dialog.FileName;
                Serializer.SaveAss(tr1, path);
            }
        }
    }
}




