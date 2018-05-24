using System;
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
        private const int MaxPhoneticLenghtDifference = 3;

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
            string normalizedInput = transliteratedInput.UpperNormalize();
            string phoneticInput = _phoneticConverter.GetPhonetic(transliteratedInput);
            _logger.Info("=========================================");
            _logger.Info($"Input: {input}");
            _logger.Info($"Transliterated: {transliteratedInput}");
            _logger.Info($"Normalized: {normalizedInput}");
            _logger.Info($"Phonetic: {phoneticInput}");
            var varsToCompare = variables.Where(v =>
                Math.Abs(v.PhoneticName.Length - phoneticInput.Length) <= MaxPhoneticLenghtDifference).ToList();
            var distances = CalculatePhoneticDistances(varsToCompare, phoneticInput);
            List<KeyValuePair<VariableRepresentation, int>> sortedDistances = distances
                .ToList().OrderBy(pair => pair.Value)
                .ToList();
            sortedDistances.Take(5)
                .ForEach(kvp => _logger.Info($"{kvp.Key.Name}({kvp.Key.PhoneticName}) - {kvp.Value}"));
            _logger.Info("=========================================");
            List<KeyValuePair<VariableRepresentation, int>> phoneticDistancesTop = sortedDistances
                .Where(kvp => kvp.Value == sortedDistances.Min(pair => pair.Value))
                .ToList();
            if (phoneticDistancesTop.Count == 1)
            {
                return phoneticDistancesTop.First().Key.Name;
            }

            var textDistances = CalculateTextDistances(phoneticDistancesTop, normalizedInput)
                .OrderBy(pair => pair.Value).ToList();
            int textDistancesMinDiff = textDistances.Min(kvp => kvp.Value);
            var textDistancesTop = textDistances.Where(pair => pair.Value == textDistancesMinDiff).ToList();
            _logger.Info($"Top normalized: {transliteratedInput}");
            textDistances.Take(5).ForEach(kvp => _logger.Info($"{kvp.Key.Name} ({kvp.Key.NormalizedName}) - {kvp.Value}"));
            if (textDistancesTop.Count == 1 || textDistancesTop.First().Value <= MaxPlainTextAllowedDistance)
                return textDistancesTop.First().Key.Name;
            return phoneticDistancesTop.First().Key.Name;
            //sortedDistances.First().Key.Name;
        }

        private Dictionary<VariableRepresentation, int> CalculatePhoneticDistances(
            List<VariableRepresentation> variables,
            string phoneticInput)
        {
            var res = new Dictionary<VariableRepresentation, int>(variables.Count);
            foreach (var variableRepresentation in variables)
            {
                try
                {
                    res[variableRepresentation] =
                        _distanceEvaluator.GetDistance(variableRepresentation.PhoneticName, phoneticInput);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    _logger.Info(variableRepresentation.ToString());
                    throw;
                }
                
            }

            return res;
        }

        private Dictionary<VariableRepresentation, int> CalculateTextDistances(
            IEnumerable<KeyValuePair<VariableRepresentation, int>> variables, string normalizedTransliteratedInput)
        {
            var variablesList = variables.ToList();
            var res = new Dictionary<VariableRepresentation, int>(variablesList.Count);
            foreach (var variableRepresentation in variablesList)
            {
                res[variableRepresentation.Key] =
                    _distanceEvaluator.GetDistance(variableRepresentation.Key.NormalizedName, normalizedTransliteratedInput);
            }

            return res;
        }
    }
}