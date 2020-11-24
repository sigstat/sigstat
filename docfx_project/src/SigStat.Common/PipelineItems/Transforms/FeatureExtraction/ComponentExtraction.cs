using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts unsorted components by tracing through the binary Skeleton raster.
    /// <para>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints</para>
    /// <para>Default Pipeline Output: (List{List{PointF}}) Components</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ComponentExtraction : PipelineBase, ITransformation
    {

        /// <summary>
        /// binary representation of a signature image
        /// </summary>
        [Input]
        public FeatureDescriptor<bool[,]> Skeleton { get; set; }// = FeatureDescriptor<bool[,]>.Get("Skeleton");

        /// <summary>
        /// endpoints
        /// </summary>
        [Input]
        public FeatureDescriptor<List<Point>> EndPoints { get; set; }// = FeatureDescriptor<List<Point>>.Get("EndPoints");

        /// <summary>
        /// crossing points
        /// </summary>
        [Input]
        public FeatureDescriptor<List<Point>> CrossingPoints { get; set; }// = FeatureDescriptor<List<Point>>.Get("CrossingPoints");

        /// <summary>
        /// Output components
        /// </summary>
        [Output("Components")]
        public FeatureDescriptor<List<List<PointF>>> OutputComponents { get; set; }

        private readonly int samplingResolution;
        private bool[,] b;

        /// <summary> Initializes a new instance of the <see cref="ComponentExtraction"/> class with specified sampling resolution.</summary>
        /// <param name="samplingResolution">Steps to trace before a new point is sampled. Smaller values result in a more precise tracing. Provide a positive value.</param>
        public ComponentExtraction(int samplingResolution)
        {
            this.samplingResolution = samplingResolution;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            b = signature.GetFeature(Skeleton);
            var endPoints = signature.GetFeature(EndPoints);
            var crossingPoints = signature.GetFeature(CrossingPoints);

            if (samplingResolution < 1)
            {
                this.LogWarning("Invalid sampling resolution {samplingResolution}. It must be a positive integer.", samplingResolution);
            }

            //TODO: megtalalni a vegpont nelkulieket (pl. perfekt O betu), ebbol egy pontot hozzaadni a crossingpointokhoz es jo lesz

            //meglevo endpointokhoz hozzaadjuk a crossingpointok szomszedait
            var crossings = SplitCrossings(crossingPoints);

            //neha a szukito miatt tobbnek erzekelunk egy crossingot -> tul kozelieket vonjuk ossze
            crossings = UniteCloseCrossings(crossings, 45);


            foreach (var endings in crossings)
            {
                endPoints.AddRange(endings);
            }
            this.LogTrace("{crossingsCount} crossings found.", crossings.Count);

            var sectionlist = Trace(endPoints);
            this.LogTrace("{sectionlist} sections found", sectionlist.Count);
            //megvannak a sectionok, de meg ossze kell oket kotni a crossingoknal

            var componentlist = JoinSections(sectionlist, crossings);

            signature.SetFeature(OutputComponents, componentlist);
            this.LogInformation("Component extraction done. {componentlistCount} components found.", componentlist.Count);
        }

        /// <summary>
        /// Unite crossingpoints into crossings (list of its endpoints), and
        /// split all crossings into neighbouring endpoints.
        /// </summary>
        /// <param name="crs">List of crossingpoints</param>
        /// <returns>List of crossings (1 crossing: List of endpoints)</returns>
        private List<List<Point>> SplitCrossings(List<Point> crs)
        {
            List<List<Point>> crossings = new List<List<Point>>();
            while (crs.Count > 0)
            {
                List<Point> crgroup = new List<Point>();//growing group of crossing points
                List<Point> newEndpoints = new List<Point>();//endpoints that belong to current crossing

                b[crs[0].X, crs[0].Y] = false;//toroljuk a crossingpointot a keprol
                crgroup.Add(crs[0]);
                crs.RemoveAt(0);

                for (int im = 0; im < crgroup.Count; im++)
                {
                    var m = crgroup[im];
                    for (int i = -1; i < 2; i++)//3x3 neighbourhood
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (b[m.X + i, m.Y + j] && !(i == 0 && j == 0))
                            {
                                bool isNeighbourCrossingPoint = false;
                                for (int iN = 0; iN < crs.Count; iN++)
                                {
                                    if (crs[iN].Equals(new Point(m.X + i, m.Y + j)))
                                    {//ha ket egymas melletti crossingpoint van, az nem szamit tobbnek
                                        isNeighbourCrossingPoint = true;
                                        b[crs[iN].X, crs[iN].Y] = false;//toroljuk a crossingpointot a keprol
                                        crgroup.Add(crs[iN]);//add new member to current crossing group
                                        crs.Remove(crs[iN]);
                                        break;
                                    }
                                }
                                if (!isNeighbourCrossingPoint)
                                {
                                    newEndpoints.Add(new Point(m.X + i, m.Y + j));
                                }
                            }
                        }
                    }
                }

                crossings.Add(newEndpoints);
            }
            return crossings;
        }

        private static List<List<Point>> UniteCloseCrossings(List<List<Point>> crossings, int epsilon)
        {
            List<PointF> cMids = new List<PointF>();
            for (int iC = 0; iC < crossings.Count; iC++)
            {
                PointF crmid = new PointF(0, 0);
                foreach (Point cre in crossings[iC])
                {
                    crmid.X += cre.X;
                    crmid.Y += cre.Y;
                }
                crmid = new PointF(crmid.X / crossings[iC].Count, crmid.Y / crossings[iC].Count);
                cMids.Add(crmid);
            }
            for (int iC = 0; iC < crossings.Count - 1; iC++)
            {
                for (int jC = iC + 1; jC < crossings.Count; jC++)
                {
                    double dist = Math.Abs(cMids[iC].X - cMids[jC].X) + Math.Abs(cMids[iC].Y - cMids[jC].Y);
                    if (dist < epsilon)
                    {//osszevon ha tul kozel
                        crossings[iC].AddRange(crossings[jC]);
                        crossings.RemoveAt(jC);
                        cMids[iC] = new PointF((cMids[iC].X + cMids[jC].X) / 2, (cMids[iC].Y + cMids[jC].Y) / 2);
                        cMids.RemoveAt(jC);
                    }
                }
            }
            return crossings;
        }

        /// <summary>
        /// lekoveti a szakaszokat. Ebbe mar ne legyenek crossingpointok
        /// </summary>
        /// <param name="endPoints"></param>
        /// <returns>List of sections</returns>
        private List<List<Point>> Trace(List<Point> endPoints)
        {
            List<List<Point>> sectionlist = new List<List<Point>>();

            Point currp;
            Point prevp = new Point(-1, -1);
            while (endPoints.Count > 0)
            {
                List<Point> section = new List<Point>();
                currp = endPoints[0];//egy endpointbol indulunk
                endPoints.RemoveAt(0);
                section.Add(new Point(currp.X, currp.Y));
                int nextsample = samplingResolution;

                bool end = false;
                while (!end)
                {
                    (prevp, currp) = Step(prevp, currp);

                    //isEndpoint()
                    for (int i = 0; i < endPoints.Count; i++)
                    {//TODO: azt is eszlelni, ha korbe korbe erunk, pl. perfekt O betu
                        if (currp.Equals(endPoints[i]))
                        {
                            //kivesszuk a listabol, hogy ne jarjuk be 2x a szakaszt
                            endPoints.RemoveAt(i);
                            end = true;
                            break;
                        }
                    }
                    if (!end && prevp.Equals(currp))
                    {
                        end = true;//ritka eset amikor nincs szomszed
                        section.Remove(prevp);
                        break;//akkor azt felejtsuk is el, jo esellyel csak szukites hibaja
                    }

                    nextsample--;
                    if (nextsample == 0 || end)
                    {//idonkent, vagy amikor szakaszveghez erunk: uj mintavetel
                        nextsample = samplingResolution;
                        section.Add(new Point(currp.X, currp.Y));
                    }
                }
                if (section.Count > 0)  //x db pontnal rovidebb sectionoket felejtsuk el, jo esellyel csak szukites hibaja
                {
                    sectionlist.Add(section);
                }
            }

            return sectionlist;
        }

        private (Point p, Point nextp) Step(Point prevp, Point p)
        {
            Point nextp;
            //TODO: itt lehetne vmi prioritast bevezetni, pl. hogy prevp ellentetes iranyat vizsgaljuk eloszor (csak gyorsasag miatt)
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (b[p.X + i, p.Y + j] && !prevp.Equals(new Point(p.X + i, p.Y + j)) && !(i == 0 && j == 0))
                    {//van szomszed, nem onnan jottunk, es nem onmaga
                        nextp = new Point(p.X + i, p.Y + j);
                        return (p, nextp);
                    }
                }
            }

            //ide akkor erhetunk, ha egy pixelnek nincs egyaltalan szomszedja. akkor ez a szakasz ennyibol all
            this.LogWarning("Section tracing: 1-pixel section found at ({p.X}, {p.Y})", p.X, p.Y);
            return (prevp/*ez most p*/, p);
        }

        private List<List<PointF>> JoinSections(List<List<Point>> sectionlist, List<List<Point>> crossings)
        {
            List<List<PointF>> componentlist = new List<List<PointF>>();

            //szakaszokbol komponensek kepzese:osszekot crossingoknal
            for (int iC = 0; iC < crossings.Count; iC++)
            {
                List<(List<Point> Data, bool First)> conn = new List<(List<Point>, bool)>();//kigyujtjuk az adott crossinghoz tartozo szakaszokat
                for (int iS = 0; iS < sectionlist.Count; iS++)
                {
                    Point sFirst = sectionlist[iS][0];
                    Point sLast = sectionlist[iS][sectionlist[iS].Count - 1];
                    bool? First = null;
                    for (int iE = 0; iE < crossings[iC].Count; iE++)
                    {
                        if (crossings[iC][iE].Equals(sFirst))
                        {
                            First = true;
                        }
                        else if (crossings[iC][iE].Equals(sLast))
                        {
                            First = false;
                        }
                        if (First != null)
                        {
                            conn.Add((sectionlist[iS], First.Value));
                            break;
                        }
                    }
                }
                //megvan hogy melyik szakaszokat kell itt osszekotni

                SortedList<double, (int,int)> diffs = new SortedList<double, (int,int)>();//ez direkt nem Point, hanem tuple (a ket listabol indexek)
                for (int iP = 0; iP < conn.Count - 1; iP++)
                {
                    int prevDisti = Math.Min(3, conn[iP].Data.Count - 1);//TODO: ez fuggjon a resolutiontol
                    Point endpos1 = conn[iP].First ? conn[iP].Data[0] : conn[iP].Data[conn[iP].Data.Count - 1];
                    Point prevpos1 = conn[iP].First ? conn[iP].Data[0 + prevDisti] : conn[iP].Data[conn[iP].Data.Count - 1 - prevDisti];                   
                    double a1 = Math.Atan2(endpos1.Y - prevpos1.Y, endpos1.X - prevpos1.X);
                    for (int jP = iP + 1; jP < conn.Count; jP++)
                    {
                        //keresztezodesen atmeno ket szakasz szogei kozti kulonbseg
                        int prevDistj = Math.Min(3, conn[jP].Data.Count - 1);//TODO: ez fuggjon a resolutiontol
                        Point endpos2 = conn[jP].First ? conn[jP].Data[0] : conn[jP].Data[conn[jP].Data.Count - 1];
                        Point prevpos2 = conn[jP].First ? conn[jP].Data[0 + prevDistj] : conn[jP].Data[conn[jP].Data.Count - 1 - prevDistj];
                        double a2 = Math.Atan2(prevpos2.Y - endpos2.Y, prevpos2.X - endpos2.X);
                        double diff = Math.Abs(AngleDiff(a1, a2));
                        while (diffs.ContainsKey(diff))
                            diff += 0.00001;//TODO ez lehet baj
                        diffs.Add(diff, (iP, jP));
                    }
                }

                //eddig int koordinatak voltak, de most at kell ternunk, mert lehet hogy a szakasz osszekotesek pontja finomabb lesz
                List<(List<PointF> Data, bool First)> connF = new List<(List<PointF>, bool)>();
                List<bool> hasPair = new List<bool>();
                foreach (var ci in conn)
                {// list<point> => list<pointF>
                    List<PointF> ps = new List<PointF>();
                    foreach (var pi in ci.Data)
                        ps.Add(pi);
                    connF.Add((ps, ci.First));
                    hasPair.Add(false);
                }
                conn = null;

                //find middle of crossing
                PointF crmid = new PointF(0, 0);
                foreach (Point cre in crossings[iC])
                {
                    crmid.X += cre.X;
                    crmid.Y += cre.Y;
                }
                crmid = new PointF(crmid.X / crossings[iC].Count, crmid.Y / crossings[iC].Count);

                while (diffs.Count > 0)
                {//osszefuzzuk a legjobb talalatokat
                    (int iP, int jP) = diffs[diffs.Keys[0]];
                    double diff = diffs.Keys[0];
                    diffs.RemoveAt(0);
                    for (int jD = 0; jD < diffs.Count; jD++)
                    {//toroljuk a rosszabb ertekeit a megtalalt endpointoknak //TODO: ez nem jo, mert lehet olyat torlunk akinek nincs mas parja
                        (int deli, int delj) = diffs[diffs.Keys[jD]];
                        if (deli == iP || deli == jP || delj == iP || delj == jP)
                            diffs.RemoveAt(jD--);
                    }

                    //megvan a par
                    hasPair[iP] = true;
                    hasPair[jP] = true;

                    //egy iranyba nezzen a ket szakasz
                    if (connF[iP].First)
                        connF[iP].Data.Reverse();
                    if (!connF[jP].First)
                        connF[jP].Data.Reverse();

                    //ket szakasz kozti pont kitalalasa: a kapcsolodo vegpontok atlaga es a keresztezodes kozeppontja kozti lerp
                    double lerp = diff / Math.PI;
                    double pairmidx = (connF[iP].Data[connF[iP].Data.Count - 1].X + connF[jP].Data[0].X) / 2.0;
                    double pairmidy = (connF[iP].Data[connF[iP].Data.Count - 1].Y + connF[jP].Data[0].Y) / 2.0;
                    double finalmidx = crmid.X * lerp + (1 - lerp) * pairmidx;
                    double finalmidy = crmid.Y * lerp + (1 - lerp) * pairmidy;
                    //osszefuzes
                    connF[iP].Data.Add(new PointF((float)finalmidx, (float)finalmidy));
                    connF[iP].Data.AddRange(connF[jP].Data);

                    //hozzaadjuk a tobbihez a ketto osszekapcsolasat
                    componentlist.Add(connF[iP].Data);
                }

                //van e olyan, akinek meg nincs parja
                for(int iP = 0; iP<connF.Count;iP++)
                {
                    if(!hasPair[iP])
                    {
                        if (connF[iP].First)
                            connF[iP].Data.Reverse();
                        componentlist.Add(connF[iP].Data);
                    }
                }

            }

            return componentlist;
        }

        private static double AngleDiff(double a1, double a2)
        {
            //https://stackoverflow.com/questions/1878907/the-smallest-difference-between-2-angles
            return Math.Atan2(Math.Sin(a2 - a1), Math.Cos(a2 - a1));
        }

    }
}
