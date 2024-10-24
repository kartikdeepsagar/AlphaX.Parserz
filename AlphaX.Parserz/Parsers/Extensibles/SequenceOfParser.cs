using System.Collections.Generic;

namespace AlphaX.Parserz
{
    internal class SequenceOfParser : Parser<ArrayResult>
    {
        internal List<IParser> Parsers { get; }

        public SequenceOfParser(IParser[] parsers, bool allowTrace)
        {
            Parsers = new List<IParser>(parsers);
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var results = new List<IParserResult>();

            for (int parserIndex = 0; parserIndex < Parsers.Count; parserIndex++)
            {
                inputState = Parsers[parserIndex].Parse(inputState);

                if (inputState.IsError)
                    return ParserStates.Error(inputState, inputState.Error);

                results.Add(inputState.Result);
            }

            var result = results.Count == 0 ? ArrayResult.Empty : new ArrayResult(results.ToArray());
            return ParserStates.Result(inputState, result, inputState.Index);
        }
    }
}
