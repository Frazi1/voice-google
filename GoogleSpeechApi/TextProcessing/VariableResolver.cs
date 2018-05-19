using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.DI.Interfaces;
using GoogleSpeechApi.TextProcessing.Interfaces;
using JetBrains.Annotations;

namespace GoogleSpeechApi.TextProcessing
{
    [UsedImplicitly]
    public class VariableResolver
    {
        private readonly ILogger _logger;
        private readonly IPhoneticConverter _phoneticConverter;
        private readonly IStringDistanceEvaluator _distanceEvaluator;
        private readonly IVariableProvider _variableProvider;
        private readonly ITextTransliterator _transliterator;

        public VariableResolver(ILogger logger, IPhoneticConverter phoneticConverter,
            IStringDistanceEvaluator distanceEvaluator, IVariableProvider variableProvider,
            ITextTransliterator transliterator)
        {
            _logger = logger;
            _phoneticConverter = phoneticConverter;
            _distanceEvaluator = distanceEvaluator;
            _variableProvider = variableProvider;
            _transliterator = transliterator;
        }

        public string ResolveName(string input)
        {
            List<VariableRepresentation> variables = _variableProvider.GetVariables().ToList();
            string transliteratedInput = _transliterator.ToCodeNativeLanguage(input);
            string phoneticInput = _phoneticConverter.GetPhonetic(transliteratedInput);
            _logger.Info("=========================================");
            _logger.Info($"Input: {input}");
            _logger.Info($"Transliterated: {transliteratedInput}");
            _logger.Info($"Phonetic: {phoneticInput}");
            var distances = CalculateDistances(variables, phoneticInput);
            var sortedDistances = distances
                .ToList().OrderBy(pair => pair.Value)
                .ToList();
            sortedDistances.ForEach(kvp => _logger.Info($"{kvp.Key.Name}({kvp.Key.PhoneticName}) - {kvp.Value}"));
            _logger.Info("=========================================");
            return sortedDistances.First().Key.Name;
        }

        private Dictionary<VariableRepresentation, int> CalculateDistances(List<VariableRepresentation> variables,
            string phoneticInput)
        {
            var res = new Dictionary<VariableRepresentation, int>(variables.Count);
            foreach (var variableRepresentation in variables)
            {
                res[variableRepresentation] = _distanceEvaluator.GetDistance(variableRepresentation.PhoneticName, phoneticInput);
            }
            return res;
        }
    }
}