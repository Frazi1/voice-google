using GoogleSpeechApi.TextProcessing.Interfaces;
using JetBrains.Annotations;
using nullpointer.Metaphone;

namespace GoogleSpeechApi.SpeechProcessing
{
    [UsedImplicitly]
    public class Metaphone : IPhoneticConverter
    {
        public string GetPhonetic(string input)
        {
            return new DoubleMetaphone(input).PrimaryKey;
        }
    }
}