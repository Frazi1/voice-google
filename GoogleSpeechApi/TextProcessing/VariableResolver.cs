using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.DI.Interfaces;
using GoogleSpeechApi.Extensions;
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

        private const int MaxPlainTextAllowedDistance = 3;

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
            var distances = CalculatePhoneticDistances(variables, phoneticInput);
            List<KeyValuePair<VariableRepresentation, int>> sortedDistances = distances
                .ToList().OrderBy(pair => pair.Value)
                .ToList();
            sortedDistances.Take(5).ForEach(kvp => _logger.Info($"{kvp.Key.Name}({kvp.Key.PhoneticName}) - {kvp.Value}"));
            _logger.Info("=========================================");
            List<KeyValuePair<VariableRepresentation, int>> phoneticDistances = sortedDistances
                .Where(kvp => kvp.Value == sortedDistances.Min(pair => pair.Value))
                .ToList();
            if (phoneticDistances.Count == 1)
            {
                return phoneticDistances.First().Key.Name;
            }

            var textDistances = CalculateTextDistances(phoneticDistances, transliteratedInput)
                .OrderBy(pair => pair.Value).ToList();
            _logger.Info($"Transliterated: {transliteratedInput}");
            textDistances.Take(5).ForEach(kvp => _logger.Info($"{kvp.Key.Name} - {kvp.Value}"));
            if (textDistances.First().Value > MaxPlainTextAllowedDistance)
                return phoneticDistances.First().Key.Name;
            return textDistances.First().Key.Name;
            //sortedDistances.First().Key.Name;
        }

        private Dictionary<VariableRepresentation, int> CalculatePhoneticDistances(List<VariableRepresentation> variables,
            string phoneticInput)
        {
            var res = new Dictionary<VariableRepresentation, int>(variables.Count);
            foreach (var variableRepresentation in variables)
            {
                res[variableRepresentation] =
                    _distanceEvaluator.GetDistance(variableRepresentation.PhoneticName, phoneticInput);
            }

            return res;
        }

        private Dictionary<VariableRepresentation, int> CalculateTextDistances(
            IEnumerable<KeyValuePair<VariableRepresentation, int>> variables, string transliteratedInput)
        {
            var variablesList = variables.ToList();
            var res = new Dictionary<VariableRepresentation, int>(variablesList.Count);
            foreach (var variableRepresentation in variablesList)
            {
                res[variableRepresentation.Key] =
                    _distanceEvaluator.GetDistance(variableRepresentation.Key.Name, transliteratedInput);
            }

            return res;
        }
    }
}