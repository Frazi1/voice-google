namespace GoogleSpeechApi.TextProcessing.Interfaces
{
    public interface IPhonemeCorrector
    {
        string Correct(string input);
    }
}