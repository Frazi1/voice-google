using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.TextProcessing.Interfaces;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
    public class NoPhonetic : IPhoneticConverter
    {
        public string GetPhonetic(string input)
        {
            return input
                .RemoveChars(" ", "-", ".")
                .ToUpper();
        }
    }
}