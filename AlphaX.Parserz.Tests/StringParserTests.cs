using AlphaX.Parserz.Results;
using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class StringParserTests
    {
        [TestCase("hello", "hello")]
        [TestCase("test", "test")]
        [TestCase("cool world", "cool world")]
        [TestCase("https://", "https://")]
        public void StringParser_Success_Test(string parser, string input)
        {
            var resultState = Parser.String(parser).Run(input);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.String);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(StringResult), resultState.Result);
        }

        [TestCase("hello", "asdas")]
        [TestCase("test", "124")]
        [TestCase("cool world", "cool e")]
        [TestCase("https://", "htts://")]
        public void StringParser_Failure_Test(string parser, string input)
        {
            var resultState = Parser.String(parser).Run(input);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
