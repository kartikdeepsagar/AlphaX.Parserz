using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Results;
using System;
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

        public static Int32Result ToInt32Result(this ArrayResult results)
        {
            return new Int32Result(int.Parse(results.Concat()));
        }

        public static DoubleResult ToDoubleResult(this ArrayResult results)
        {
            return new DoubleResult(double.Parse(results.Concat()));
        }

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

            return new StringResult(new ArrayResult(resultItems.ToArray()).Concat());
        }

        private static string Concat(this ArrayResult result)
        {
            return string.Concat(result.Select(x => x.Value));
        }
    }
}
