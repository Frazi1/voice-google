using System.Collections.Generic;
using System.Linq;

namespace GoogleSpeechApi.Grammars
{
    public class MatchingResult
    {
        public List<string> Matches { get; }
        public string InvokationParam { get; }
        public bool IsMatched => Matches.Any();


        public MatchingResult(List<string> matches, string invokationParam)
        {
            Matches = matches;
            InvokationParam = invokationParam;
        }
    }
}