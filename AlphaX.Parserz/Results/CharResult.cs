namespace AlphaX.Parserz.Results
{
    public class CharResult : ParserResult<char>
    {
        public static CharResult Invalid = new CharResult();

        public CharResult(char value) : base(value, ParserResultType.Char)
        {

        }

        public CharResult() : base()
        {

        }
    }
}
