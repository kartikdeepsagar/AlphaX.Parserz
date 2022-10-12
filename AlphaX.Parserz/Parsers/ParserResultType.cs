using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaX.Parserz.Parsers
{
    public class ParserResultType
    {
        public static ParserResultType Number = new ParserResultType("Number");
        public static ParserResultType Decimal = new ParserResultType("Decimal");
        public static ParserResultType String = new ParserResultType("String");
        public static ParserResultType Boolean = new ParserResultType("Boolean");
        public static ParserResultType Array = new ParserResultType("Array");
        public static ParserResultType Char = new ParserResultType("Char");

        public string Name { get; }

        public ParserResultType(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
