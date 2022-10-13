using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.Parserz.Extensions
{
    public static class ParserExtensions
    {
        /// <summary>
        /// Transforms the error of parser.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        /// <param name="errorMap"></param>
        /// <returns></returns>
        public static IParser<T> MapError<T>(this IParser<T> parser, Func<IParserError, IParserError> errorMap) 
            where T : IParserResult
        {
            return new ErrorMappedParser<T>(parser, errorMap);
        }

        /// <summary>
        /// Transforms the error of parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="errorMap"></param>
        /// <returns></returns>
        public static IParser MapError(this IParser parser, Func<IParserError, IParserError> errorMap)
        {
            return new ErrorMappedParser<IParserResult>(parser, errorMap);
        }

        /// <summary>
        /// Transforms the result of parser.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="parser"></param>
        /// <param name="resultMap"></param>
        /// <returns>A result mapped parser.</returns>
        public static IParser<TOut> MapResult<TIn, TOut>(this IParser<TIn> parser, Func<TIn, TOut> resultMap) 
            where TIn : IParserResult
            where TOut : IParserResult
        {
            return new ResultMappedParser<TIn, TOut>(parser, resultMap);
        }

        /// <summary>
        /// Transforms the result of parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="resultMap"></param>
        /// <returns>An error mapped parser.</returns>
        public static IParser MapResult(this IParser parser, Func<IParserResult, IParserResult> resultMap)
        {
            return new ResultMappedParser<IParserResult, IParserResult>(parser, resultMap);
        }

        /// <summary>
        /// Creates a parser chain by appending a new parser to the this parser based on the result.
        /// </summary>
        /// <param name="previousParser"></param>
        /// <param name="nextParserFunc"></param>
        /// <returns>A chained parser.</returns>
        public static IParser Next(this IParser previousParser, Func<IParserResult, IParser> nextParserFunc)
        {
            return new ChainedParser(previousParser, nextParserFunc);
        }

        /// <summary>
        /// Creates a sequence of parsers that will result in success state if all the parsers are successfull.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="nextParser"></param>
        /// <returns>A sequence of parser.</returns>
        public static IParser<ArrayResult> AndThen(this IParser parser, IParser nextParser)
        {
            if(parser is SequenceOfParser seqParser)
            {
                seqParser.Parsers.Add(nextParser);
                return seqParser;
            }

            return new SequenceOfParser(parser, nextParser);
        }

        /// <summary>
        /// Creates a choice of parsers that will result in success state if any of the parser is successfull.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="nextParser"></param>
        /// <returns>A choice parser.</returns>
        public static IParser<IParserResult> Or(this IParser parser, IParser nextParser)
        {
            if (parser is ChoiceParser choiceParser)
            {
                choiceParser.Parsers.Add(nextParser);
                return choiceParser;
            }

            return new ChoiceParser(parser, nextParser);
        }

        /// <summary>
        /// Creates a many parser that will run the provided parser continuously 
        /// on an input string until it fails or reaches the input end.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="minCount">Minimum number of times that the parser should successfully run</param>
        /// <param name="maxCount">Maximum number of times that the parser should successfully run. Note: Skips this check if value is -1.</param>
        /// <returns>A many parser</returns>
        public static IParser<ArrayResult> Many(this IParser parser, int minCount = 0, int maxCount = -1)
        {
            return new ManyParser(parser, minCount, maxCount);
        }

        /// <summary>
        /// Creates a many seperated by parser that will run the provided parser continuously 
        /// on an input string until it fails or reaches the input end.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="septByParser"></param>
        /// <param name="minCount">Minimum number of times that the parser should successfully run</param>
        /// <param name="maxCount">Maximum number of times that the parser should successfully run. Note: Skips this check if value is -1.</param>
        /// <returns>A many parser</returns>
        public static IParser<ArrayResult> ManySeptBy(this IParser parser, IParser septByParser, int minCount = 0, int maxCount = -1)
        {
            return new ManySeptByParser(parser, septByParser, minCount, maxCount);
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
