using AlphaX.Parserz.Interfaces;
using System.Text.RegularExpressions;

namespace AlphaX.Parserz
{
    public abstract class RegexParser<T> : Parser<T> where T : IParserResult
    {
        public Regex Regex { get; }

        public RegexParser(Regex regex)
        {
            Regex = regex;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var targetString = inputState.Input;
            var match = Regex.Match(targetString);

            if (!match.Success)
            {
                return CreateErrorState(inputState, CreateError(match.Index, inputState.Input));
            }

            var result = ConvertResult(match);

            if (result != null && result.IsValid)
                return CreateResultState(inputState, result, inputState.Index + match.Length);

            return CreateErrorState(inputState, CreateError(match.Index, inputState.Input));
        }

        protected abstract T ConvertResult(Match value);

        protected abstract IParserError CreateError(int index, string value);
    }
}
