using System;
using GoogleSpeechApi.SpeechProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Speech.Test
{
    [TestClass]
    public class LeventeinTest
    {
        private readonly LevensteinDistanceEvaluator _ev = new LevensteinDistanceEvaluator();
        [TestMethod]
        public void TestMethod1()
        {
            int d = _ev.GetDistance("cat", "tat");
            Assert.AreEqual(d, 1);
        }
    }
}
