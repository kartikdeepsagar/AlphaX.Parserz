using AlphaX.Parserz.Extensions;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Parsers;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System;
using System.Globalization;

namespace AlphaX.Parserz
{
    public static class Parser
    {
        /// <summary>
        /// Gets the letter parser.
        /// </summary>
        public static IParser<CharResult> Letter { get; }
        /// <summary>
        /// Gets the white space parser.
        /// </summary>
        public static IParser<CharResult> WhiteSpace { get; }
        /// <summary>
        /// Gets the digit parser.
        /// </summary>
        public static IParser<ByteResult> Digit { get; }
        /// <summary>
        /// Gets the decimal parser.
        /// </summary>
        public static IParser<DoubleResult> Decimal { get; }
        /// <summary>
        /// Gets the letter or digit parser.
        /// </summary>
        public static IParser LetterOrDigit { get; }
        /// <summary>
        /// Gets the boolean parser.
        /// </summary>
        public static IParser<BooleanResult> Boolean { get; }
        /// <summary>
        /// Gets the end of input parser.
        /// </summary>
        public static IParser EndOfInput { get; }

        static Parser()
        {
            Letter = new LetterParser();
            Digit = new DigitParser();
            EndOfInput = new EndOfInputParser();

            LetterOrDigit = Letter.Or(Digit)
                            .MapError(x => new ParserError(x.Index, string.Format(ParserMessages.InputError, x.Index, "a letter or digit")));

            Boolean = String("true")
                        .Or(String("false"))
                        .MapResult(x => new BooleanResult(bool.Parse(x.Value.ToString())))
                        .MapError(x => new ParserError(x.Index, string.Format(ParserMessages.InputError, x.Index, "true/false")));

            var digitsParser = Digit.Many(1).MapResult(x => x.ToStringResult());
            Decimal = digitsParser.AndThen(String(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                       .AndThen(digitsParser)
                       .MapResult(x => x.ToDoubleResult());

            WhiteSpace = Char(' ');
        }

        /// <summary>
        /// Gets the character parser.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IParser<CharResult> Char(char value) => new CharParser(value);
        /// <summary>
        /// Gets the string parser.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matchCase"></param>
        /// <returns></returns>
        public static IParser<StringResult> String(string value, bool matchCase = false) => new StringParser(value, matchCase);

        public static IParser Lazy(Func<IParser> parser) => new LazyParser(parser);
    }
}