using System;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;

namespace AlphaX.Parserz
{
    public class UntilFoundParser : Parser<StringResult>
    {
        private string _selector;
        private bool _matchCase;

        public UntilFoundParser(string selector, bool matchCase = false)
        {
            if(selector == null) 
                throw new ArgumentNullException(nameof(selector));

            _selector = selector;
            _matchCase = matchCase;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;
            var index = targetString.IndexOf(_selector, _matchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase);

            if(index >= 0)
            {
                return CreateResultState(inputState, new StringResult(_selector), index + _selector.Length);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index,
               string.Format(ParserMessages.UnexpectedInputError, inputState.Index, _selector, targetString)));
        }
    }
}
