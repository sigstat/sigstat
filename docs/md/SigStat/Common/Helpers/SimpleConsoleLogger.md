# [SimpleConsoleLogger](./SimpleConsoleLogger.md)

Namespace: [SigStat]() > [Common]() > [Helpers]()

Assembly: SigStat.Common.dll

Implements [ILogger](./SimpleConsoleLogger.md)

## Summary
A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.

## Constructors

| Name | Summary | 
| --- | --- | 
| SimpleConsoleLogger (  ) | Initializes a SimpleConsoleLogger instance with LogLevel set to LogLevel.Information | 
| SimpleConsoleLogger ( [`LogLevel`](./SimpleConsoleLogger.md) ) | Initializes an instance of SimpleConsoleLogger with a custom LogLevel | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [LogLevel](./SimpleConsoleLogger.md) | LogLevel | All events below this level will be filtered | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/System.IDisposable) | BeginScope ( [`TState`](./SimpleConsoleLogger.md) ) |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsEnabled ( [`LogLevel`](./SimpleConsoleLogger.md) ) |  | 
| void | Log ( [`LogLevel`](./SimpleConsoleLogger.md), [`EventId`](./SimpleConsoleLogger.md), [`TState`](./SimpleConsoleLogger.md), [`Exception`](https://docs.microsoft.com/en-us/dotnet/api/System.Exception), [`Func`](./SimpleConsoleLogger.md)\<[`TState`](./SimpleConsoleLogger.md), [`Exception`](https://docs.microsoft.com/en-us/dotnet/api/System.Exception), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)> ) |  | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [ErrorEventHandler](./SimpleConsoleLogger.md) | Logged | Occurs when an error is logged | 


