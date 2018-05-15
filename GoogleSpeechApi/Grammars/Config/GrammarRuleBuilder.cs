using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Commands.Interfaces;
using GoogleSpeechApi.DI;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;
using Ninject;

namespace GoogleSpeechApi.Grammars.Config
{
    public class GrammarRuleBuilder
    {
        public static GrammarRuleBuilder Get => new GrammarRuleBuilder();

        private readonly IEnumerable<string> _wildCards = new List<string>
        {
            "<text>"
        };

        private readonly IEnumerable<IInputPreprocessor> _preprocessors = DefaultConfig.GetPreprocessors();

        private ICommand Command { get; set; }
        private string InputString { get; set; }

        public GrammarRuleBuilder FromString(string input)
        {
            InputString = input;
            return this;
        }

        public GrammarRuleBuilder WithCommand(ICommand command)
        {
            Command = command;
            return this;
        }

        public GrammarRule Build()
        {
            string processedString = _preprocessors.Run(InputString);
            List<ITextSequence> sequences = new List<ITextSequence>();
            while (processedString != string.Empty)
            {
                sequences.Add(GetNextTextSequence(processedString, out processedString));
            }

            var rule = new GrammarRule(sequences, Command);
            return rule;
        }

        private bool IsWildCard(string lexem)
        {
            return _wildCards.Contains(lexem);
        }

        private ITextSequence GetNextTextSequence(string input, out string remainingString)
        {
            List<string> buffer = new List<string>();

            string word = input.PopWord(out remainingString);
            if (IsWildCard(word))
                return Kernel.Instance.Get<TextWildCard>();

            while (!string.IsNullOrEmpty(word))
            {
                buffer.Add(word);
                if (!IsWildCard(remainingString.PeekWord()))
                    word = remainingString.PopWord(out remainingString);
                else
                    break;
            }

            return new TextSequence(buffer);
        }
    }
}