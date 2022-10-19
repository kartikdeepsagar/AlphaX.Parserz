using AlphaX.Parserz.Results;
using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class DigitParserTests
    {
        [TestCase("1")]
        [TestCase("0")]
        [TestCase("5")]
        [TestCase("4")]
        public void DigitParser_Success_Test(string value)
        {
            var resultState = Parser.Digit.Run(value);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Number);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(DoubleResult), resultState.Result);
        }

        [TestCase("a")]
        [TestCase(".")]
        [TestCase("/")]
        [TestCase("o")]
        [TestCase("^1")]
        [TestCase("x")]
        public void DecimalParser_Failure_Test(string value)
        {
            var resultState = Parser.Digit.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
