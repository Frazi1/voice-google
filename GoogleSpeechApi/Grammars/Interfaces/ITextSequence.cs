namespace GoogleSpeechApi.Grammars.Interfaces
{
    public interface ITextSequence
    {
        string Match(string text, out string remainingText);
    }
}