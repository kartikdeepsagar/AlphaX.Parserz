using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class LetterParserTests
    {
        [TestCase("a")]
        [TestCase("X")]
        [TestCase("x")]
        [TestCase("g")]
        [TestCase("P")]
        public void LetterParser_Success_Test(string value)
        {
            var resultState = Parser.Letter.Run(value);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Char);
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
            var resultState = Parser.Letter.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
