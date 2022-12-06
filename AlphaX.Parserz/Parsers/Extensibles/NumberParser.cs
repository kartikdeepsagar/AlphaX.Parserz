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
    }
}
