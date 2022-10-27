namespace AlphaX.Parserz
{
    public class ParserResultType
    {
        public static ParserResultType Number = new ParserResultType("Number");
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
