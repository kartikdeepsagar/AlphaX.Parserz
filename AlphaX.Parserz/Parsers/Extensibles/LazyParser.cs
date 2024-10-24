using System;

namespace AlphaX.Parserz
{
    internal class LazyParser : Parser<IParserResult>
    {
        private Lazy<IParser> _parser;

        public LazyParser(Func<IParser> parser, bool allowTrace)
        {
            _parser = new Lazy<IParser>(parser);
            AllowTrace = allowTrace;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            return _parser.Value.Parse(inputState);
        }
    }
}
