using System.Collections.Generic;

namespace GoogleSpeechApi.Grammars
{
    public class Grammar
    {
        private readonly List<IInputPreprocessor> _preprocessors = new List<IInputPreprocessor>
        {
            new TrimmerPreprocessor(),
            new StringLowerPreprocessor()
        };

        public List<GrammarCommandBinding> Bindings { get; }

        public Grammar()
        {
            Bindings = new List<GrammarCommandBinding>();
        }

        public bool Execute(string input)
        {
            _preprocessors.ForEach(p => { input = p.Process(input); });
            foreach (var binding in Bindings)
            {
                bool executed = binding.Execute(input);
                if (executed)
                    return true;
            }
            return false;
        }
    }

    internal class TrimmerPreprocessor : IInputPreprocessor
    {
        public string Process(string input)
        {
            return input.Trim();
        }
    }

    internal class StringLowerPreprocessor : IInputPreprocessor
    {
        public string Process(string input)
        {
            return input.ToLower();
        }
    }
}