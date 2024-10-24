using AlphaX.Parserz.Tracing;

namespace AlphaX.Parserz
{
    /// <summary>
    /// Class that represents a parser of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Parser<T> : IParser<T> where T : IParserResult
    {
        internal bool AllowTrace { get; set; }

        protected Parser()
        {
            AllowTrace = false;
        }

        public IParserState Parse(IParserState inputState)
        {
            if (ParserTracer.Enabled && AllowTrace)
            {
                ParserTracer.Trace(this, inputState, false);
            }

            if (inputState.IsError)
            {
                return inputState;
            }

            IParserState resultState = ParseInput(inputState);

            if (ParserTracer.Enabled && AllowTrace)
            {
                ParserTracer.Trace(this, resultState, true);
            }

            return resultState;
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

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
