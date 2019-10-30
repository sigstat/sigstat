# [InsertTable](./ExcelHelper--InsertTable.md)

Insert a table filled with data from an IEnumerable

| <span>Return&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Name | 
| :--- | :--- | 
| [ExcelRange](./ExcelHelper--InsertTable.md) | [InsertTable](./ExcelHelper--InsertTable.md) ([`ExcelWorksheet`](./ExcelHelper--InsertTable.md) ws, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) row, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) col, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Ienumerable)\<[`T`](./ExcelHelper--InsertTable.md)> data, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) title, [`ExcelColor`](./../Excel/ExcelColor.md) color, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) showHeader, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) Name) | 


#### Parameters
**`ws`**  [`ExcelWorksheet`](./ExcelHelper--InsertTable.md)<br>Worksheet in wich the table is created<br><br>**`row`**  [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)<br>Starting row of the table<br><br>**`col`**  [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)<br>Starting column of the table<br><br>**`data`**  [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Ienumerable)\<[`T`](./ExcelHelper--InsertTable.md)><br>2D array in wich the data to insert is stored<br><br>**`title`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>The table's title<br><br>**`color`**  [`ExcelColor`](./../Excel/ExcelColor.md)<br>The table's color<br><br>**`showHeader`**  [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)<br>Defines if the table has header<br><br>**`Name`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>If given, creates a named range, with this name
#### Returns
[ExcelRange](./ExcelHelper--InsertTable.md)<br>
Range of the inserted data