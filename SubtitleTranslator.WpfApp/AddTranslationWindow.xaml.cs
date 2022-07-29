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
using SubtitleTranslator.Model;

namespace SubtitleTranslator.WpfApp
{

    public partial class AddTranslationWindow : Window
    {

        string path;
        public Translation getTranslation()
        {
            return Tr;
        }




        public Translation Tr = new Translation();

        bool isAss = false;

        public AddTranslationWindow()
        {
            InitializeComponent();

            

            srLangCb.ItemsSource = Languages.GetSrc();
            dstLangCb.ItemsSource = Languages.GetDst();

        }

        private void loadSubButton_Click(object sender, RoutedEventArgs e)
        {
            isAss = false;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Subtitles"; // Default file name
            dialog.DefaultExt = ".srt"; // Default file extension
            dialog.Filter = "SRT subtitles (.srt)|*.srt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                srtPath.Text = dialog.FileName;
            }
        }



        private void translateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(trNameTextBox.Text, @"^[a-zA-Z0-9]{2,20}$"))
            {
                MessageBox.Show("Invalid translation name");
                return;
            }

            if(srLangCb.SelectedIndex == -1)
            {
                MessageBox.Show("Empty source language field");
                return;
            }

            if (dstLangCb.SelectedIndex == -1)
            {
                MessageBox.Show("Empty destination language field");
                return;
            }

            if(srtPath.Text == string.Empty)
            {
                MessageBox.Show("No file selected");
                return;
            }

            path = srtPath.Text;
            List<SrtLine> lines = new List<SrtLine>();

            if(isAss != true)
            {
                
                Serializer.Load(lines, path);
            }
            else
            {
                Serializer.LoadAss(lines, path);
            }
            

            Tr.srtLines = lines;
            Tr.Name = trNameTextBox.Text;
            Tr.sourceLanguage = srLangCb.Text.Substring(0, srLangCb.Text.IndexOf(":"));
            Tr.destinationLanguage = dstLangCb.Text.Substring(0, dstLangCb.Text.IndexOf(":"));
            Tr.Created = DateTime.Now;
            
            
            

            DialogResult = true;
        }

        private void assLoadButton_Click(object sender, RoutedEventArgs e)
        {
            isAss = true;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Subtitles"; // Default file name
            dialog.DefaultExt = ".ass"; // Default file extension
            dialog.Filter = "ASS subtitles (.ass)|*.ass"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                srtPath.Text = dialog.FileName;
            }
        }
    }
}
