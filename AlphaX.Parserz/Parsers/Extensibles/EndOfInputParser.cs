using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    public class EndOfInputParser : Parser<IParserResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;

            if (targetString == string.Empty)
            {
                return CreateResultState(inputState, inputState.Result, inputState.Index);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.EndofInput, targetString)));
        }
    }
}
