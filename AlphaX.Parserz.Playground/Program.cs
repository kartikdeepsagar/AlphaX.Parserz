using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaX.Parserz.Tracing;

namespace AlphaX.Parserz.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var @parser = AlphaX.Parserz.Parser.String("@");
            var dotParser = AlphaX.Parserz.Parser.String(".");
            var comParser = AlphaX.Parserz.Parser.String("com");
            var alphaxParser = AlphaX.Parserz.Parser.String("alphax");
            var microsoftParser = AlphaX.Parserz.Parser.String("microsoft");

            // username parser to parse names starting with letters and then containing letters/digits

            var userNameParser = AlphaX.Parserz.Parser.Letter
               .AndThen(Parser.AnyLetterOrDigit(ParseMode.Both).Many())
               .MapResult(x => x.ToStringResult()); // converting to string result

            // domain parser for example, @gmail.com
            var domainParser = @parser
                .AndThen(alphaxParser.Or(microsoftParser))
                .AndThen(dotParser)
                .AndThen(comParser)
                .MapResult(x => x.ToStringResult());

            var emailParser = userNameParser.AndThen(domainParser)
                 .MapResult(x => new EmailResult(new Email()
                 {
                     UserName = (string)x.Value[0].Value,
                     Domain = (string)x.Value[1].Value
                 }));

            ParserTracer.Enabled = true;
            var result = emailParser.Run("emailparser1@alphax.com");
            Console.WriteLine(JsonSerializer.Serialize(ParserTracer.GetTraces()));
            IEnumerable<Trace> traces = ParserTracer.GetTraces();
            ParserTracer.Reset();

            Console.ReadKey();
        }
    }

    public class Email
    {
        public string UserName { get; set; }
        public string Domain { get; set; }
    }

    public class EmailResult : ParserResult<Email>
    {
        // specifies the type of result
        public static ParserResultType EmailResultType = new ParserResultType("email");

        public EmailResult(Email email) : base(email, EmailResultType)
        {

        }
    }
}
