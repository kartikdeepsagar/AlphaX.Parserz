﻿using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
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
            IParserState errorState = null;
            while (true)
            {
                var state = Parser.Parse(inputState);

                if (state.IsError)
                {
                    errorState = state;
                    break;
                }

                results.Add(state.Result);
                inputState = state;
            }

            var result = results.Count == 0 ? ArrayResult.Empty : new ArrayResult(results.ToArray());

            if (results.Count < MinCount)
            {
                errorState = errorState == null ? inputState : errorState;
                return CreateErrorState(errorState, new ParserError(errorState.Index, string.Format(ParserMessages.UnexpectedInputError, errorState.Index,
                    string.Format(ParserMessages.AtleastCount, MinCount, MinCount > 1 ? "s" : string.Empty),
                    string.Format(ParserMessages.GotCount, results.Count, results.Count > 1 ? "s" : string.Empty))));
            }

            if (MaxCount != -1 && results.Count > MaxCount)
            {
                errorState = errorState == null ? inputState : errorState;
                return CreateErrorState(errorState, new ParserError(errorState.Index, string.Format(ParserMessages.UnexpectedInputError, errorState.Index,
                    string.Format(ParserMessages.AtmostCount, MaxCount, MaxCount > 1 ? "s" : string.Empty),
                    string.Format(ParserMessages.GotCount, results.Count, results.Count > 1 ? "s" : string.Empty))));
            }

            return CreateResultState(inputState, result, inputState.Index);
        }
    }
}
