using AlphaX.Parserz.Parsers;

namespace AlphaX.Parserz.Results
{
    public class CharResult : ParserResult<char>
    {
        public CharResult(char value) : base(value, ParserResultType.Char)
        {

        }
    }
}
