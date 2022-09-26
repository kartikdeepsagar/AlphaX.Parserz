using AlphaX.Parserz.Extensions;
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

            if (match.Success)
                return CreateResultState(inputState, ConvertResult(match.Value), inputState.Index + match.Length);

            return CreateErrorState(inputState, CreateError(match.Index, targetString));
        }

        protected abstract T ConvertResult(string value);

        protected abstract IParserError CreateError(int index, string value);
    }
}
