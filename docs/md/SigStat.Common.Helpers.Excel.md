#### `ExcelHelper`

```csharp
public static class SigStat.Common.Helpers.Excel.ExcelHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>BorderAround(this ExcelRange, ExcelBorderStyle = Thin)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>BorderEverywhere(this ExcelRange, ExcelBorderStyle = Thin)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>FormatAsTable(this ExcelRange, ExcelColor = Primary, Boolean = True, Boolean = True)</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>FormatAsTableWithTitle(this ExcelRange, String, ExcelColor = Primary, Boolean = True, Boolean = True)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>HyperlinkStyle(this ExcelStyle)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertHierarchicalList(this ExcelWorksheet, Int32, Int32, HierarchyElement, String = null, ExcelColor = Primary)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertLegend(this ExcelRange, String, String = null, Boolean = False, ExcelColor = Info)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertLink(this ExcelRange, String)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertLink(this ExcelRange, String, String)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertTable(this ExcelWorksheet, Int32, Int32, Object[,], String = null, ExcelColor = Primary, Boolean = True, Boolean = True)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertTable(this ExcelWorksheet, Int32, Int32, IEnumerable<T>, String = null, ExcelColor = Primary, Boolean = True)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>InsertTable(this ExcelWorksheet, Int32, Int32, IEnumerable<KeyValuePair<TKey, TValue>>, String = null, ExcelColor = Primary, Boolean = False)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>Merge(this ExcelRangeBase)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>PrintHierarchicalList(this ExcelWorksheet, Int32, Int32, HierarchyElement)</sub> | <sub></sub> | 


#### `HierarchyElement`

```csharp
public class SigStat.Common.Helpers.Excel.HierarchyElement

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<HierarchyElement></sub> | <sub>Children</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Name</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>AddChild(HierarchyElement)</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>getCount()</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>getDepth()</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>ToString()</sub> | <sub></sub> | 


