using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IdentifierExtractor
{
    public class FileReader
    {
        private const string MultiLineCommentStart = "/*";
        private const string MultiLineCommentEnd = "*/";
        private const string SingleLineComment = "//";

        public List<string> GetWordsFromFile(string path)
        {
            string text = string.Join(" ", File.ReadAllLines(path));
            text = text.RemoveBetween(MultiLineCommentStart, MultiLineCommentEnd);
            text = text.RemoveBetween(SingleLineComment, "\n");
            text = text.RemoveBetween('"'.ToString(), '"'.ToString());
            List<string> words = text.Split(new[] {' ', '.', ',','(','<','>','['}, StringSplitOptions.RemoveEmptyEntries).ToList();
            words = words
				.Select(w => w.ReplaceMany(new[] {"[", "]", "(", ")", ";", "{", "}", "&", "|", "?", "+", "=", "-", "%", "/", "*", ">"}, ""))
                .Where(w => w != string.Empty)
                .Distinct()
                .ToList();
            return words;
        }
    }
}