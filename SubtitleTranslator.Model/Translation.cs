using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslator.Model
{
    public class Translation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TranslationId { get; set; }
        public string? sourceLanguage { get; set; }
        public string? destinationLanguage { get; set; }
        public string Name { get; set; }
        public string? Licence { get; set; }
        public DateTime Created { get; set; }  

        public  List<SrtLine> srtLines { get; set; }

        


        public Translation()
        {
            sourceLanguage = "Brak";
            destinationLanguage = "Brak";
        }

        public Translation(string sLang, string dLang)
        {
            sourceLanguage = sLang;
            destinationLanguage = dLang;
        }

        public Translation(Translation translation)
        {
            sourceLanguage = translation.sourceLanguage;
            destinationLanguage = translation.destinationLanguage;
        }


    }
}
