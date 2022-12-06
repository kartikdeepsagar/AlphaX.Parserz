using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;

namespace AlphaX.Parserz.Extended
{
    public static class BuiltInParser
    {
        static BuiltInParser()
        {

        }

        /// <summary>
        /// Gets a ranged number parser.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="canParseDecimal"></param>
        /// <returns></returns>
        public static IParser<DoubleResult> RangedNumberParser(double minimum, double maximum, bool canParseDecimal = true) => new RangedNumberParser(minimum, maximum, canParseDecimal);
    }
}
