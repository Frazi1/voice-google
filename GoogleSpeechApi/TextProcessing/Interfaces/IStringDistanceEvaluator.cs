namespace GoogleSpeechApi.TextProcessing.Interfaces
{
    public interface IStringDistanceEvaluator
    {
        int GetDistance(string s1, string s2);
    }
}