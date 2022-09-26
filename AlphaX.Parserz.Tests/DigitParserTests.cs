using AlphaX.Parserz;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class DigitParserTests
    {
        private IParser _digitParser;

        [SetUp]
        public void Setup()
        {
            _digitParser = Parser.Digit;
        }

        [TestCase("1")]
        [TestCase("0")]
        [TestCase("5")]
        [TestCase("4")]
        public void DigitParser_Success_Test(string value)
        {
            var resultState = _digitParser.Run(value);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(ByteResult), resultState.Result);
        }

        [TestCase("a")]
        [TestCase(".")]
        [TestCase("/")]
        [TestCase("o")]
        [TestCase("^1")]
        [TestCase("x")]
        public void DecimalParser_Failure_Test(string value)
        {
            var resultState = _digitParser.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
