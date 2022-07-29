using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslator.Model
{
    public static class Languages
    {
        public static List<String> GetSrc()
        {
            List<string> srcList = new List<string>(new string[]{
"BG: Bulgarian",
"CS: Czech",
"DA: Danish",
"DE: German",
"EL: Greek",
"EN: English",
"ES: Spanish",
"ET: Estonian",
"FI: Finnish",
"FR: French",
"HU: Hungarian",
"ID: Indonesian",
"IT: Italian",
"JA: Japanese",
"LT: Lithuanian",
"LV: Latvian",
"NL: Dutch",
"PL: Polish",
"PT: Portuguese (all Portuguese varieties mixed)",
"RO: Romanian",
"RU: Russian",
"SK: Slovak",
"SL: Slovenian",
"SV: Swedish",
"TR: Turkish",
"ZH: Chinese"
});
            return srcList;
        }

        public static List<String> GetDst()
        {
            List<string> dstList = new List<string>(new string[] {
"BG: Bulgarian",
"CS: Czech",
"DA: Danish",
"DE: German",
"EL: Greek",
"EN-GB: English (British)",
"EN-US: English (American)",
"ES: Spanish",
"ET: Estonian",
"FI: Finnish",
"FR: French",
"HU: Hungarian",
"ID: Indonesian",
"IT: Italian",
"JA: Japanese",
"LT: Lithuanian",
"LV: Latvian",
"NL: Dutch",
"PL: Polish",
"PT-PT: Portuguese (all Portuguese varieties excluding Brazilian Portuguese)",
"PT-BR: Portuguese (Brazilian)",
"RO: Romanian",
"RU: Russian",
"SK: Slovak",
"SL: Slovenian",
"SV: Swedish",
"TR: Turkish",
"ZH: Chinese"
});
            return dstList;
        }



        public static string Styles()
        {
            return "[Script Info]\nTitle: English (US)\nScriptType: v4.00+\nCollisions: Normal\nPlayDepth: 0\nWrapStyle: 0\nScaledBorderAndShadow: yes\nPlayResX: 640\nPlayResY: 360\n\n[V4+ Styles]\nFormat: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding\nStyle: Default,Roboto Medium,26,&H00FFFFFF,&H000000FF,&H00020713,&H00000000,0,0,0,0,100,100,0,0,1,1.3,0,2,20,20,23,1\n\n[Events]\nFormat: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text";
        }



    }



}
