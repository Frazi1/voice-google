using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class GrammarRule : IGrammarRule
    {
        public List<ITextSequence> TextSequences { get; }

        public GrammarRule()
        {
            TextSequences = new List<ITextSequence>();
        }

        public GrammarRule(List<ITextSequence> textSequences)
        {
            TextSequences = textSequences;
        }

        public bool Match(string input, out string mathedResult)
        {
            bool result = true;
            mathedResult = string.Empty;
            foreach (ITextSequence textSequence in TextSequences)
            {
                if (result)
                {
                    mathedResult = textSequence.Match(input, out input);
                    result = mathedResult != string.Empty;
                }
                else
                    break;
            }
            return result;
        }

    }
}