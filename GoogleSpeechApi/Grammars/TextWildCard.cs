using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.TextProcessing;
using JetBrains.Annotations;

namespace GoogleSpeechApi.Grammars
{
    [UsedImplicitly]
    public class TextWildCard : ITextSequence
    {
        private readonly VariableResolver _variableResolver;

        public TextWildCard(VariableResolver variableResolver)
        {
            _variableResolver = variableResolver;
        }

        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            //TODO: make no greedy
            IEnumerable<string> list = text.ToList();

            string input = list.JoinToString(" ");
            string variable = _variableResolver.ResolveName(input);
            //input = _transliterator.ToEnglish(input, TransliterationType.ISO);
            //string phonetic = PhoneticConverter.GetPhonetic(input);
            //List<VariableRepresentation> vars = _ideContext.GetVariables().ToList();
            remainingText = Enumerable.Empty<string>();
            return new List<string> {variable};
        }
    }
}