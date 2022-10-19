namespace AlphaX.Parserz.Results
{
    public class DoubleResult : ParserResult<double>
    {
        public static DoubleResult Invalid = new DoubleResult();

        public DoubleResult(double value) : base(value, ParserResultType.Number)
        {
            
        }

        private DoubleResult() : base()
        {

        }
    }
}
