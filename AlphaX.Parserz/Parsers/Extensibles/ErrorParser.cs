namespace AlphaX.Parserz
{
    internal class ErrorParser : Parser<IParserResult>
    {
        private IParserError _error;

        public ErrorParser(IParserError error, bool allowTrace)
        {
            _error = error;
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            return ParserStates.Error(inputState, _error);
        }
    }
}
