﻿using System;
using System.Runtime.InteropServices.ComTypes;

namespace AlphaX.Parserz
{
    public static class ParserExtensions
    {
        /// <summary>
        /// Transforms the error of parser.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        /// <param name="errorMap"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<T> MapError<T>(this IParser<T> parser, Func<IParserError, IParserError> errorMap, bool allowTrace = false)
            where T : IParserResult
        {
            return new ErrorMappedParser<T>(parser, errorMap, allowTrace);
        }

        /// <summary>
        /// Transforms the error of parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="errorMap"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser MapError(this IParser parser, Func<IParserError, IParserError> errorMap, bool allowTrace = false)
        {
            return new ErrorMappedParser<IParserResult>(parser, errorMap, allowTrace);
        }

        /// <summary>
        /// Transforms the result of parser.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="parser"></param>
        /// <param name="resultMap"></param>
        /// <param name="allowTrace"></param>
        /// <returns>A result mapped parser.</returns>
        public static IParser<TOut> MapResult<TIn, TOut>(this IParser<TIn> parser, Func<TIn, TOut> resultMap, bool allowTrace = false)
            where TIn : IParserResult
            where TOut : IParserResult
        {
            return new ResultMappedParser<TIn, TOut>(parser, resultMap, allowTrace);
        }

        /// <summary>
        /// Transforms the result of parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="resultMap"></param>
        /// <param name="allowTrace"></param>
        /// <returns>An error mapped parser.</returns>
        public static IParser MapResult(this IParser parser, Func<IParserResult, IParserResult> resultMap, bool allowTrace = false)
        {
            return new ResultMappedParser<IParserResult, IParserResult>(parser, resultMap, allowTrace);
        }

        /// <summary>
        /// Creates a parser chain by appending a new parser to the this parser based on the result.
        /// </summary>
        /// <param name="previousParser"></param>
        /// <param name="nextParserFunc"></param>
        /// <param name="allowTrace"></param>
        /// <returns>A chained parser.</returns>
        public static IParser Next(this IParser previousParser, Func<IParserResult, IParser> nextParserFunc, bool allowTrace = false)
        {
            return new ChainedParser(previousParser, nextParserFunc, allowTrace);
        }

        /// <summary>
        /// Creates a sequence of parsers that will result in success state if all the parsers are successfull.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="nextParser"></param>
        /// <param name="allowTrace"></param>
        /// <returns>A sequence of parser.</returns>
        public static IParser<ArrayResult> AndThen(this IParser parser, IParser nextParser, bool allowTrace = false)
        {
            if (parser is SequenceOfParser seqParser)
            {
                seqParser.Parsers.Add(nextParser);
                return seqParser;
            }

            return new SequenceOfParser(new IParser[] { parser, nextParser }, allowTrace);
        }

        /// <summary>
        /// Creates a choice of parsers that will result in success state if any of the parser is successfull.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="nextParser"></param>
        /// <param name="allowTrace"></param>
        /// <returns>A choice parser.</returns>
        public static IParser<IParserResult> Or(this IParser parser, IParser nextParser, bool allowTrace = false)
        {
            if (parser is ChoiceParser choiceParser)
            {
                choiceParser.Parsers.Add(nextParser);
                return choiceParser;
            }

            return new ChoiceParser(new IParser[] { parser, nextParser }, allowTrace);
        }

        /// <summary>
        /// Creates a many parser that will run the provided parser continuously 
        /// on an input string until it fails or reaches the input end.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="minCount">Minimum number of times that the parser should successfully run</param>
        /// <param name="maxCount">Maximum number of times that the parser should successfully run. Note: Skips this check if value is -1.</param>
        /// <param name="allowTrace"></param>
        /// <returns>A many parser</returns>
        public static IParser<ArrayResult> Many(this IParser parser, int minCount = 0, int maxCount = -1, bool allowTrace = false)
        {
            return new ManyParser(parser, minCount, maxCount, allowTrace);
        }

        /// <summary>
        /// Creates a many seperated by parser that will run the provided parser continuously 
        /// on an input string until it fails or reaches the input end.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="septByParser"></param>
        /// <param name="minCount">Minimum number of times that the parser should successfully run</param>
        /// <param name="maxCount">Maximum number of times that the parser should successfully run. Note: Skips this check if value is -1.</param>
        /// <param name="allowTrace"></param>
        /// <returns>A many parser</returns>
        public static IParser<ArrayResult> ManySeptBy(this IParser parser, IParser septByParser, int minCount = 0, int maxCount = -1, bool allowTrace = false)
        {
            return new ManySeptByParser(parser, septByParser, minCount, maxCount, allowTrace);
        }

        /// <summary>
        /// A parser to check the end of an input.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns>End of input parser</returns>
        public static IParser EndOfInput(this IParser parser)
        {
            return parser.Next(x => Parser.EndOfInput);
        }
    }
}
