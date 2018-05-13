using System.Collections.Generic;
using GoogleSpeechApi.Grammars.Interfaces;
using GoogleSpeechApi.Grammars.Preprocessors;

namespace GoogleSpeechApi.Grammars.Config
{
    public static class DefaultConfig
    {
        public static IEnumerable<IInputPreprocessor> GetPreprocessors()
        {
            return new List<IInputPreprocessor>
            {
                new StringLowerPreprocessor(),
                new TrimmerPreprocessor()
            };
        }
    }
}