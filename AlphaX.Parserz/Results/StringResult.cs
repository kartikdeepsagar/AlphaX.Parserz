namespace AlphaX.Parserz.Results
{
    public class StringResult : ParserResult<string>
    {
        public static StringResult Invalid = new StringResult();

        public StringResult(string value) : base(value, ParserResultType.String)
        {
            
        }

        public StringResult() : base()
        {

        }
    }
}