using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class TextWildCard : ITextSequence
    {
        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            //TODO: make no greedy
            remainingText = Enumerable.Empty<string>();
            return text;
        }
    }
}