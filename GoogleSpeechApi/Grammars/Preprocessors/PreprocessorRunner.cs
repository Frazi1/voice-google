using System.Collections.Generic;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Interfaces;

namespace GoogleSpeechApi.Grammars.Preprocessors
{
    public static class PreprocessorRunner
    {
        public static string Run(this IEnumerable<IInputPreprocessor> preprocessors, string input)
        {
            preprocessors.ForEach(p => { input = p.Process(input); });
            return input;
        }
    }
}