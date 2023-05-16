namespace AlphaX.Parserz
{
    public class ObjectResult : ParserResult<object>
    {
        public static ObjectResult Invalid = new ObjectResult();

        public ObjectResult(object value) : base(value, ParserResultType.Object)
        {

        }

        public ObjectResult() : base()
        {

        }
    }
}
