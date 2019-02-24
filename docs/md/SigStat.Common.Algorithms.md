## `Dtw`

Dynamic Time Warping algorithm - test if still works
```csharp
public class SigStat.Common.Algorithms.Dtw

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `List<ValueTuple<Int32, Int32>>` | ForwardPath | Gets the list of points representing the shortest path. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Compute(`Double[][]`signature1, `Double[][]`signature2) | Generate shortest path between the two sequences. | 


## `DtwPy`

```csharp
public static class SigStat.Common.Algorithms.DtwPy

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Dtw(`IEnumerable<P>`sequence1, `IEnumerable<P>`sequence2, `Func<P, P, Double>`distance) |  | 
| `Double` | EuclideanDistance(`Double[]`p1, `Double[]`p2) |  | 


## `HSCPThinningStep`

HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
```csharp
public class SigStat.Common.Algorithms.HSCPThinningStep

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Nullable<Boolean>` | ResultChanged | Gets whether the last `SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])` call was effective. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean[,]` | Scan(`Boolean[,]`b) | Does one step of the thinning. Call it iteratively while ResultChanged. | 


## `PatternMatching3x3`

Binary 3x3 pattern matcher with rotating option.
```csharp
public class SigStat.Common.Algorithms.PatternMatching3x3

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Match(`Boolean[,]`input) | Match the 3x3 input with the 3x3 pattern. | 
| `Boolean` | RotMatch(`Boolean[,]`input) | Match the 3x3 input with the 3x3 pattern from all 4 directions. | 


