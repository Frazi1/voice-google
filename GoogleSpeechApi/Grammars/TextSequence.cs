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

        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            List<string> textPattern = text
                .ToList();

            remainingText = textPattern.ToList();
            if (textPattern.Count < MatchingPattern.Count)
            {
                return Enumerable.Empty<string>();
            }

            List<string> matchesList = new List<string>();
            for (int i = 0; i < MatchingPattern.Count; i++)
            {
                if (textPattern[i] == MatchingPattern[i])
                    matchesList.Add(textPattern[i]);
                else
                    return Enumerable.Empty<string>();
            }

            remainingText = textPattern.Skip(matchesList.Count).ToList();
            return matchesList;
        }
    }
}