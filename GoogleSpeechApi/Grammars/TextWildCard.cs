using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.SpeechProcessing.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class TextWildCard : ITextSequence
    {
        private IPhoneticConverter PhoneticConverter { get; }
        private readonly IIdeContext _ideContext;

        public TextWildCard(IPhoneticConverter phoneticConverter, IIdeContext ideContext)
        {
            PhoneticConverter = phoneticConverter;
            _ideContext = ideContext;
        }

        public IEnumerable<string> Match(IEnumerable<string> text, out IEnumerable<string> remainingText)
        {
            //TODO: make no greedy
            IEnumerable<string> list = text.ToList();
            
            string phonetic = PhoneticConverter.GetPhonetic(list.JoinToString(" "));
            List<VariableRepresentation> vars = _ideContext.GetVariables().ToList();
            remainingText = Enumerable.Empty<string>();
            return list;
        }
    }
}