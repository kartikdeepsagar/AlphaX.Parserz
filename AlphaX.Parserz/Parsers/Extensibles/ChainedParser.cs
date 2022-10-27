using AlphaX.Parserz.Interfaces;
using System;

namespace AlphaX.Parserz
{
    public class ChainedParser : Parser<IParserResult>
    {
        public IParser PreviousParser { get; }
        public Func<IParserResult, IParser> NextParserFunc { get; }

        public ChainedParser(IParser previousParser, Func<IParserResult, IParser> nextParserFunc)
        {
            PreviousParser = previousParser;
            NextParserFunc = nextParserFunc;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var newState = PreviousParser.Parse(inputState);

            if (NextParserFunc == null)
                return newState;

            var nextParser = NextParserFunc(newState.Result);

            if (nextParser == null)
                return newState;

            return nextParser.Parse(newState);
        }
    }
}
