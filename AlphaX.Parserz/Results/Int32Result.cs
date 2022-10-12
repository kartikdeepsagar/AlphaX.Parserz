﻿using AlphaX.Parserz.Parsers;

namespace AlphaX.Parserz.Results
{
    public class Int32Result : ParserResult<int>
    {
        public Int32Result(int value) : base(value, ParserResultType.Number)
        {
            
        }
    }
}
