using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    public class ParserError : IParserError
    {
        public int Index { get; }
        public string Message { get; }

        public ParserError(int index, string message)
        {
            Index = index;
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
