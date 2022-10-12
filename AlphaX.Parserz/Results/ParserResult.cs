using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Parsers;
using System.Diagnostics;

namespace AlphaX.Parserz.Results
{
    public abstract class ParserResult<T> : IParserResult<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object _value;
        public T Value => (T)_value;
        object IParserResult.Value => _value;

        public ParserResultType Type { get; }

        public ParserResult(T value, ParserResultType resultType)
        {
            _value = value;
            Type = resultType;
        }
    }
}
