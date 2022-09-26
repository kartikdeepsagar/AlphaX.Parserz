namespace AlphaX.Parserz.Interfaces
{
    /// <summary>
    /// Interface that represents a parser.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Runs the parser for input string.
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>A success/error parser state</returns>
        IParserState Run(string input);
        /// <summary>
        /// Parses an input state to a new success/error state.
        /// </summary>
        /// <param name="inputState">The input state to parse</param>
        /// <returns></returns>
        IParserState Parse(IParserState inputState);
    }

    /// <summary>
    /// Interface that represents a generic parser.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<T> : IParser where T : IParserResult
    {

    }
}
