using System;

namespace AlphaX.Parserz
{
    internal class ErrorMappedParser<T> : Parser<T> where T : IParserResult
    {
        public IParser Parser { get; }
        public Func<IParserError, IParserError> ErrorMap { get; }

        public ErrorMappedParser(IParser parser, Func<IParserError, IParserError> errorMap, bool allowTrace)
        {
            Parser = parser;
            ErrorMap = errorMap;
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var newState = Parser.Parse(inputState);

            if (newState.IsError)
            {
                return ParserStates.Error(newState, ErrorMap(newState.Error));
            }

            return newState;
        }
    }
}
