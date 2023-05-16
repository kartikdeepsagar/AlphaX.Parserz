namespace AlphaX.Parserz
{
    /// <summary>
    /// Class that represents a parser of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Parser<T> : IParser<T> where T : IParserResult
    {
        public IParserState Parse(IParserState inputState)
        {
            if (inputState.IsError)
                return inputState;

            return ParseInput(inputState);
        }

        public IParserState Run(string input)
        {
            return Parse(new ParserState()
            {
                Index = 0,
                ActualInput = input
            });
        }

        /// <summary>
        /// Takes an input state and parses it to new output state.
        /// Note: This method will only get called if the input state doesn't have any error.
        /// </summary>
        /// <param name="inputState">The input state</param>
        /// <returns></returns>
        protected abstract IParserState ParseInput(IParserState inputState);
    }
}
