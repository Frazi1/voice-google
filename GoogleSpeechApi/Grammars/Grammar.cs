using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Commands.Interfaces;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Config;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;

namespace GoogleSpeechApi.Grammars
{
    public class Grammar
    {
        private readonly IEnumerable<IInputPreprocessor> _preprocessors = DefaultConfig.GetPreprocessors();
        private readonly Queue<Tuple<ICommand, string>> _invokationQueue = new Queue<Tuple<ICommand, string>>();

        public List<GrammarRule> Rules { get; }

        public Grammar()
        {
            Rules = new List<GrammarRule>();
        }

        public bool Execute(string input)
        {
            List<string> preprocessedInput = Preprocess(input);
            IEnumerable<string> remaining = preprocessedInput.ToList();
            int currentRuleIndex = 0;
            while(currentRuleIndex < Rules.Count && remaining.Any())
            {
                var rule = Rules[currentRuleIndex];
                MatchingResult result = rule.Match(remaining, out remaining);
                if (result.IsMatched)
                {
                    rule.ExecuteCommand(result.InvokationParam);
                    currentRuleIndex = 0;
                }
                currentRuleIndex++;
            }
            return remaining.Any();
        }

        private List<string> Preprocess(string input)
        {
            List<string> preprocessedInput = _preprocessors.Run(input)
                .Split(' ')
                .ToList();
            return preprocessedInput;
        }
    }
}