namespace AlphaX.Parserz.Results
{
    public class Int64Result : ParserResult<long>
    {
        public Int64Result(long value) : base(value, ParserResultType.Number)
        {

        }
    }
}
