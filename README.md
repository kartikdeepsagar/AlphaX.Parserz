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

Lets look at some examples for getting a headstart.

* Parsing Digit
```c#
var resultState = Parser.Digit.Run("1"); // resturns a result state
```
* Parsing Multiple Digits
```c#
int minimumCount = 1;
int maximumCount = 3;
var threeDigitParser = Parser.Digit.Many(1, 3);
var resultState = threeDigitParser.Run("874");
```
