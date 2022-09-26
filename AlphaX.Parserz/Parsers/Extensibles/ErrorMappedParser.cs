using AlphaX.Parserz.Interfaces;
using System;

namespace AlphaX.Parserz
{
    public class ErrorMappedParser<T> : Parser<T> where T : IParserResult
    {
        public IParser Parser { get; }
        public Func<IParserError, IParserError> ErrorMap { get; }

        public ErrorMappedParser(IParser parser, Func<IParserError, IParserError> errorMap)
        {
            Parser = parser;
            ErrorMap = errorMap;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var newState = Parser.Parse(inputState);

            if (newState.IsError)
            {
                return CreateErrorState(newState, ErrorMap(newState.Error));
            }

            return newState;
        }
    }
}
