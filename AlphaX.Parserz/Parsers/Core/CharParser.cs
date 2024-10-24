using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class CharParser : Parser<CharResult>
    {
        public char Value { get; }

        public CharParser(char value)
        {
            Value = value;
            AllowTrace = true;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return ParserStates.Error(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, Value.ToString(), targetString)));

            var character = targetString[0];

            if (character == Value)
            {
                return ParserStates.Result(inputState, new CharResult(Value), inputState.Index + 1);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, Value.ToString(), character)));
        }

        public override string ToString()
        {
            return $"{nameof(CharParser)}('{Value}')";
        }
    }
}
