﻿using AlphaX.Parserz;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class DecimalParserTests
    {
        private IParser _decimalParser;

        [SetUp]
        public void Setup()
        {
            _decimalParser = Parser.Decimal;
        }

        [TestCase("1.92")]
        [TestCase("29.76")]
        [TestCase("23.1123")]
        [TestCase("323.23")]
        [TestCase("0.00")]
        [TestCase("1.0")]
        public void DecimalParser_Success_Test(string value)
        {
            var resultState = _decimalParser.Run(value);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(DoubleResult), resultState.Result);
        }

        [TestCase("..2")]
        [TestCase("90w.w")]
        [TestCase("009.s2.2")]
        [TestCase("90..178")]
        [TestCase(".0")]
        [TestCase("1.")]
        public void DecimalParser_Failure_Test(string value)
        {
            var resultState = _decimalParser.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}