using GoogleSpeechApi.TextProcessing.Interfaces;
using JetBrains.Annotations;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
    [UsedImplicitly]
    public class SimplePhonemeCorrector : IPhonemeCorrector
    {
        public string Correct(string input)
        {
            return input.ToUpper()
                .Replace("X", "KS");
        }
    }
}