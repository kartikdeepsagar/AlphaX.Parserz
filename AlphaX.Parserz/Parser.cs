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
        /// <returns></returns>
        public static IParser<StringResult> UntilFound(string selector, bool matchCase = false) => new UntilFoundParser(selector, matchCase);

        /// <summary>
        /// Gets the string value parser.
        /// </summary>
        /// <param name="doubleQuotes">Specifies to parse double quoted string otherwise single quoted string</param>
        /// <returns></returns>
        public static IParser<StringResult> StringValue(bool doubleQuotes = true) => new StringValueParser(doubleQuotes);

        /// <summary>
        /// Gets the number parser.
        /// </summary>
        /// <param name="canParseDecimal"></param>
        /// <returns></returns>
        public static IParser<DoubleResult> Number(bool canParseDecimal = true) => new NumberParser(canParseDecimal);

        /// <summary>
        /// Gets the lazy parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static IParser Lazy(Func<IParser> parser) => new LazyParser(parser);

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return a result.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IParser FromResult(IParserResult result)
        {
            return new ResultParser(result);
        }

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return an error.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static IParser FromError(IParserError error)
        {
            return new ErrorParser(error);
        }
    }
}