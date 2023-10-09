using System;
using System.Linq;
using CrossInformAnalyzer;
using NUnit.Framework;
using CrossInformTextAnalyzer;

namespace CrossInformAnalyzerTest
{
    public class Tests
    {
        [Test]
        public void ReadTextCorrectTest()
        {
            var textAnalyzer = new TextAnalyzer();
            textAnalyzer.ReadText("..\\..\\..\\..\\Test1.txt");

            Assert.IsNotNull(textAnalyzer.GetText());
        }

        [Test]
        public void ReadTextIncorrectTest()
        {
            var textAnalyzer = new TextAnalyzer();

            Assert.Throws<ArgumentNullException>(() => textAnalyzer.ReadText(null));
        }

        [Test]
        public void ReadTextFromNonexistentFileText()
        {
            var textAnalyzer = new TextAnalyzer();
            
            Assert.Throws<ArgumentException>(() => textAnalyzer.ReadText("..Test1.txt"));
        }


        [Test]
        public void GetPopularZeroLengthSubstringTest()
        {
            var textAnalyzer = new TextAnalyzer();

            Assert.Throws<ArgumentOutOfRangeException>(() => textAnalyzer.GetPopularSubstrings(0, 10));
        }

        [Test]
        public void GetZeroPopularSubstringsTest()
        {
            var textAnalyzer = new TextAnalyzer();

            Assert.Throws<ArgumentOutOfRangeException>(() => textAnalyzer.GetPopularSubstrings(3, 0));
        }

        [Test]
        public void CheckFirstPopularSubstringTest()
        {
            var textAnalyzer = new TextAnalyzer();
            var filePath = "..\\..\\..\\..\\Test1.txt";
            var substrings = textAnalyzer.GetPopularSubstrings(3, 10, filePath);
            Assert.AreEqual(substrings.FirstOrDefault().Key, "ени");
            Assert.AreEqual(substrings.FirstOrDefault().Value, 14);
        }
    }
}