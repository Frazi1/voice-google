using System.Collections.Generic;

namespace GoogleSpeechApi.Grammars.Interfaces
{
    public interface ITextSequence
    {
        IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText);
    }
}