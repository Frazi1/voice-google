using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;
using GoogleSpeechApi.SpeechProcessing.Interfaces;
using JetBrains.Annotations;

namespace GoogleSpeechApi.Grammars
{
    [UsedImplicitly]
    public class TextWildCard : ITextSequence
    {
        private IPhoneticConverter PhoneticConverter { get; }
        private readonly IIdeContext _ideContext;
        private readonly EnglishToRussianTransliterator _transliterator;

        public TextWildCard(IPhoneticConverter phoneticConverter, IIdeContext ideContext,
            EnglishToRussianTransliterator transliterator)
        {
            PhoneticConverter = phoneticConverter;
            _ideContext = ideContext;
            _transliterator = transliterator;
        }

        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            //TODO: make no greedy
            IEnumerable<string> list = text.ToList();

            string input = list.JoinToString(" ");
            string phonetic = PhoneticConverter.GetPhonetic(_transliterator.ToEnglish(input));
            List<VariableRepresentation> vars = _ideContext.GetVariables().ToList();
            //input = _transliterator.ToEnglish(input, TransliterationType.ISO);
            //string phonetic = PhoneticConverter.GetPhonetic(input);
            //List<VariableRepresentation> vars = _ideContext.GetVariables().ToList();
            remainingText = Enumerable.Empty<string>();
            return list;
        }
    }
}