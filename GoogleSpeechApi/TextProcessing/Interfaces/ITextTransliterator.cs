namespace GoogleSpeechApi.TextProcessing.Interfaces
{
    public interface ITextTransliterator
    {
        string ToCodeNativeLanguage(string input);
        string ToSpeechNativeLanguage(string input);
    }
}