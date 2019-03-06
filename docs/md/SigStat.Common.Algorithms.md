#### `Dtw`

<sub>Dynamic Time Warping algorithm - test comment</sub>
```csharp
public class SigStat.Common.Algorithms.Dtw

```

###### Properties

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<ValueTuple<Int32, Int32>></sub> | <sub>ForwardPath</sub> | <sub>Gets the list of points representing the shortest path.</sub> | 


###### Methods

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Compute(Double[][], Double[][])</sub> | <sub>Generate shortest path between the two sequences.</sub> | 


#### `DtwPy`

<sub>A simple implementation of the DTW algorithm.</sub>
```csharp
public static class SigStat.Common.Algorithms.DtwPy

```

###### Static Methods

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Dtw(IEnumerable<P>, IEnumerable<P>, Func<P, P, Double>)</sub> | <sub>Calculates the distance between two time sequences</sub> | 
| <sub>Double</sub> | <sub>EuclideanDistance(Double[], Double[])</sub> | <sub>Calculates the euclidean distance of two vectors</sub> | 


#### `HSCPThinningStep`

<sub>HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf</sub>
```csharp
public class SigStat.Common.Algorithms.HSCPThinningStep

```

###### Properties

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Nullable<Boolean></sub> | <sub>ResultChanged</sub> | <sub>Gets whether the last `SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])` call was effective.</sub> | 


###### Methods

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Boolean[,]</sub> | <sub>Scan(Boolean[,])</sub> | <sub>Does one step of the thinning. Call it iteratively while ResultChanged.</sub> | 


#### `PatternMatching3x3`

<sub>Binary 3x3 pattern matcher with rotating option.</sub>
```csharp
public class SigStat.Common.Algorithms.PatternMatching3x3

```

###### Methods

| Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Boolean</sub> | <sub>Match(Boolean[,])</sub> | <sub>Match the 3x3 input with the 3x3 pattern.</sub> | 
| <sub>Boolean</sub> | <sub>RotMatch(Boolean[,])</sub> | <sub>Match the 3x3 input with the 3x3 pattern from all 4 directions.</sub> | 


