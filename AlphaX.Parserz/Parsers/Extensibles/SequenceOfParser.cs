using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using System;
using System.Collections.Generic;

namespace AlphaX.Parserz.Parsers
{
    public class SequenceOfParser : Parser<ArrayResult>
    {
        internal List<IParser> Parsers { get; }

        public SequenceOfParser(params IParser[] parsers)
        {
            Parsers = new List<IParser>(parsers);
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var results = new List<IParserResult>();

            for (int parserIndex = 0; parserIndex < Parsers.Count; parserIndex++)
            {
                inputState = Parsers[parserIndex].Parse(inputState);

                if (inputState.IsError)
                    return CreateErrorState(inputState, inputState.Error);

                results.Add(inputState.Result);
            }

            return CreateResultState(inputState, new ArrayResult(results.ToArray()), inputState.Index);
        }
    }
}
