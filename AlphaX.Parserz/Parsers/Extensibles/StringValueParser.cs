using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using System.Text;

namespace AlphaX.Parserz
{
    public class StringValueParser : Parser<StringResult>
    {
        private char _quoteChar;

        public StringValueParser(bool doubleQuotes = true)
        {
            _quoteChar = doubleQuotes ? '"' : '\'';
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            if (inputState.Input.Length == 0 || inputState.Input[0] != _quoteChar)
                return CreateErrorState(inputState, new ParserError(inputState.Index, $"Unexpected input. Expected '{_quoteChar}'."));

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
                    return CreateResultState(inputState, new StringResult(strBuffer.ToString()), inputState.Index + index + 1);
                }
                else
                {
                    strBuffer.Append(input[index]);
                }
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, "Unexpected input. Expected a string value"));
        }
    }
}
