using System;
using AlphaX.Parserz.Resources;

namespace AlphaX.Parserz
{
    internal class UntilFoundParser : Parser<StringResult>
    {
        private string _selector;
        private bool _matchCase;

        public UntilFoundParser(string selector, bool matchCase, bool allowTrace)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            _selector = selector;
            _matchCase = matchCase;
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;
            var index = targetString.IndexOf(_selector, _matchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase);

            if (index >= 0)
            {
                return ParserStates.Result(inputState, new StringResult(targetString.Substring(index)), index + _selector.Length);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
               string.Format(ParserMessages.UnexpectedInputError, inputState.Index, _selector, targetString)));
        }
    }
}
