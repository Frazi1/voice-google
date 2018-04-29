using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars.Preprocessors
{
    internal class TrimmerPreprocessor : IInputPreprocessor
    {
        public string Process(string input)
        {
            return input.Trim();
        }
    }
}