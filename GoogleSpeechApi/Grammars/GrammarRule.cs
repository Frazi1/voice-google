using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Commands.Interfaces;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class GrammarRule : IGrammarRule
    {
        public List<ITextSequence> TextSequences { get; }
        public ICommand Command { get; set; }

        public GrammarRule(List<ITextSequence> textSequences, ICommand command)
        {
            TextSequences = textSequences;
            Command = command;
        }

        public MatchingResult Match(IEnumerable<string> input, out IEnumerable<string> remaining)
        {
            List<string> matchedResult = new List<string>();
            remaining = input.ToList();
            foreach (ITextSequence textSequence in TextSequences)
            {
                List<string> currentMatch = textSequence.Match(remaining, out remaining).ToList();
                if(currentMatch.Any())
                    matchedResult.AddRange(currentMatch);
                else
                    break;
            }
            return new MatchingResult(matchedResult, matchedResult.JoinToString(" "));
        }

        public void ExecuteCommand(string param)
        {
            Command.Execute(param);
        }
    }
}