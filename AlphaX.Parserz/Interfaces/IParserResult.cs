namespace AlphaX.Parserz.Interfaces
{
    /// <summary>
    /// Interface that represents a parser result.
    /// </summary>
    public interface IParserResult
    {
        object Value { get; }
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
