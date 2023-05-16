using NUnit.Framework;

namespace AlphaX.Parserz.Tests
{
    public class BooleanParserTests
    {
        [TestCase("true")]
        [TestCase("True")]
        [TestCase("TRUE")]
        [TestCase("false")]
        [TestCase("False")]
        [TestCase("FALSE")]
        public void BooleanParser_Success_Test(string value)
        {
            var resultState = Parser.Boolean.Run(value);
            Assert.AreEqual(resultState.Result.Type, ParserResultType.Boolean);
            Assert.IsFalse(resultState.IsError);
            Assert.IsInstanceOf(typeof(BooleanResult), resultState.Result);
        }

        [TestCase("tru")]
        [TestCase("fal se")]
        [TestCase("ture")]
        [TestCase("lfase")]
        [TestCase(" true 1")]
        public void BooleanParser_Failure_Test(string value)
        {
            var resultState = Parser.Boolean.Run(value);
            Assert.IsTrue(resultState.IsError);
        }
    }
}
