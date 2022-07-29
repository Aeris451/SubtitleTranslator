using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SubtitleTranslator.Model
{
    public static class Serializer
    {

        public static void Save(Translation tr, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in tr.srtLines)
                {
                    sw.WriteLine(item.lineNumber);
                    sw.WriteLine(item.timeStamp);
                    sw.WriteLine(item.translatedText+"\n");
                }
            }
        }


        public static void Load(List<SrtLine> lines, string path)
        {


            using (StreamReader sr = new StreamReader(path))
            {


                while (!sr.EndOfStream)
                {

                    SrtLine read = new SrtLine();

                    string line = sr.ReadLine();



                    if (Regex.IsMatch(line, @"^[0-9]{1,5}$"))
                    {
                        int.TryParse(line, out int lnNum);
                        read.lineNumber = lnNum;


                        line = sr.ReadLine();
                    }

                    if (Regex.IsMatch(line, @"^[0-9:,\-> ]{29,31}$"))
                    {
                        read.timeStamp = line;

                        while ((line = sr.ReadLine()) != String.Empty)
                        {
                            if (read.text == null)
                            {
                                read.text = line;
                            }
                            else
                            {
                                read.text = read.text + "\n" + line;
                            }



                            if (sr.EndOfStream)
                            {
                                break;
                            }

                        }
                        read.text = read.text + "\n";
                        lines.Add(read);
                    }






                }




            }






        }


        public static string tagRemover(string line)
        {
            line = line.Replace("{\\i1}", "");
            line = line.Replace("{\\i0}", "");
            line = line.Replace("{\\an8}", "");
            return line;
        }

        public static void LoadAss(List<SrtLine> lines, string path)
        {


            using (StreamReader sr = new StreamReader(path))
            {

                int num = 0;
                while (!sr.EndOfStream)
                {

                    SrtLine read = new SrtLine();

                    string line = sr.ReadLine();


                    if (line.Contains("Dialogue:"))
                    {

                        num++;
                        read.lineNumber = num;
                        read.timeStamp = line.Substring(10, 23);
                        read.timeStamp = ((read.timeStamp.Substring(0, 12)).Replace(",", "") + "0 --> 0" + (read.timeStamp.Substring(12)).Replace(",", "")+"0").Replace(".", ",");
                        read.text = line.Substring(line.IndexOf("0,,")+3);
                        if (read.text.Contains("\\N"))
                        {
                            read.text = read.text.Substring(0, read.text.IndexOf("\\N")) +"\n"+ read.text.Substring(read.text.IndexOf("\\N"));
                            read.text = tagRemover(read.text);
                            read.text = read.text.Replace("\\N", "");

                        }
                        lines.Add(read);
                    }



                    

                    if (sr.EndOfStream)
                    {
                        break;
                    }



                }




            }






        }

        

        public static void SaveAss(Translation tr, string path)
        {

            if(tr.srtLines != null)
            {

            
                using (StreamWriter sw = new StreamWriter(path))
                {

                    sw.Write(Languages.Styles()+ "\n\n");

                    foreach (var item in tr.srtLines)
                    {
                        if (item.translatedText != null)
                        {

                            item.translatedText = item.translatedText.Replace("\n", "\\N");

                            if (item.translatedText.Substring(item.translatedText.Length - 2) == "\\N")
                            {
                                item.translatedText = item.translatedText.Replace("\\N", "");
                            }
                        }
                        if (item.text.Contains("{\\an8}"))
                        {
                            item.translatedText = "{\\an8}" + item.translatedText;
                        }

                        item.timeStamp = item.timeStamp.Substring(0, 1) + "," + item.timeStamp.Substring(1, 10).Replace(",", ".")+item.timeStamp.Substring(12, item.timeStamp.Substring(12).Length-1).Replace(",", ".");
                        item.timeStamp = item.timeStamp.Replace(" --> 0", ",");
                        sw.WriteLine("Dialogue: "+item.timeStamp+ ",Default,,0,0,0,,"+item.translatedText); 
                    }
                }
            }


        }




    }
}

        

