using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class DecimalParserTests
    {
        [TestCase("1.92")]
        [TestCase("29.76")]
        [TestCase("23.1123")]
        [TestCase("323.23")]
        [TestCase("0.00")]
        [TestCase("1.0")]
        public void DecimalParser_Success_Test(string value)
        {
            var resultState = Parser.Number().Run(value);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Number);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(DoubleResult), resultState.Result);
        }

        [TestCase("..2")]
        [TestCase("S90w.w")]
        [TestCase("*009.s2.2")]
        [TestCase("_90..178")]
        [TestCase("|.0")]
        [TestCase("O1.")]
        public void DecimalParser_Failure_Test(string value)
        {
            var resultState = Parser.Number().Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
