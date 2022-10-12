using AlphaX.Parserz.Interfaces;
using System;

namespace AlphaX.Parserz
{
    public class LazyParser : Parser<IParserResult>
    {
        private Lazy<IParser> _parser;

        public LazyParser(Func<IParser> parser)
        {
            _parser = new Lazy<IParser>(parser);
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            return _parser.Value.Parse(inputState);
        }
    }
}
