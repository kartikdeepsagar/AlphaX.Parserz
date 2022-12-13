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
            IParserState state = null;
            for (int parserIndex = 0; parserIndex < Parsers.Count; parserIndex++)
            {
                state = Parsers[parserIndex].Parse(inputState);

                if (!state.IsError)
                    return state;
            }

            if (state == null)
                state = inputState;

            return CreateErrorState(inputState, new ParserError(state.Index,
                string.Format(ParserMessages.UnexpectedInputError, state.Index, ParserMessages.AtleastOneParserMatch, ParserMessages.NoParserMatch)));
        }
    }
}
