using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class DigitParser : Parser<DoubleResult>
    {
        public DigitParser()
        {
            AllowTrace = true;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return ParserStates.Error(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));

            var character = targetString[0];
            if (char.IsDigit(character))
            {
                return ParserStates.Result(inputState, new DoubleResult(character - '0'), inputState.Index + 1);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));
        }
    }
}
