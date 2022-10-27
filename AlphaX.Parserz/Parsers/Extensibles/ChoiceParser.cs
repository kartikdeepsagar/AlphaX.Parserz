using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using System.Collections.Generic;

namespace AlphaX.Parserz
{
    public class ChoiceParser : Parser<IParserResult>
    {
        internal List<IParser> Parsers { get; }

        public ChoiceParser(params IParser[] parsers)
        {
            Parsers = new List<IParser>(parsers);
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            for(int parserIndex = 0; parserIndex < Parsers.Count; parserIndex++)
            {
                var state = Parsers[parserIndex].Parse(inputState);

                if (!state.IsError)
                    return state;
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.AtleastOneParserMatch, ParserMessages.NoParserMatch)));
        }
    }
}
