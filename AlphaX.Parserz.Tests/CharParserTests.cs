using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class CharParserTests
    {
        [TestCase('a', "a")]
        [TestCase('b', "b")]
        [TestCase('1', "1")]
        [TestCase('2', "2")]
        public void CharParser_Success_Test(char parser, string input)
        {
            var resultState = Parser.Char(parser).Run(input);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Char);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(CharResult), resultState.Result);
        }

        [TestCase('a', "x")]
        [TestCase('b', "4")]
        [TestCase('1', "2")]
        [TestCase('2', "5")]
        public void CharParser_Failure_Test(char parser, string input)
        {
            var resultState = Parser.Char(parser).Run(input);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
