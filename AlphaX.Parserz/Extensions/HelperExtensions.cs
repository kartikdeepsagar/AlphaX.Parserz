using AlphaX.Parserz.Exceptions;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.Parserz.Extensions
{
    public static class HelperExtensions
    {
        public static T As<T>(this IParserResult result)
            where T : IParserResult
        {
            return (T)result;
        }

        /// <summary>
        /// Converts array result to int32 result.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Int32Result ToInt32Result(this ArrayResult results)
        {
            string input = results.Concat();

            if (!int.TryParse(input, out int result))
                throw new ParsingException(string.Format(ParserMessages.TypeConvertError, input, typeof(Int32Result)));

            return new Int32Result(result);
        }

        /// <summary>
        /// Converts array result to int64 result.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Int64Result ToInt64Result(this ArrayResult results)
        {
            string input = results.Concat();

            if(!long.TryParse(input, out long result))
                throw new ParsingException(string.Format(ParserMessages.TypeConvertError, input, typeof(Int64Result)));

            return new Int64Result(result);
        }

        /// <summary>
        /// Converts array result to double result. Returns 0 if conversion fails.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static DoubleResult ToDoubleResult(this ArrayResult results)
        {
            string input = results.Concat();

            if(!double.TryParse(input, out double result))
                throw new ParsingException(string.Format(ParserMessages.TypeConvertError, input, typeof(DoubleResult)));

            return new DoubleResult(result);
        }

        /// <summary>
        /// Converts array result to string result.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static StringResult ToStringResult(this ArrayResult results)
        {
            List<IParserResult> resultItems = results.ToList();

            for(int index = 0; index < resultItems.Count; index++)
            {
                var result = resultItems[index];

                if(result is ArrayResult arrayResult)
                {
                    resultItems.Remove(arrayResult);
                    for (int index2 = 0; index2 < arrayResult.Value.Length; index2++)
                        resultItems.Insert(index + index2, arrayResult.Value[index2]);
                }
                else
                {
                    if(!resultItems.Contains(result))
                        resultItems.Insert(index, result);
                }
            }

            return new StringResult(resultItems.Concat());
        }

        private static string Concat(this IEnumerable<IParserResult> result)
        {
            return string.Concat(result.Select(x => x.Value));
        }
    }
}
