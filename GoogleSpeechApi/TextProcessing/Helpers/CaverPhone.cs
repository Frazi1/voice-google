using GoogleSpeechApi.TextProcessing.Interfaces;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
  
    public class CaverPhone : IPhoneticConverter
    {
        private readonly Phonix.CaverPhone _caverPhone = new Phonix.CaverPhone();
        public string GetPhonetic(string input)
        {
            return _caverPhone.BuildKey(input);
        }
    }
}