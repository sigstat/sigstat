#### `Dtw`

Dynamic Time Warping algorithm - test if still works
```csharp
public class SigStat.Common.Algorithms.Dtw

```

###### Properties

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `List<ValueTuple<Int32, Int32>>` | ForwardPath | Gets the list of points representing the shortest path. | 


###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Double` | Compute(Double[][], Double[][]) | Generate shortest path between the two sequences. | 


#### `DtwPy`

```csharp
public static class SigStat.Common.Algorithms.DtwPy

```

###### Static Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Double` | Dtw(IEnumerable<P>, IEnumerable<P>, Func<P, P, Double>) |  | 
| `Double` | EuclideanDistance(Double[], Double[]) |  | 


#### `HSCPThinningStep`

HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
```csharp
public class SigStat.Common.Algorithms.HSCPThinningStep

```

###### Properties

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Nullable<Boolean>` | ResultChanged | Gets whether the last `SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])` call was effective. | 


###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Boolean[,]` | Scan(Boolean[,]) | Does one step of the thinning. Call it iteratively while ResultChanged. | 


#### `PatternMatching3x3`

Binary 3x3 pattern matcher with rotating option.
```csharp
public class SigStat.Common.Algorithms.PatternMatching3x3

```

###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Boolean` | Match(Boolean[,]) | Match the 3x3 input with the 3x3 pattern. | 
| `Boolean` | RotMatch(Boolean[,]) | Match the 3x3 input with the 3x3 pattern from all 4 directions. | 


