using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    public class StrokeMerging : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<double> InputWidthOfPen { get; set; }

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponent { get; set; }

        [Input]
        public FeatureDescriptor<List<ConnectionNode>> InputConnectionNodes { get; set; }

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> OutputComponents { get; set; }

        private static readonly double minSmooth = Math.PI / 180.0 * 155.0; 

        public void Transform(Signature signature)
        {
            this.LogInformation("StrokeMerging - transform started.");
            double widthOfPen = signature.GetFeature<double>(InputWidthOfPen);
            var components = signature.GetFeature<List<StrokeComponent>>(InputComponent);
            var strokes = components.GetAllStrokes();
            var connectionNodes = signature.GetFeature<List<ConnectionNode>>(InputConnectionNodes);

            var compPairs = new Dictionary<Stroke, Stroke>();

            foreach (var connectionNode in connectionNodes)
            {
                if (connectionNode.Degree(strokes) % 2 == 0)
                {

                    var addToCompPairs = new Dictionary<Stroke, Stroke>();
                    if (connectionNode.Degree(strokes) == 2)
                    {
                        addToCompPairs = MergeAtNodeD2(connectionNode, connectionNode.OutStrokes(strokes), widthOfPen);
                    }
                    else
                    {
                        addToCompPairs = MergingAtNode(connectionNode, connectionNode.OutStrokes(strokes), widthOfPen);
                    }
                    //

                    foreach (var pair in addToCompPairs)
                    {
                        compPairs.TryAdd(pair.Key, pair.Value);
                    }
                }
            }

            var strokeChains = MakeStrokeChains(components, connectionNodes, compPairs);

            var mergedComponents = new List<StrokeComponent>();
            foreach (var strokeChain in strokeChains)
            {
                mergedComponents.Add(MakeComponent(strokeChain));
            }
            signature.SetFeature<List<StrokeComponent>>(OutputComponents, mergedComponents);
            this.LogInformation("StrokeMerging - transform finished");
        }

        

        private static List<StrokeComponent> MergePairs(List<StrokeComponent> components, List<ConnectionNode> connectionNodes, Dictionary<Stroke, Stroke> compPairs)
        {
            var res = new List<StrokeComponent>();

            var strokeChains = MakeStrokeChains(components, connectionNodes, compPairs); 

            foreach (var strokeChain in strokeChains)
            {
                res.Add(MakeComponent(strokeChain));
            }

            return res;
        }

        private static List<List<Stroke>> MakeStrokeChains(List<StrokeComponent> components, List<ConnectionNode> connectionNodes, Dictionary<Stroke, Stroke> compPairs)
        {
            var res = new List<List<Stroke>>();
            var strokes = components.GetAllStrokes();
            var isInRes = new HashSet<StrokeComponent>();

            connectionNodes.Sort((p, q) => p.Degree(strokes) < q.Degree(strokes) ? -1 : 1);

            var actualStrokeChain = new List<Stroke>();
            while (components.Exists(comp => !isInRes.Contains(comp)))
            {
                actualStrokeChain = new List<Stroke>();
                var actualStroke = FindNextStartStroke(strokes, connectionNodes, isInRes);
                while (actualStroke != null)
                {
                    actualStrokeChain.Add(actualStroke);
                    isInRes.Add(actualStroke.Component);
                    actualStroke = compPairs.GetValueOrDefault(actualStroke);
                    if (actualStroke == actualStrokeChain[actualStrokeChain.Count - 1])
                    {
                        actualStroke = null;
                    }
                }
                res.Add(actualStrokeChain);
            }
            return res;
        }

        private static Stroke FindNextStartStroke(List<Stroke> strokes, List<ConnectionNode> connectionNodes, HashSet<StrokeComponent> isInRes)
        {
            foreach (var connectionNode in connectionNodes)
            {
                var outStrokes = connectionNode.OutStrokes(strokes);
                if (outStrokes.Exists(stroke => !isInRes.Contains(stroke.Component)))
                {
                    return outStrokes.Find(stroke => !isInRes.Contains(stroke.Component));
                }
            }
            throw new Exception();
        }

        private static StrokeComponent MakeComponent(List<Stroke> strokeChain)
        {
            var resStroke = new Stroke();
            resStroke.AddRange(strokeChain[0]);
            for (int i = 1; i < strokeChain.Count; i++)
            {
                resStroke.AddRange(OnlineToOfflineFeature.MakeVertexLine(strokeChain[i - 1].End, strokeChain[i].Start));
                resStroke.AddRange(strokeChain[i]);
            }
            return new StrokeComponent(resStroke);
        }

        private Dictionary<Stroke, Stroke> MergeAtNodeD2(ConnectionNode connectionNode, List<Stroke> outStrokes, double widthOfPen)
        {
            var res = new Dictionary<Stroke, Stroke>();
            int n = outStrokes.Count;
            if (n != 2)
            {
                throw new ArgumentException();
            }
            res.TryAdd(outStrokes[0].Sibling, outStrokes[1]);
            res.TryAdd(outStrokes[1].Sibling, outStrokes[0]);
            return res;
        }

        private static Dictionary<Stroke, Stroke> MergingAtNode(ConnectionNode conenctionNode, List<Stroke> outStrokes, double widthOfPen)
        {
            var res = new Dictionary<Stroke, Stroke>();
            int n = outStrokes.Count;
            double[,] angles = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    angles[i,j] = CalcDiffAngle(outStrokes[i], outStrokes[j], widthOfPen);
                }
            }
            bool[] hasAPair = new bool[n];
            double maxAngle = Math.PI;
            while (maxAngle > minSmooth)
            {
                maxAngle = 0.0;
                int maxi = -1;
                int maxj = -1;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!hasAPair[i] && !hasAPair[j] && angles[i,j] > maxAngle)
                        { maxi = i; maxj = j; maxAngle = angles[i,j]; }
                    }
                }

                if (maxAngle > minSmooth)
                {
                    hasAPair[maxi] = true;
                    hasAPair[maxj] = true;
                    res.TryAdd(outStrokes[maxi].Sibling, outStrokes[maxj]);
                    res.TryAdd(outStrokes[maxj].Sibling, outStrokes[maxi]);
                }
            }
            return res;
        }

        private static double CalcDiffAngle(Stroke stroke1, Stroke stroke2, double widthOfPen)
        {
            int diffIdx = (int)(2.0 * widthOfPen);
            return Geometry.DiffAngle(stroke1.StartDirection(diffIdx), stroke2.StartDirection(diffIdx));
        }
    }
}
