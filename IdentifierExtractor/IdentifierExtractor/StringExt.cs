using System;
using System.Linq;

namespace IdentifierExtractor
{
    public static class StringExt
    {
        public static string RemoveBetween(this string text, string start, string end)
        {
            while (true)
            {
                int startIndex = text.IndexOf(start, StringComparison.Ordinal);
                if (startIndex < 0)
                    return text;
                int endIndex = text.IndexOf(end, startIndex, StringComparison.Ordinal);
                if (endIndex < 0)
                    return text;
                text = text.Remove(startIndex, endIndex - startIndex + end.Length);
            }
        }

        public static string ReplaceMany(this string text, string[] toReplace, string newChar)
        {
            string result = text;
            foreach (var c in toReplace)
                result = result.Replace(c, newChar);
            return result;
        }
    }
}