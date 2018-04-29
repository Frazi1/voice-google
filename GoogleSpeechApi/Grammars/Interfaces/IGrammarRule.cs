namespace GoogleSpeechApi.Grammars.Interfaces
{
    public interface IGrammarRule
    {
        bool Match(string input, out string matchedResult);
    }
}