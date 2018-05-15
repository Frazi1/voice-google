using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoogleSpeechApi.Grammars.Preprocessors;
using GoogleSpeechApi.SpeechProcessing.Interfaces;
using JetBrains.Annotations;

namespace GoogleSpeechApi.Context.Provider
{
    [UsedImplicitly]
    public class FileVariableProvider : IVariableProvider
    {
        public string Path { get; }
        private IPhoneticConverter PhoneticConverter { get; }
        private readonly EnglishToRussianTransliterator _transliterator;

        public FileVariableProvider(string path, IPhoneticConverter phoneticConverter, EnglishToRussianTransliterator transliterator)
        {
            Path = path;
            PhoneticConverter = phoneticConverter;
            _transliterator = transliterator;
        }

        public IEnumerable<VariableRepresentation> GetVariables()
        {
            string[] lines = File.ReadAllLines(Path);
            return lines
                .Select(line =>
                {
                    //string russianName = _transliterator.ToRussian(line);
                    string phoneticName = PhoneticConverter.GetPhonetic(line);
                    VariableRepresentation variableRepresentation = new VariableRepresentation(line, phoneticName);
                    return variableRepresentation;
                });
        }
    }
}