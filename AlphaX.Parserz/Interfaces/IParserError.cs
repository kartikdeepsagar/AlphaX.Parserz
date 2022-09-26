namespace AlphaX.Parserz.Interfaces
{
    /// <summary>
    /// Interface that represents parser error.
    /// </summary>
    public interface IParserError
    {
        /// <summary>
        /// Index of the input where parsing failed.
        /// </summary>
        int Index { get; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        string Message { get; }
    }
}
