using System.Collections.Generic;
using System.Linq;

namespace AlphaX.Parserz.Tracing
{
    public static class ParserTracer
    {
        internal static List<string> _trace;

        internal const string BEGIN_TRACE_FORMAT = "{0} > Parsing '{1}' at index '{2}'";
        internal const string RESULT_TRACE_FORMAT = "{0} > Parsed & left with '{1}'";
        internal const string ERROR_TRACE_FORMAT = "{0} > Parse Failed. {1}";

        public static bool Enabled {  get; set; }

        static ParserTracer()
        {
            _trace = new List<string>();
            Enabled = false;
        }

        public static void Trace(IParser parser, IParserState state, bool isResult)
        {
            if (state.IsError)
            {
                _trace.Add(string.Format(ERROR_TRACE_FORMAT,
                    parser.GetType().Name,
                    state.Error
                ));
            }
            else if(isResult)
            {
                _trace.Add(string.Format(RESULT_TRACE_FORMAT,
                   parser,
                   state.Input
                ));
            }
            else
            {
                _trace.Add(string.Format(BEGIN_TRACE_FORMAT,
                   parser,
                   state.ActualInput,
                   state.Index
                ));
            }    
        }

        public static IEnumerable<string> GetTrace()
        {
            return _trace.AsEnumerable();
        }

        public static void Reset()
        {
            _trace.Clear();
        }
    }
}
