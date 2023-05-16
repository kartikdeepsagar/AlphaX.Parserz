using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    public enum ParseMode
    {
        Both,
        UpperCase,
        LowerCase
    }

    internal class LetterParser : Parser<CharResult>
    {
        private ParseMode _mode;

        public LetterParser(ParseMode mode = ParseMode.Both)
        {
            _mode = mode;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return ParserStates.Error(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Letters, targetString)));

            var character = targetString[0];

            if (_mode == ParseMode.Both && char.IsLetter(character))
                return ParserStates.Result(inputState, new CharResult(character), inputState.Index + 1);
            else if (_mode == ParseMode.LowerCase && char.IsLetter(character) && char.IsLower(character))
                return ParserStates.Result(inputState, new CharResult(character), inputState.Index + 1);
            else if (_mode == ParseMode.UpperCase && char.IsLetter(character) && char.IsUpper(character))
                return ParserStates.Result(inputState, new CharResult(character), inputState.Index + 1);

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Letters, character)));
        }
    }
}
