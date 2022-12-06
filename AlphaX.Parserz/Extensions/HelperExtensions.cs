using AlphaX.Parserz.Exceptions;
using AlphaX.Parserz.Interfaces;
using AlphaX.Parserz.Resources;
using AlphaX.Parserz.Results;
using System.Collections.Generic;
using System.Text;

namespace AlphaX.Parserz.Extensions
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Converts array result to double result. Returns 0 if conversion fails.
        /// </summary>
        /// <param name="arrayResult"></param>
        /// <returns></returns>
        public static DoubleResult ToDoubleResult(this ArrayResult arrayResult)
        {
            string input = arrayResult.ToStringResult().Value;

            if (!double.TryParse(input, out double result))
                throw new ParsingException(string.Format(ParserMessages.TypeConvertError, input, typeof(DoubleResult)));

            return new DoubleResult(result);
        }

        /// <summary>
        /// Converts array result to string result.
        /// </summary>
        /// <param name="arrayResult"></param>
        /// <returns></returns>
        public static StringResult ToStringResult(this ArrayResult arrayResult)
        {
            var nodes = new Queue<IParserResult>();
            nodes.Enqueue(arrayResult);
            var stringBuilder = new StringBuilder();

            while (nodes.Count != 0)
            {
                var item = nodes.Dequeue();

                if (item is ArrayResult aResult)
                {
                    for (int index = 0; index < aResult.Value.Length; index++)
                        nodes.Enqueue(aResult.Value[index]);
                }
                else
                {
                    stringBuilder.Append(item.Value);
                }
            }

            return new StringResult(stringBuilder.ToString());
        }

        /// <summary>
        /// Converts string result to double result.
        /// </summary>
        /// <param name="stringResult"></param>
        /// <returns></returns>
        public static DoubleResult ToDoubleResult(this StringResult stringResult)
        {
            try
            {
                if (!string.IsNullOrEmpty(stringResult.Value))
                    return new DoubleResult(double.Parse(stringResult.Value));

                return DoubleResult.Invalid;
            }
            catch
            {
                return DoubleResult.Invalid;
            }
        }

        /// <summary>
        /// Converts string result to boolean result.
        /// </summary>
        /// <param name="stringResult"></param>
        /// <returns></returns>
        public static BooleanResult ToBooleanResult(this StringResult stringResult)
        {
            try
            {
                if (!string.IsNullOrEmpty(stringResult.Value))
                    return new BooleanResult(bool.Parse(stringResult.Value));

                return BooleanResult.Invalid;
            }
            catch
            {
                return BooleanResult.Invalid;
            }
        }
    }
}
