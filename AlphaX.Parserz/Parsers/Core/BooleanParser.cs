using System;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class BooleanParser : Parser<BooleanResult>
    {
        private const string TRUE = "true";
        private const string FALSE = "false";

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (TRUE.Length <= targetString.Length && targetString.StartsWith(TRUE, StringComparison.InvariantCultureIgnoreCase))
            {
                return ParserStates.Result(inputState, new BooleanResult(true), inputState.Index + TRUE.Length);
            }

            if (FALSE.Length <= targetString.Length && targetString.StartsWith(FALSE, StringComparison.InvariantCultureIgnoreCase))
            {
                return ParserStates.Result(inputState, new BooleanResult(false), inputState.Index + FALSE.Length);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, $"{TRUE}/{FALSE}", targetString)));
        }
    }
}
