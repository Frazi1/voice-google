using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;

namespace GoogleSpeechApi.Grammars.Config
{
    public class GrammarRuleBuilder
    {
        private readonly IEnumerable<string> _wildCards = new List<string>
        {
            "<text>"
        };

        private readonly IEnumerable<IInputPreprocessor> _preprocessors = DefaultConfig.GetPreprocessors();


        public GrammarRule FromString(string input)
        {
            string processedString = _preprocessors.Run(input);
            var rule = new GrammarRule();
            while (processedString != string.Empty)
            {
                rule.TextSequences.Add(GetNextTextSequence(processedString, out processedString));
            }
            return rule;
        }

        private bool IsWildCard(string lexem)
        {
            return _wildCards.Contains(lexem);
        }

        private ITextSequence GetNextTextSequence(string input, out string remainingString)
        {
            List<string> buffer = new List<string>();

            string word = input.PopWord(out input);
            if (IsWildCard(word))
                throw new NotImplementedException("Implement wildcards");
            while (!IsWildCard(word) && !string.IsNullOrEmpty(word))
            {
                buffer.Add(word);
                word = input.PopWord(out input);
            }

            remainingString = input;
            return new TextSequence(buffer);
        }
    }
}