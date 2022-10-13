using AlphaX.Parserz.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.Parserz.Results
{
    public class ArrayResult : ParserResult<IParserResult[]>, IEnumerable<IParserResult>
    {
        public ArrayResult(IParserResult[] value) : base(value, ParserResultType.Array)
        {
            
        }

        public IEnumerator<IParserResult> GetEnumerator()
        {
            return Value.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
