using System.Diagnostics;

namespace AlphaX.Parserz
{
    public abstract class ParserResult<T> : IParserResult<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object _value;
        public T Value => (T)_value;
        object IParserResult.Value => _value;

        public ParserResultType Type { get; }
        public bool IsValid { get; }

        public ParserResult(T value, ParserResultType resultType)
        {
            _value = value;
            Type = resultType;
            IsValid = true;
        }

        public ParserResult()
        {
            IsValid = false;
            _value = null;
            Type = null;
        }
    }
}
