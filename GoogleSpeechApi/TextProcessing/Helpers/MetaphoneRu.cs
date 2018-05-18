using System.Text.RegularExpressions;
using GoogleSpeechApi.TextProcessing.Interfaces;
using JetBrains.Annotations;
// ReSharper disable All

namespace GoogleSpeechApi.SpeechProcessing
{
    // Алгоритм вычисления кода русского Metaphone
    // Для всех гласных букв проделать следующие операции:
    // ЙО, ИО, ЙЕ, ИЕ > И
    // О, Ы, Я > А
    // Е, Ё, Э > И
    // Ю > У
    // Для всех согласных букв, за которыми следует любая согласная, кроме Л, М, Н или Р, либо же для согласных на конце слова, провести оглушение:
    // Б > П
    // З > С
    // Д > Т
    // В > Ф
    // Г > К
    // Склеиваем ТС и ДС в Ц:
    // ТС > Ц

    [UsedImplicitly]
    public class MetaphoneRu: IPhoneticConverter
    {
        private static string Normalize(string value)
        {
            value = Regex.Replace(value, @"[^АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЫыЭэЮюЯя]",
                string.Empty);
            return value.ToUpper();
        }

        private static string RemoveDuplicates(string value)
        {
            return Regex.Replace(value, @"(.)\1{1,}", "$1");
        }

        private static string IOtoI(string value)
        {
            return value
                .Replace("ЙО", "И")
                .Replace("ИО", "И")
                .Replace("ЙЕ", "И")
                .Replace("ИЕ", "И");
        }

        private static string OtoA(string value)
        {
            return value
                .Replace("О", "А")
                .Replace("Ы", "А")
                .Replace("Я", "А");
        }

        private static string EtoI(string value)
        {
            return value
                .Replace("Е", "И")
                .Replace("Ё", "И")
                .Replace("Э", "И");
        }

        private static string UtoY(string value)
        {
            return value
                .Replace("Ю", "У");
        }

        private static string BtoP(string value)
        {
            value = Regex.Replace(value, @"Б[Б|В|Г|Д|Ж|З|Й|К|П|С|Т|Ф|Х|Ц|Ч|Ш|Щ]", "П");
            value = Regex.Replace(value, @"Б$", "П");
            return value;
        }

        private static string ZtoS(string value)
        {
            value = Regex.Replace(value, @"З[Б|В|Г|Д|Ж|З|Й|К|П|С|Т|Ф|Х|Ц|Ч|Ш|Щ]", "С");
            value = Regex.Replace(value, @"З$", "С");
            return value;
        }

        private static string DtoT(string value)
        {
            value = Regex.Replace(value, @"Д[Б|В|Г|Д|Ж|З|Й|К|П|С|Т|Ф|Х|Ц|Ч|Ш|Щ]", "Т");
            value = Regex.Replace(value, @"Д$", "Т");
            return value;
        }

        private static string VtoF(string value)
        {
            value = Regex.Replace(value, @"В[Б|В|Г|Д|Ж|З|Й|К|П|С|Т|Ф|Х|Ц|Ч|Ш|Щ]", "Ф");
            value = Regex.Replace(value, @"В$", "Ф");
            return value;
        }

        private static string GtoK(string value)
        {
            value = Regex.Replace(value, @"Г[Б|В|Г|Д|Ж|З|Й|К|П|С|Т|Ф|Х|Ц|Ч|Ш|Щ]", "К");
            value = Regex.Replace(value, @"Г$", "К");
            return value;
        }

        private static string TStoC(string value)
        {
            return Regex.Replace(value, @"[ТС|ДС]", "Ц");
        }

        /// <summary>
        /// Получение фонетического звучания
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetPhonetic(string value)
        {
            value = Normalize(value);
            value = RemoveDuplicates(value);
            value = IOtoI(value);
            value = OtoA(value);
            value = EtoI(value);
            value = UtoY(value);
            value = BtoP(value);
            value = ZtoS(value);
            value = DtoT(value);
            value = VtoF(value);
            value = GtoK(value);
            value = TStoC(value);
            value = RemoveDuplicates(value);
            return value;
        }
    }
}