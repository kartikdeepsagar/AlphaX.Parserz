using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System.Text.RegularExpressions;

namespace AlphaX.Parserz.Parsers
{
    public class NumberParser : RegexParser<DoubleResult>
    {
        public NumberParser(string decimalSeperator) :
            base(new Regex(@"^[\+\-]?[\d]*\" + decimalSeperator + @"?[\d]+", RegexOptions.Compiled)) { }

        protected override DoubleResult ConvertResult(string value)
        {
            try
            {
                if (value.Length == 0 || value == null)
                    return null;

                return new DoubleResult(double.Parse(value));
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
    }
}
