using AlphaX.Parserz.Interfaces;

namespace AlphaX.Parserz
{
    public class ParserState : IParserState
    {
        public int Index { get; set; }
        public string ActualInput { get; set; }
        public IParserResult Result { get; set; }
        public bool IsError => Error != null;
        public IParserError Error { get; set; }
        public string Input => ActualInput?.Substring(Index);

        public IParserState Clone()
        {
            return new ParserState()
            {
                Index = this.Index,
                ActualInput = this.ActualInput,
                Result = this.Result,
                Error = Error
            };
        }
    }
}
