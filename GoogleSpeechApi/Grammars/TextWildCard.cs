using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.DI.Interfaces;
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
        private readonly ILogger _logger;

        public TextWildCard(VariableResolver variableResolver, ILogger logger)
        {
            _variableResolver = variableResolver;
            _logger = logger;
        }

        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            //TODO: make no greedy
            IEnumerable<string> list = text.ToList();

            string input = list.JoinToString(" ");
            try
            {
                string variable = _variableResolver.ResolveName(input);
                //input = _transliterator.ToEnglish(input, TransliterationType.ISO);
                //string phonetic = PhoneticConverter.GetPhonetic(input);
                //List<VariableRepresentation> vars = _ideContext.GetVariables().ToList();
                remainingText = Enumerable.Empty<string>();
                return new List<string> { variable };
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
    }
}