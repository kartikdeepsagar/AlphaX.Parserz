using System;

namespace AlphaX.Parserz
{
    internal class ResultMappedParser<TIn, TOut> : Parser<TOut>
        where TOut : IParserResult
        where TIn : IParserResult
    {
        public IParser Parser { get; }
        public Func<TIn, TOut> ResultMap { get; set; }

        public ResultMappedParser(IParser parser, Func<TIn, TOut> resultMap)
        {
            Parser = parser;
            ResultMap = resultMap;
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
