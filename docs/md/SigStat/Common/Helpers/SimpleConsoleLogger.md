# [SimpleConsoleLogger](./SimpleConsoleLogger.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Helpers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger)

## Summary
A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.

## Constructors

| Name | Summary | 
| --- | --- | 
| SimpleConsoleLogger (  ) | Initializes a SimpleConsoleLogger instance with LogLevel set to LogLevel.Information | 
| SimpleConsoleLogger ( [`LogLevel`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel) logLevel ) | Initializes an instance of SimpleConsoleLogger with a custom LogLevel | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [LogLevel](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel) | LogLevel | All events below this level will be filtered | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/System.IDisposable) | BeginScope ( [`TState`](./SimpleConsoleLogger.md) state ) |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsEnabled ( [`LogLevel`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel) logLevel ) |  | 
| void | Log ( [`LogLevel`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel) logLevel, [`EventId`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.EventId) eventId, [`TState`](./SimpleConsoleLogger.md) state, [`Exception`](https://docs.microsoft.com/en-us/dotnet/api/System.Exception) exception, [`Func`](./SimpleConsoleLogger.md)\<[`TState`](./SimpleConsoleLogger.md), [`Exception`](https://docs.microsoft.com/en-us/dotnet/api/System.Exception), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)> formatter ) |  | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [ErrorEventHandler](./SimpleConsoleLogger.md) | Logged | Occurs when an error is logged | 


