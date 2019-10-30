# [InsertDictionary](./ExcelHelper--InsertDictionary.md)

Insert table from key-value pairs

| Return<div><a href="#"><img width=225></a></div> | Name<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [ExcelRange](./ExcelHelper--InsertDictionary.md) | [InsertDictionary](./ExcelHelper--InsertDictionary.md) ([`ExcelWorksheet`](./ExcelHelper--InsertDictionary.md) ws, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) row, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) col, [`IEnumerable`](./ExcelHelper--InsertDictionary.md)\<[`KeyValuePair`](./ExcelHelper--InsertDictionary.md)\<[`TKey`](./ExcelHelper--InsertDictionary.md), [`TValue`](./ExcelHelper--InsertDictionary.md)>> data, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) title, [`ExcelColor`](./../Excel/ExcelColor.md) color, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) Name ) | 


#### Parameters
**`ws`**  [`ExcelWorksheet`](./ExcelHelper--InsertDictionary.md)<br>Worksheet in wich the table is created<br><br>**`row`**  [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)<br>Starting row of the table<br><br>**`col`**  [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)<br>Starting column of the table<br><br>**`data`**  [`IEnumerable`](./ExcelHelper--InsertDictionary.md)\<[`KeyValuePair`](./ExcelHelper--InsertDictionary.md)\<[`TKey`](./ExcelHelper--InsertDictionary.md), [`TValue`](./ExcelHelper--InsertDictionary.md)>><br>IEnumerable of key-value pairs in wich the data to insert is stored<br><br>**`title`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>The table's title<br><br>**`color`**  [`ExcelColor`](./../Excel/ExcelColor.md)<br>The table's color<br><br>**`Name`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>If given, creates a named range, with this name
#### Returns
[ExcelRange](./ExcelHelper--InsertDictionary.md)<br>
Range of the inserted data