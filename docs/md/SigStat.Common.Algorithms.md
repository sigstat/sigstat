#### `Dtw`

Dynamic Time Warping algorithm - test
```csharp
public class SigStat.Common.Algorithms.Dtw

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Compute(Double[][], Double[][])</sub> | Generate shortest path between the two sequences. | 


#### `DtwPy`

A simple implementation of the DTW algorithm.
```csharp
public static class SigStat.Common.Algorithms.DtwPy

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Dtw(IEnumerable<P>, IEnumerable<P>, Func<P, P, Double>)</sub> | Calculates the distance between two time sequences | 
| `Double` | <sub>EuclideanDistance(Double[], Double[])</sub> | Calculates the euclidean distance of two vectors | 


#### `HSCPThinningStep`

HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
```csharp
public class SigStat.Common.Algorithms.HSCPThinningStep

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Boolean[,]` | <sub>Scan(Boolean[,])</sub> | Does one step of the thinning. Call it iteratively while ResultChanged. | 


#### `PatternMatching3x3`

Binary 3x3 pattern matcher with rotating option.
```csharp
public class SigStat.Common.Algorithms.PatternMatching3x3

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Boolean` | <sub>Match(Boolean[,])</sub> | Match the 3x3 input with the 3x3 pattern. | 
| `Boolean` | <sub>RotMatch(Boolean[,])</sub> | Match the 3x3 input with the 3x3 pattern from all 4 directions. | 


