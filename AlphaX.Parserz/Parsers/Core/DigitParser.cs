using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;

namespace AlphaX.Parserz
{
    public class DigitParser : Parser<DoubleResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return CreateErrorState(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));

            var character = targetString[0];
            if (char.IsDigit(character))
            {
                return CreateResultState(inputState, new DoubleResult(character - '0'), inputState.Index + 1);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));
        }
    }
}
