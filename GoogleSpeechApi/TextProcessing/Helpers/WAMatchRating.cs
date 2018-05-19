using GoogleSpeechApi.TextProcessing.Interfaces;
using Phonix;

namespace GoogleSpeechApi.TextProcessing.Helpers
{
    public class WAMatchRating : IPhoneticConverter
    {
        private readonly MatchRatingApproach _approach = new MatchRatingApproach();
        private readonly IPhonemeCorrector _corrector;
        public WAMatchRating(IPhonemeCorrector corrector)
        {
            _corrector = corrector;
        }

        public string GetPhonetic(string input)
        {
            string phonetic = _approach.BuildKey(input);
            string corrected = _corrector.Correct(phonetic);
            return corrected;
        }
    }
}