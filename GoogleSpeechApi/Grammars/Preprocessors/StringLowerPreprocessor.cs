using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars.Preprocessors
{
    internal class StringLowerPreprocessor : IInputPreprocessor
    {
        public string Process(string input)
        {
            return input.ToLower();
        }
    }
}