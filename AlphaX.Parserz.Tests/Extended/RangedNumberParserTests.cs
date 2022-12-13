using AlphaX.Parserz.Extended;
using AlphaX.Parserz.Results;
using NUnit.Framework;
using System;

namespace AlphaX.Parserz.Tests.Extended
{
    public class HumanTimeSpanParserTests
    {
        [TestCase("1w 3d 4h 3m", 1, 3, 4, 3)]
        [TestCase("10w 4d", 10, 4, null, null)]
        [TestCase("1w 4m", 1, null, null, 4)]
        [TestCase("4d 1m", null, 4, null, 1)]
        [TestCase("1d 2h", null, 1, 2, null)]
        [TestCase("12w", 12, null, null, null)]
        [TestCase("2w 1d 2h 5m", 2, 1, 2, 5)]
        public void HumanTimeSpanParserTests_Success_Test(string value, double? weeks, double? days, double? hours, double? minutes)
        {
            var resultState = BuiltInParser.HumanTimeSpanParser.Run(value);
            Assert.AreEqual(resultState.Result.Type, HumanTimeSpanParserResult.HumanTimeSpan);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(HumanTimeSpanParserResult), resultState.Result);

            var result = resultState.Result as HumanTimeSpanParserResult;
            Assert.AreEqual(result.Value.Weeks, weeks);
            Assert.AreEqual(result.Value.Days, days);
            Assert.AreEqual(result.Value.Hours, hours);
            Assert.AreEqual(result.Value.Minutes, minutes);
        }

        [TestCase("4m 1w")]
        [TestCase("5m 1h")]
        [TestCase("4d1w")]
        [TestCase("wd")]
        [TestCase("w0m")]
        [TestCase("1d 1m 1w")]
        public void HumanTimeSpanParserTests_Failure_Test(string value)
        {
            var resultState = BuiltInParser.HumanTimeSpanParser.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }

    public class RangedNumberParserTests
    {
        [TestCase("2", 1, 10)]
        [TestCase("7", 5, 10)]
        [TestCase("10", 10, 10)]
        [TestCase("15", 10, 20)]
        [TestCase("11", 1, 20)]
        [TestCase("2.42", 1.5, 2.5)]
        public void RangedNumberParser_Success_Test(string value, double min, double max)
        {
            var resultState = BuiltInParser.RangedNumberParser(min, max).Run(value);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Number);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(DoubleResult), resultState.Result);
        }

        [TestCase("232", 1, 10)]
        [TestCase("12", 5, 10)]
        [TestCase("231", 10, 10)]
        [TestCase("23", 10, 20)]
        [TestCase("099", 1, 20)]
        [TestCase("2.42", 2.5, 3.0)]
        public void RangedNumberParser_Failure_Test(string value, double min, double max)
        {
            var resultState = BuiltInParser.RangedNumberParser(min, max).Run(value);
            Assert.IsTrue(resultState.IsError);
        }

        [TestCase("232", 1000, 10)]
        [TestCase("232", 1, 0)]
        public void RangedNumberParser_Exception_Test(string value, double min, double max)
        {
            Assert.Throws<InvalidOperationException>(() => BuiltInParser.RangedNumberParser(min, max).Run(value));
        }
    }
}
