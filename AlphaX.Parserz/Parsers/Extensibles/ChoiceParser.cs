using System.Collections.Generic;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class ChoiceParser : Parser<IParserResult>
    {
        internal List<IParser> Parsers { get; }

        public ChoiceParser(IParser[] parsers, bool allowTrace)
        {
            Parsers = new List<IParser>(parsers);
            AllowTrace = allowTrace;
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

            return ParserStates.Error(inputState, new ParserError(state.Index,
                string.Format(ParserMessages.UnexpectedInputError, state.Index, ParserMessages.AtleastOneParserMatch, ParserMessages.NoParserMatch)));
        }
    }
}
