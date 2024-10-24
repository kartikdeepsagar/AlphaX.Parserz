using System;
using AlphaX.Parserz.Parsers;
using AlphaX.Parserz.Resources;

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
        /// Gets the white spaces parser.
        /// </summary>
        public static IParser<ArrayResult> WhiteSpaces { get; }
        /// <summary>
        /// Gets the digit parser.
        /// </summary>
        public static IParser<DoubleResult> Digit { get; }
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
            Boolean = new BooleanParser();
            WhiteSpace = Char(' ');
            WhiteSpaces = WhiteSpace.Many();
        }

        /// <summary>
        /// Gets the any letter or digit parser.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static IParser AnyLetterOrDigit(ParseMode mode = ParseMode.Both) => new LetterParser(mode).Or(Digit)
          .MapError(x => new ParserError(x.Index, string.Format(ParserMessages.InputError, x.Index, "a letter or digit")));

        /// <summary>
        /// Gets the letter parser.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static IParser<CharResult> AnyLetter(ParseMode mode = ParseMode.Both) => new LetterParser(mode);

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

        /// <summary>
        /// Gets the until parser.
        /// </summary>
        /// <param name="selector">Parses the input until selector is found</param>
        /// <param name="matchCase"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<StringResult> UntilFound(string selector, bool matchCase = false, bool allowTrace = false) => new UntilFoundParser(selector, matchCase, allowTrace);

        /// <summary>
        /// Gets the string value parser.
        /// </summary>
        /// <param name="doubleQuotes">Specifies to parse double quoted string otherwise single quoted string</param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<StringResult> StringValue(bool doubleQuotes = true, bool allowTrace = false) => new StringValueParser(doubleQuotes, allowTrace);

        /// <summary>
        /// Gets the number parser.
        /// </summary>
        /// <param name="canParseDecimal"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<DoubleResult> Number(bool canParseDecimal = true, bool allowTrace = false) => new NumberParser(canParseDecimal, allowTrace);

        /// <summary>
        /// Gets the lazy parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser Lazy(Func<IParser> parser, bool allowTrace = false) => new LazyParser(parser, allowTrace);

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return a result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser FromResult(IParserResult result, bool allowTrace = false)
        {
            return new ResultParser(result, allowTrace);
        }

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return an error.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser FromError(IParserError error, bool allowTrace = false)
        {
            return new ErrorParser(error, allowTrace);
        }
    }
}