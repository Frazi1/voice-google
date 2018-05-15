using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace GoogleSpeechApi.Grammars.Preprocessors
{
    [UsedImplicitly]
    public class EnglishToRussianTransliterator
    {
        private static readonly Dictionary<string, string> Gost = new Dictionary<string, string>
        {
            {"Є", "EH"},
            {"І", "I"},
            {"і", "i"},
            {"№", "#"},
            {"є", "eh"},
            {"А", "A"},
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ё", "JO"},
            {"Ж", "ZH"},
            {"З", "Z"},
            {"И", "I"},
            {"Й", "JJ"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "KH"},
            {"Ц", "C"},
            {"Ч", "CH"},
            {"Ш", "SH"},
            {"Щ", "SHH"},
            {"Ъ", "'"},
            {"Ы", "Y"},
            {"Ь", ""},
            {"Э", "EH"},
            {"Ю", "YU"},
            {"Я", "YA"},
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "jo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "jj"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},

            {"ф", "f"},
            {"х", "kh"},
            {"ц", "c"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "shh"},
            {"ъ", ""},
            {"ы", "y"},
            {"ь", ""},
            {"э", "eh"},
            {"ю", "yu"},
            {"я", "ya"},
            {"«", ""},
            {"»", ""},
            {"—", "-"},
            {" ", "-"},
        }; //ГОСТ 16876-71

        public readonly Dictionary<string, string> Iso = new Dictionary<string, string>
        {
            {"Є", "YE"},
            {"І", "I"},
            {"Ѓ", "G"},
            {"і", "i"},
            {"№", "#"},
            {"є", "ye"},
            {"ѓ", "g"},
            {"А", "A"},
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ё", "YO"},
            {"Ж", "ZH"},
            {"З", "Z"},
            {"И", "I"},
            {"Й", "J"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "X"},
            {"Ц", "C"},
            {"Ч", "CH"},
            {"Ш", "SH"},
            {"Щ", "SHH"},
            {"Ъ", "'"},
            {"Ы", "Y"},
            {"Ь", ""},
            {"Э", "E"},
            {"Ю", "YU"},
            {"Я", "YA"},
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "yo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "j"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "x"},
            {"ц", "c"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "shh"},
            {"ъ", ""},
            {"ы", "y"},
            {"ь", ""},
            {"э", "e"},
            {"ю", "yu"},
            {"я", "ya"},
            {"«", ""},
            {"»", ""},
            {"—", "-"},
            {" ", "-"}
        }; //ISO 9-95


        public string ToEnglish(string text, TransliterationType type = TransliterationType.ISO)
        {
            string output = text;

            output = Regex.Replace(output, @"\s|\.|\(", " ");
            output = Regex.Replace(output, @"\s+", " ");
            output = Regex.Replace(output, @"[^\s\w\d-]", "");
            output = output.Trim();

            Dictionary<string, string> tdict = GetDictionaryByType(type);

            foreach (KeyValuePair<string, string> key in tdict)
            {
                output = output.Replace(key.Key, key.Value);
            }

            return output;
        }

        public string ToRussian(string text, TransliterationType type = TransliterationType.ISO)
        {
            string output = text;

            Dictionary<string, string> tdict = GetDictionaryByType(type);
            foreach (KeyValuePair<string, string> key in tdict)
            {
                if (!string.IsNullOrEmpty(key.Value))
                output = output.Replace(key.Value, key.Key);
            }

            return output;
        }

        private Dictionary<string, string> GetDictionaryByType(TransliterationType type)
        {
            Dictionary<string, string> tdict = Iso;
            if (type == TransliterationType.Gost) tdict = Gost;
            return tdict;
        }
    }

    public enum TransliterationType
    {
        Gost,
        ISO
    }
}