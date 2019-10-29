# [LogTrace](./ILoggerObjectExtensions--LogTrace.md)

Formats and writes a trace log message.

| Return<div><a href="#"><img width=225></a></div> | Name<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Void](https://docs.microsoft.com/en-us/dotnet/api/System.Void) | [LogTrace](./ILoggerObjectExtensions--LogTrace.md) ( [`ILoggerObject`](./../ILoggerObject.md) obj, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) message, [`Object`](https://docs.microsoft.com/en-us/dotnet/api/System.Object)[] args ) | 


#### Parameters
**`obj`**  [`ILoggerObject`](./../ILoggerObject.md)<br>The SigStat.Common.ILoggerObject containing the Logger to write to.<br><br>**`message`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>Format string of the log message in message template format. Example: "User {User} logged in from {Address}"<br><br>**`args`**  [`Object`](https://docs.microsoft.com/en-us/dotnet/api/System.Object)[]<br>An object array that contains zero or more objects to format.
#### Returns
[Void](https://docs.microsoft.com/en-us/dotnet/api/System.Void)<br>
