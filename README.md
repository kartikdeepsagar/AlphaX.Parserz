# AlphaX.Parserz

A strong & fast .NET Standard [Parser Combinator](https://en.wikipedia.org/wiki/Parser_combinator#:~:text=In%20computer%20programming%2C%20a%20parser,new%20parser%20as%20its%20output.) library for creating simple/complex parsers. 

# Architecture of the library

In this library, a parser is represented by the following *IParser* interface
```c#
public interface IParser
{
     IParserState Run(string input);
     IParserState Parse(IParserState inputState);
}
```
**Run** - Run method takes a string input and tries to parser the input as per the implemented logic of the parser. (Internally calls the Parse method)

**Parse** - Parse method takes an input state and returns an output (success/failure) state.

The input/output parser state is represented by the followin *IParserState* interface
```c#
public interface IParserState : ICloneable<IParserState>
{
      int Index { get; set; }
      string ActualInput { get; set; }
      string Input { get; }
      bool IsError { get; }
      IParserResult Result { get; set; }
      IParserError Error { get; set; }
}
```
**Index** - Index of the input from where the parsing will start.

**Actual Input** - Actual input passes to the initial parser.

**Input** - Input for the next parser.

**IsError** - Gets if the parser state is a failure state.

**IParserResult** - Represents the result of a state.

**IParserError** - Represents the error of a state.

*IParserResult/IParserResult<T>* interface
```c#
 public interface IParserResult
 {
      object Value { get; }
 }

 public interface IParserResult<T> : IParserResult
 {
      new T Value { get; }
 }
```
**Value** - Result value.

*IParserError* interface
```c#
public interface IParserError
{
      int Index { get; }
      string Message { get; }
}
```
**Index** - Index of the input where error occured.

**Message** - Error message with failure information.

# Creating a simple digit parser with *AlphaX.Parserz*

Create a *DigitParser* class by inheriting *AlphaX.Parserz.[Parser<T>](https://github.com/kartikdeepsagar/AlphaX.Parserz/blob/master/AlphaX.Parserz/Parsers/ParserBase.cs)* class and override its **ParseInput** method as follows:
```c#
public class DigitParser : Parser<ByteResult>
{
      protected override IParserState ParseInput(IParserState inputState)
      {
            var targetString = inputState.Input;

            if (string.IsNullOrEmpty(targetString))
                return CreateErrorState(inputState, new ParserError(inputState.Index,
                    string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));

            var character = targetString[0];
            if (char.IsDigit(character))
            {
                return CreateResultState(inputState, new ByteResult(Convert.ToByte(character - '0')), inputState.Index + 1);
            }

            return CreateErrorState(inputState, new ParserError(inputState.Index, 
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.Digits, targetString)));
     }
}
```
It's that simple! :-)

This library provides some inbuilt parsers to make your work easy. However, you can always use these inbuilt parsers to make a more complex parser or create your own parsers
```c#
public static class Parser
{
      public static IParser<ByteResult> Digit { get; }
      public static IParser<DoubleResult> Decimal { get; }
      public static IParser LetterOrDigit { get; }
      public static IParser<BooleanResult> Boolean { get; }
      ...
        
      static Parser()
      {
          Digit = new DigitParser();
          ...
      }
```

Lets look at some examples for getting a headstart.

* Parsing Digit
```c#
var resultState = Parser.Digit.Run("1");
```
* Parsing Multiple Digits
```c#
int minimumCount = 1;
int maximumCount = 3;
var threeDigitParser = Parser.Digit.Many(1, 3);
var resultState = threeDigitParser.Run("874");
```
You can see that we have used an extension method i.e. **Many** in the above code. It just returns a new ManyParser which basically runs the input parser on the input string provided number (min/max) of times.
```c#
 public static IParser<ArrayResult> Many(this IParser parser, int minCount = 0, int maxCount = -1)
 {
       return new ManyParser(parser, minCount, maxCount);
 }
```
Similarly, you can combine small parsers to make a more complex parser. For example, you can create a basic email (gmail/microsoft) parser as follows:
```c#
var @parser = AlphaX.Parserz.Parser.String("@");
var dotParser = AlphaX.Parserz.Parser.String(".");
var comParser = AlphaX.Parserz.Parser.String("com");
var gmailParser = AlphaX.Parserz.Parser.String("gmail");
var microsoftParser = AlphaX.Parserz.Parser.String("microsoft");

// username parser to parse names starting with letters and then containing letters/digits
var userNameParser = AlphaX.Parserz.Parser.Letter.Many()
   .AndThen(Parser.LetterOrDigit.Many())
   .MapResult(x => x.ToStringResult()); // converting to string result

// domain parser for example, @gmail.com
var domainParser = @parser
    .AndThen(gmailParser.Or(microsoftParser))
    .AndThen(dotParser)
    .AndThen(comParser)
    .MapResult(x => x.ToStringResult());

var emailParser = userNameParser.AndThen(domainParser)
     .MapResult(x => new EmailResult(new Email()
     {
             UserName = (string)x.Value[0].Value,
             Domain = (string)x.Value[1].Value
      }));
```
And the EmailResult class is defined as follows:
```c#
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
```
And this is how we can use the parser
```c#
var result = emailParser.Run("testuser@gmail.com");
var email = result.Result as EmailResult;
Console.WriteLine(JsonConvert.SerializeObject(email.Value)); // {"UserName":"testuser","Domain":"@gmail.com"}
```
