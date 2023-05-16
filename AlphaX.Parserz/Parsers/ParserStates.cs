namespace AlphaX.Parserz
{
    public static class ParserStates
    {
        /// <summary>
        /// Creates a new error state.
        /// </summary>
        /// <param name="inputState">The current state</param>
        /// <param name="error">Parser error</param>
        /// <returns></returns>
        public static IParserState Error(IParserState inputState, IParserError error)
        {
            var errorState = inputState.Clone();
            errorState.Error = error;
            errorState.Result = null;
            return errorState;
        }

        /// <summary>
        /// Creates a new result state.
        /// </summary>
        /// <param name="inputState">Input state</param>
        /// <param name="result">Result of the state</param>
        /// <param name="index">New index of the state</param>
        /// <returns></returns>
        public static IParserState Result(IParserState inputState, IParserResult result, int index)
        {
            var resultState = inputState.Clone();
            resultState.Index = index;
            resultState.Result = result;
            return resultState;
        }
    }
}
