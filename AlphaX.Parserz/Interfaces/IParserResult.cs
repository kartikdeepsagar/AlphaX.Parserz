namespace AlphaX.Parserz
{
    /// <summary>
    /// Interface that represents a parser result.
    /// </summary>
    public interface IParserResult
    {
        bool IsValid { get; }
        object Value { get; }
        /// <summary>
        /// Gets the result type
        /// </summary>
        ParserResultType Type { get; }
    }

    /// <summary>
    /// Interface that represents a parser result of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParserResult<T> : IParserResult
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        new T Value { get; }
    }
}
