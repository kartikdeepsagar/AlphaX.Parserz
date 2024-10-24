using System;

namespace AlphaX.Parserz
{
    internal class ChainedParser : Parser<IParserResult>
    {
        public IParser PreviousParser { get; }
        public Func<IParserResult, IParser> NextParserFunc { get; }

        public ChainedParser(IParser previousParser, Func<IParserResult, IParser> nextParserFunc, bool allowTrace)
        {
            PreviousParser = previousParser;
            NextParserFunc = nextParserFunc;
            AllowTrace = allowTrace;       
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var newState = PreviousParser.Parse(inputState);

            if (NextParserFunc == null || newState.IsError)
                return newState;

            var nextParser = NextParserFunc(newState.Result);

            if (nextParser == null)
                return newState;

            return nextParser.Parse(newState);
        }
    }
}
