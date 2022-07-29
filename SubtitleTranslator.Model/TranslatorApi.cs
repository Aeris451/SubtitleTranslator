using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepL;
using System.IO;
using System.Windows;

namespace SubtitleTranslator.Model
{
    public static class TranslatorApi
    {

        
        public static async void Translator(string key, string dst, string sc, List<SrtLine> lines)
        {
            
            double i = 0;
            var translator = new Translator(key);
            string line;

            
            foreach (var item in lines)
            {
                if(item.text != String.Empty)
                {
                    line = item.text;
                    if (line.Contains("{\\an8}"))
                    {
                        line = line.Replace("{\\an8}", "");
                    }

                    item.translatedText = (await translator.TranslateTextAsync(line, sc, dst)).Text;
                    i = i + item.text.Length;
                }

            }
            MessageBox.Show("Translated " + i + " characters");
        }
        
        public static async void GetUsage(string key)
        {
            var translator = new Translator(key);

            var usage = await translator.GetUsageAsync();
            if (usage.AnyLimitReached)
            {
                MessageBox.Show("Translation limit exceeded.");
            }
            else if (usage.Character != null)
            {
                MessageBox.Show($"Character usage: {usage.Character}");
            }
            else
            {
                MessageBox.Show($"{usage}");

                
            }
        }

    }
}
