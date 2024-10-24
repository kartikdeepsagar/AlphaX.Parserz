using System.Text;

namespace AlphaX.Parserz
{
    internal class StringValueParser : Parser<StringResult>
    {
        private char _quoteChar;

        public StringValueParser(bool doubleQuotes, bool allowTrace)
        {
            _quoteChar = doubleQuotes ? '"' : '\'';
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            if (inputState.Input.Length == 0 || inputState.Input[0] != _quoteChar)
                return ParserStates.Error(inputState, new ParserError(inputState.Index, $"Unexpected input. Expected '{_quoteChar}'."));

            var input = inputState.Input;
            var strBuffer = new StringBuilder();
            for (int index = 1; index < input.Length; index++)
            {
                if (input[index] == _quoteChar && (index != input.Length - 1 && input[index + 1] == _quoteChar))
                {
                    strBuffer.Append(input[index]);
                    index++;
                }
                else if (input[index] == _quoteChar && (index == input.Length - 1 || input[index + 1] != _quoteChar))
                {
                    return ParserStates.Result(inputState, new StringResult(strBuffer.ToString()), inputState.Index + index + 1);
                }
                else
                {
                    strBuffer.Append(input[index]);
                }
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index, "Unexpected input. Expected a string value"));
        }
    }
}
