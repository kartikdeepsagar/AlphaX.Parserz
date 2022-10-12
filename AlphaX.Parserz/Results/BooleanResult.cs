using AlphaX.Parserz.Parsers;

namespace AlphaX.Parserz.Results
{
    public class BooleanResult : ParserResult<bool>
    {
        public BooleanResult(bool value) : base(value, ParserResultType.Boolean)
        {
            
        }
    }
}
