using System.Collections.Generic;

namespace GoogleSpeechApi.Grammars.Interfaces
{
    public interface IGrammarRule
    {
        MatchingResult Match(IEnumerable<string> input, out IEnumerable<string> remaining);
        void ExecuteCommand(string param);
    }
}