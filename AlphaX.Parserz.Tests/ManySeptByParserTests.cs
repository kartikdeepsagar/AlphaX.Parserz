using NUnit.Framework;
using AlphaX.Parserz.Extensions;

namespace AlphaX.Parserz.Tests
{
    public class ManySeptByParserTests
    {
        [TestCase("1 1")]
        [TestCase("2 1")]
        [TestCase("0 0")]
        public void ManySeptByParserWithMinCount_SuccessTest(string input)
        {
            var manyParser = new ManySeptByParser(Parser.Digit, Parser.WhiteSpace, 2);
            var result = manyParser.Run(input);
            Assert.IsFalse(result.IsError);
        }

        [TestCase("13")]
        [TestCase("34")]
        [TestCase("782 2")]
        public void ManySeptByParserWithMinCount_FailureTest(string input)
        {
            var manyParser = new ManySeptByParser(Parser.Digit, Parser.WhiteSpace, 3);
            var result = manyParser.Run(input);
            Assert.IsTrue(result.IsError);
        }

        [TestCase("123,232")]
        [TestCase("982,232,973")]
        [TestCase("000,123,000")]
        public void ManySeptByParserWithMinMaxCount_SuccessTest(string input)
        {
            var manyParser = new ManySeptByParser(Parser.Digit.Many(3, 3), Parser.Char(','), 2, 3);
            var result = manyParser.Run(input);
            Assert.IsFalse(result.IsError);
        }

        [TestCase("1231")]
        [TestCase("1231")]
        [TestCase("225687322")]
        public void ManySeptByParserWithMinMaxCount_FailureTest(string input)
        {
            var manyParser = new ManySeptByParser(Parser.Digit.Many(3, 3), Parser.Char(','), 2, 3);
            var result = manyParser.Run(input);
            Assert.IsTrue(result.IsError);
        }
    }
}
