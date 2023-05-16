using System.Text.RegularExpressions;
using System.Threading;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz.Parsers
{
    internal class NumberParser : RegexParser<DoubleResult>
    {
        public NumberParser(bool canParseDecimal = true) :
            base(GetRegex(canParseDecimal))
        { }

        protected override DoubleResult ConvertResult(Match value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Value))
                    return null;

                return new DoubleResult(double.Parse(value.Value));
            }
            catch
            {
                return null;
            }
        }

        protected override IParserError CreateError(int index, string value)
        {
            return new ParserError(index, string.Format(ParserMessages.UnexpectedInputError, index, "number", value));
        }

        private static Regex GetRegex(bool canParseDecimal)
        {
            if (canParseDecimal)
                return new Regex(@"^[\+\-]?[\d]*\" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + @"?[\d]+", RegexOptions.Compiled);
            else
                return new Regex(@"^[\+\-]?[\d]+", RegexOptions.Compiled);
        }
    }
}
