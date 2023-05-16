using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class EndOfInputParser : Parser<IParserResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (targetString == string.Empty)
            {
                return ParserStates.Result(inputState, inputState.Result, inputState.Index);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.EndofInput, targetString)));
        }
    }
}
