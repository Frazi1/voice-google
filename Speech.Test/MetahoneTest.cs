using GoogleSpeechApi.SpeechProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nullpointer.Metaphone;
using Phonix;
using DoubleMetaphone = nullpointer.Metaphone.DoubleMetaphone;

namespace Speech.Test
{
    [TestClass]
    public class MetahoneTest
    {
        private readonly DoubleMetaphone _metaphone = new ShortDoubleMetaphone();
        [TestMethod]
        public void Test1()
        {
            _metaphone.computeKeys("Filipowitz");
            string r = _metaphone.PrimaryKey;
            string alt = _metaphone.AlternateKey;
        }

        [TestMethod]
        public void Test2()
        {
            Phonix.Metaphone m = new Phonix.Metaphone();
            string  r = m.BuildKey("VariableRepresentations");
            string r1 = m.BuildKey("Filipowitz");

            var m2 = new Phonix.MatchRatingApproach();
            string r2 = m2.BuildKey("ajdaya-kontekst");
            int d = m2.MatchRatingCompute("ajdaya-kontekst", "idecontext");
            int d2 = m2.MatchRatingCompute("ajdaya-kontekst", "petuhebaniy");

            var m3 = new Phonix.CaverPhone();
            string r3 = m3.BuildKey("TextWildCard");
        }
    }
}