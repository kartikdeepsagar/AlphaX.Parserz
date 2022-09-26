using AlphaX.Parserz.Interfaces;
using System.Diagnostics;

namespace AlphaX.Parserz.Results
{
    public abstract class ParserResult<T> : IParserResult<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object _value;
        public T Value => (T)_value;
        object IParserResult.Value => _value;

        public ParserResult(T value)
        {
            _value = value;
        }
    }
}
