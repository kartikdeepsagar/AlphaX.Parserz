using AlphaX.Parserz;
using AlphaX.Parserz.Extended;
using AlphaX.Parserz.Extensions;
using AlphaX.Parserz.Interfaces;

namespace ConsoleApp1
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            string[] vars = { "2w 5m", "1w 2d 1m 3h", "1h", "1m", "2d" };

            foreach(var s in vars)
            {
                var ss = BuiltInParser.HumanTimeSpanParser.Run(s);
                var val = ss.Result.Value;
            }
        }

    }
}
