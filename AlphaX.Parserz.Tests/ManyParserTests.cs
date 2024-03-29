﻿using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class ManyParserTests
    {
        [TestCase("12")]
        [TestCase("00")]
        [TestCase("32423")]
        public void ManyParserWithMinCount_SuccessTest(string input)
        {
            var manyParser = Parser.Digit.Many(2);
            var result = manyParser.Run(input);
            Assert.IsFalse(result.IsError);
        }


        [TestCase("1")]
        [TestCase("34")]
        [TestCase("782")]
        public void ManyParserWithMinCount_FailureTest(string input)
        {
            var manyParser = Parser.Digit.Many(4);
            var result = manyParser.Run(input);
            Assert.IsTrue(result.IsError);
        }

        [TestCase("122")]
        [TestCase("1234")]
        [TestCase("98")]
        public void ManyParserWithMinMaxCount_SuccessTest(string input)
        {
            var manyParser = Parser.Digit.Many(2, 4);
            var result = manyParser.Run(input);
            Assert.IsFalse(result.IsError);
        }

        [TestCase("123121")]
        [TestCase("1")]
        [TestCase("22322")]
        public void ManyParserWithMinMaxCount_FailureTest(string input)
        {
            var manyParser = Parser.Digit.Many(2, 4);
            var result = manyParser.Run(input);
            Assert.IsTrue(result.IsError);
        }
    }
}
