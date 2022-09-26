using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System;
using System.Collections.Generic;

namespace AlphaX.Parserz
{
    public class ManyParser : Parser<ArrayResult>
    {
        public int MinCount { get; }
        public int MaxCount { get; }
        public IParser Parser { get; }

        public ManyParser(IParser parser, int minCount = 0, int maxCount = -1)
        {
            Parser = parser;
            MinCount = minCount;
            MaxCount = maxCount;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            List<IParserResult> results = new List<IParserResult>();

            while(true)
            {
                var state = Parser.Parse(inputState);
                if (!state.IsError)
                {
                    results.Add(state.Result);
                    inputState = state;
                }
                else
                {
                    break;
                }
            }

            var result = new ArrayResult(results.ToArray());

            if (results.Count < MinCount)
            {
                inputState.Result = result;
                return CreateErrorState(inputState, new ParserError(inputState.Index, string.Format(ParserMessages.UnexpectedInputError, inputState.Index,
                    string.Format(ParserMessages.AtleastCount, MinCount, MinCount > 1 ? "s" : string.Empty), 
                    string.Format(ParserMessages.GotCount, results.Count, results.Count > 1 ? "s" : string.Empty))));
            }

            if (MaxCount != -1 && results.Count > MaxCount)
            {
                inputState.Result = result;
                return CreateErrorState(inputState, new ParserError(inputState.Index, string.Format(ParserMessages.UnexpectedInputError, inputState.Index,
                    string.Format(ParserMessages.AtmostCount, MaxCount, MaxCount > 1 ? "s" : string.Empty),
                    string.Format(ParserMessages.GotCount, results.Count, results.Count > 1 ? "s" : string.Empty))));
            }

            return CreateResultState(inputState, result, inputState.Index);
        }
    }
}
