using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System;

namespace AlphaX.Parserz.Parsers
{
    public class DigitParser : Parser<ByteResult>
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
                return CreateResultState(inputState, new ByteResult(Convert.ToByte(character - '0')), inputState.Index + 1);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));
        }
    }
}
