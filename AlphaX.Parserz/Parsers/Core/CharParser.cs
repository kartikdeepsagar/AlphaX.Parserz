using AlphaX.Parserz.Extensions;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System;

namespace AlphaX.Parserz
{
    public class CharParser : Parser<CharResult>
    {
        public char Value { get; }

        public CharParser(char value)
        {
            Value = value;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return CreateErrorState(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, Value.ToString(), targetString)));

            var character = targetString[0];

            if (character == Value)
            {
                return CreateResultState(inputState, new CharResult(Value), inputState.Index + 1);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, Value.ToString(), character)));
        }
    }
}
