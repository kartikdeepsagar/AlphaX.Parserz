using AlphaX.Parserz;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class LetterParserTests
    {
        private IParser _letterParser;

        [SetUp]
        public void Setup()
        {
            _letterParser = Parser.Letter;
        }

        [TestCase("a")]
        [TestCase("X")]
        [TestCase("x")]
        [TestCase("g")]
        [TestCase("P")]
        public void LetterParser_Success_Test(string value)
        {
            var resultState = _letterParser.Run(value);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(CharResult), resultState.Result);
        }

        [TestCase("&")]
        [TestCase("1")]
        [TestCase("9")]
        [TestCase("[")]
        [TestCase("^1")]
        [TestCase(";")]
        public void LetterParser_Failure_Test(string value)
        {
            var resultState = _letterParser.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
