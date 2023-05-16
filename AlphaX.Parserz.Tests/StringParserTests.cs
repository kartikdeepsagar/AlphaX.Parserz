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

        [TestCase("'hello'")]
        [TestCase("'test'")]
        [TestCase("'test sdas   sad[]324@#$'")]
        [TestCase("'cool world'")]
        [TestCase("'https:'")]
        public void StringValueParser_SingleQuote_Success_Test(string input)
        {
            var resultState = Parser.StringValue(false).Run(input);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.String);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(StringResult), resultState.Result);
        }

        [TestCase("\"hello\"")]
        [TestCase("\"hell $   @o\"")]
        [TestCase("\"test\"")]
        [TestCase("\"cool world\"")]
        [TestCase("\"https://\"")]
        public void StringValueParser_DoubleQuote_Success_Test(string input)
        {
            var resultState = Parser.StringValue().Run(input);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.String);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(StringResult), resultState.Result);
        }

        [TestCase("hello'")]
        [TestCase("'test")]
        public void StringValueParser_SingleQuote_Failure_Test(string input)
        {
            var resultState = Parser.StringValue(false).Run(input);
            Assert.IsTrue(resultState.IsError);
        }

        [TestCase("\'hello")]
        [TestCase("'test")]
        [TestCase("\"cool world'")]
        [TestCase("\"https://")]
        public void StringValueParser_DoubleQuote_Failure_Test(string input)
        {
            var resultState = Parser.StringValue().Run(input);
            Assert.IsTrue(resultState.IsError);
        }

        [TestCase("Hi i am alphax and i am better", "alphax")]
        public void UntilParser_Success_Test(string input, string selector)
        {
            var resultState = Parser.UntilFound(selector).Run(input);
            Assert.IsFalse(resultState.IsError);
        }
    }
}
