﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoogleSpeechApi.Extensions;
using GoogleSpeechApi.Grammars.Preprocessors;
using GoogleSpeechApi.TextProcessing.Helpers;
using GoogleSpeechApi.TextProcessing.Interfaces;
using JetBrains.Annotations;

namespace GoogleSpeechApi.Context.Provider
{
    [UsedImplicitly]
    public class FileVariableProvider : IVariableProvider
    {
        public string Path { get; }
        private IPhoneticConverter PhoneticConverter { get; }
        private readonly ITextTransliterator _transliterator;

        public FileVariableProvider(string path, IPhoneticConverter phoneticConverter, ITextTransliterator transliterator)
        {
            Path = path;
            PhoneticConverter = phoneticConverter;
            _transliterator = transliterator;
        }

        public IEnumerable<VariableRepresentation> GetVariables()
        {
            string[] lines = File.ReadAllLines(Path);
            return lines
                .Select(name =>
                {
                    //string russianName = _transliterator.ToRussian(line);
                    string phoneticName = PhoneticConverter.GetPhonetic(name);
                    VariableRepresentation variableRepresentation = new VariableRepresentation(name, phoneticName, name.UpperNormalize());
                    return variableRepresentation;
                });
        }
    }
}