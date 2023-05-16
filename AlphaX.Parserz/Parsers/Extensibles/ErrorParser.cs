namespace AlphaX.Parserz
{
    internal class ErrorParser : Parser<IParserResult>
    {
        private IParserError _error;

        public ErrorParser(IParserError error)
        {
            _error = error;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            return ParserStates.Error(inputState, _error);
        }
    }
}
