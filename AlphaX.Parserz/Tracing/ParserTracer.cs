using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.Parserz.Tracing
{
    public static class ParserTracer
    {
        internal static ConcurrentStack<Trace> _trace;

        public static bool Enabled {  get; set; }

        static ParserTracer()
        {
            _trace = new ConcurrentStack<Trace>();
            Enabled = false;
        }

        public static void Trace(IParser parser, IParserState state, bool isResult)
        {
            _trace.Push(new Trace(parser, state, isResult));
        }

        public static IEnumerable<Trace> GetTraces()
        {
            return _trace.AsEnumerable().Reverse();
        }

        public static void Reset()
        {
            _trace.Clear();
        }
    }

    public class Trace : IDisposable
    {
        public string Parser { get; }
        public bool IsInput { get; }
        public IParserState State { get; private set; }

        public Trace(IParser parser, IParserState state, bool isResult)
        {
            Parser = parser.GetType().Name;
            State = state.Clone();
            IsInput = !isResult;
        }

        public void Dispose()
        {
            State = null;
        }
    }

}
