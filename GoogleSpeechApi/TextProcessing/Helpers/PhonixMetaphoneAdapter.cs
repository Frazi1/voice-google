using GoogleSpeechApi.TextProcessing.Interfaces;
using Phonix;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
    public class PhonixMetaphoneAdapter : IPhoneticConverter

    {
        private readonly Metaphone _metaphone = new Metaphone(50);
        public string GetPhonetic(string input)
        {
            return _metaphone.BuildKey(input);
        }
    }
}