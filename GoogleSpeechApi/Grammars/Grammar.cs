using System.Collections.Generic;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Config;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;

namespace GoogleSpeechApi.Grammars
{
    public class Grammar
    {
        private readonly IEnumerable<IInputPreprocessor> _preprocessors = DefaultConfig.GetPreprocessors();

        public List<GrammarCommandBinding> Bindings { get; }

        public Grammar()
        {
            Bindings = new List<GrammarCommandBinding>();
        }

        public bool Execute(string input)
        {
            string preprocessedString = _preprocessors.Run(input);
            foreach (var binding in Bindings)
            {
                bool executed = binding.Execute(preprocessedString);
                if (executed)
                    return true;
            }
            return false;
        }
    }
}