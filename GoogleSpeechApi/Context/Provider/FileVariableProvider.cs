using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoogleSpeechApi.SpeechProcessing.Interfaces;

namespace GoogleSpeechApi.Context.Provider
{
    public class FileVariableProvider : IVariableProvider
    {
        public string Path { get; }
        private IPhoneticConverter PhoneticConverter { get; }

        public FileVariableProvider(string path, IPhoneticConverter phoneticConverter)
        {
            Path = path;
            PhoneticConverter = phoneticConverter;
        }

        public IEnumerable<VariableRepresentation> GetVariables()
        {
            string[] lines = File.ReadAllLines(Path);
            return lines
                .Select(line =>
                {
                    string phoneticName = PhoneticConverter.GetPhonetic(line);
                    VariableRepresentation variableRepresentation = new VariableRepresentation(line, phoneticName);
                    return variableRepresentation;
                });
        }
    }
}