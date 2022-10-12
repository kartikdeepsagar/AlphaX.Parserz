using AlphaX.Parserz.Parsers;

namespace AlphaX.Parserz.Results
{
    public class ByteResult : ParserResult<byte>
    {
        public ByteResult(byte value) : base(value,  ParserResultType.Number)
        {
            
        }
    }
}
