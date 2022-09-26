using AlphaX.Parserz.Extensions;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;

namespace AlphaX.Parserz
{
    public class LetterParser : Parser<CharResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return CreateErrorState(inputState, new ParserError(inputState.Index, 
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Letters, targetString)));

            var character = targetString[0];

            if (char.IsLetter(character))
                return CreateResultState(inputState, new CharResult(character), inputState.Index + 1);

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Letters, character)));
        }
    }
}
