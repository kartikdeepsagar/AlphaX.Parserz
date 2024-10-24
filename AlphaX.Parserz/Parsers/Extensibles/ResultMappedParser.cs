using System;

namespace AlphaX.Parserz
{
    internal class ResultMappedParser<TIn, TOut> : Parser<TOut>
        where TOut : IParserResult
        where TIn : IParserResult
    {
        public IParser Parser { get; }
        public Func<TIn, TOut> ResultMap { get; set; }

        public ResultMappedParser(IParser parser, Func<TIn, TOut> resultMap, bool allowTrace)
        {
            Parser = parser;
            ResultMap = resultMap;
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var newState = Parser.Parse(inputState);

            if (!newState.IsError)
            {
                return ParserStates.Result(newState, ResultMap((TIn)newState.Result), newState.Index);
            }

            return newState;
        }
    }
}
