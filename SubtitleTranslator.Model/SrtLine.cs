using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslator.Model
{
    public class SrtLine
    {
        [Key]
        public int? Id { get; set; }    

        public int? lineNumber { get; set; }

        public string timeStamp { get; set; }
        public string text { get; set; }

        public string? translatedText { get; set; }




        public int? TranslationId { get; set; } // właściwość klucza obcego
        public virtual Translation Translation { get; set; } //właściwość nawigacyjna
    }
}
