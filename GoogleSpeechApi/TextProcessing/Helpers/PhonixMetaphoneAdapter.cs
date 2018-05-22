using GoogleSpeechApi.TextProcessing.Interfaces;
using Phonix;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
    public class PhonixMetaphoneAdapter : IPhoneticConverter

    {
        private readonly Metaphone _metaphone = new Metaphone(50);
        private readonly DoubleMetaphone _doubleMetaphone= new DoubleMetaphone(50);
        public string GetPhonetic(string input)
        {
            //return _metaphone.BuildKey(input);
            return _doubleMetaphone.BuildKey(input);
        }
    }
}