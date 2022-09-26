using AlphaX.Parserz.Interfaces;

namespace AlphaX.Parserz.Parsers
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

        /// <summary>
        /// Creates a new error state.
        /// </summary>
        /// <param name="currentState">The current state</param>
        /// <param name="error">Parser error</param>
        /// <returns></returns>
        protected virtual IParserState CreateErrorState(IParserState currentState, IParserError error)
        {
            var errorState = currentState.Clone();
            errorState.Error = error;
            return errorState;
        }

        /// <summary>
        /// Creates a new result state.
        /// </summary>
        /// <param name="currentState">Input state</param>
        /// <param name="result">Result of the state</param>
        /// <param name="index">New index of the state</param>
        /// <returns></returns>
        protected virtual IParserState CreateResultState(IParserState currentState, T result, int index)
        {
            var resultState = currentState.Clone();
            resultState.Index = index;
            resultState.Result = result;
            return resultState;
        }
    }
}
