using System;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class StringParser : Parser<StringResult>
    {
        public string Value { get; }
        public bool MatchCase { get; }

        public StringParser(string value, bool matchCase = false)
        {
            Value = value;
            MatchCase = matchCase;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (Value.Length <= targetString.Length && targetString.StartsWith(Value, MatchCase ? StringComparison.Ordinal
                : StringComparison.InvariantCultureIgnoreCase))
            {
                return ParserStates.Result(inputState, new StringResult(Value), inputState.Index + Value.Length);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, Value, targetString)));
        }
    }
}
