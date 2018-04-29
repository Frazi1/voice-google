namespace GoogleSpeechApi.Grammars
{
    internal interface IInputPreprocessor
    {
        string Process(string input);
    }
}