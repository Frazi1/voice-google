using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class TextSequence : ITextSequence
    {
        private const char WhiteSpace = ' ';

        public List<string> MatchingPattern { get; }

        public TextSequence(IEnumerable<string> matchingPattern)
        {
            MatchingPattern = matchingPattern
                .Select(s => s.ToLower())
                .ToList();
        }

        public string Match(string text, out string remainingText)
        {
            List<string> textPattern = text
                .Split(WhiteSpace)
                .Select(s => s.ToLower())
                .ToList();

            remainingText = text;
            int count = MatchingPattern.Count;
            if (textPattern.Count < count)
            {
                return string.Empty;
            }

            List<string> matchesList = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (textPattern[i] == MatchingPattern[i])
                    matchesList.Add(textPattern[i]);
                else
                    return string.Empty;
            }

            string matched = string.Join(WhiteSpace.ToString(), matchesList);
            remainingText = text.Replace(matched, string.Empty);
            return matched;
        }
    }
}