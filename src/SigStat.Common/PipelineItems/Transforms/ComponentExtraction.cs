using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class ComponentExtraction : PipelineBase, ITransformation
    {
        private readonly int samplingResolution;
        private bool[,] b;


        public ComponentExtraction(int samplingResolution)
        {
            this.samplingResolution = samplingResolution;
            this.Output(FeatureDescriptor<List<List<PointF>>>.Descriptor("Components"));
        }

        public void Transform(Signature signature)
        {
            b = signature.GetFeature(FeatureDescriptor<bool[,]>.Descriptor("Skeleton"));
            var endPoints = signature.GetFeature(FeatureDescriptor<List<Point>>.Descriptor("EndPoints"));
            var crossingPoints = signature.GetFeature(FeatureDescriptor<List<Point>>.Descriptor("CrossingPoints"));

            //TODO: megtalalni a vegpont nelkulieket (pl. perfekt O betu), ebbol egy pontot hozzaadni a crossingpointokhoz es jo lesz

            //meglevo endpointokhoz hozzaadjuk a crossingpointok szomszedait
            var crossings = SplitCrossings(crossingPoints);
            foreach (var endings in crossings)
                endPoints.AddRange(endings);
            Progress = 33;
            Log(LogLevel.Debug, $"{crossings.Count} crossings found.");

            var sectionlist = Trace(endPoints);
            Log(LogLevel.Debug, $"{sectionlist.Count} sections found");
            Progress = 66;
            //megvannak a sectionok, de meg ossze kell oket kotni a crossingoknal

            var componentlist = JoinSections(sectionlist, crossings);

            signature.SetFeature(OutputFeatures[0], componentlist);
            Progress = 100;
            Log(LogLevel.Info, $"Component extraction done. {componentlist.Count} components found.");
        }

        /// <summary>
        /// Unite crossingpoints into crossings (list of its endpoints), and
        /// split all crossings into neighbouring endpoints.
        /// </summary>
        /// <param name="crs">List of crossingpoints</param>
        /// <returns>List of crossings (1 crossing: List of endpoints)</returns>
        private List<List<Point>> SplitCrossings(List<Point> crs)
        {
            //bool[,] d = b.Clone() as bool[,];
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
                        for (int j = -1; j < 2; j++)
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
                                    newEndpoints.Add(new Point(m.X + i, m.Y + j));
                            }
                }

                crossings.Add(newEndpoints);
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

            Point p = new Point(-1, -1);
            Point prevp = new Point(-1, -1);
            while (endPoints.Count > 0)
            {
                List<Point> section = new List<Point>();
                p = endPoints[0];//egy endpointbol indulunk
                endPoints.RemoveAt(0);
                section.Add(new Point(p.X, p.Y));
                int nextsample = samplingResolution;

                bool end = false;
                while (!end)
                {
                    (prevp, p) = Step(prevp, p);

                    //isEndpoint()
                    for (int i = 0; i < endPoints.Count; i++)
                    {//TODO: azt is eszlelni, ha korbe korbe erunk, pl. perfekt O betu
                        if (p.Equals(endPoints[i]))
                        {
                            //kivesszuk a listabol, hogy ne jarjuk be 2x a szakaszt
                            endPoints.RemoveAt(i);
                            end = true;
                            break;
                        }
                    }
                    if (prevp.Equals(p))
                        end = true;//ritka eset amikor nincs szomszed

                    nextsample--;
                    if (nextsample == 0 || end)
                    {//idonkent, vagy amikor szakaszveghez erunk: uj mintavetel
                        nextsample = samplingResolution;
                        section.Add(new Point(p.X, p.Y));
                    }
                }
                sectionlist.Add(section);
            }

            return sectionlist;
        }

        private (Point p, Point nextp) Step(Point prevp, Point p)
        {
            Point nextp = new Point(-1, -1);
            //TODO: itt lehetne vmi prioritast bevezetni, pl. hogy prevp ellentetes iranyat vizsgaljuk eloszor (csak gyorsasag miatt)
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (b[p.X + i, p.Y + j] && !prevp.Equals(new Point(p.X + i, p.Y + j)) && !(i == 0 && j == 0))
                    {//van szomszed, nem onnan jottunk, es nem onmaga
                        nextp = new Point(p.X + i, p.Y + j);
                        return (p, nextp);
                    }

            //ide akkor erhetunk, ha egy pixelnek nincs egyaltalan szomszedja. akkor ez a szakasz ennyibol all
            Log(LogLevel.Warn, "Section tracing: 1-pixel section found");
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
                    /*if (sectionlist[iS].Count < 4)
                        continue;//tul rovid, elengedjuk*/

                    Point sFirst = sectionlist[iS][0];
                    Point sLast = sectionlist[iS][sectionlist[iS].Count - 1];
                    bool? First = null;
                    for (int iE = 0; iE < crossings[iC].Count; iE++)
                    {
                        if (crossings[iC][iE].Equals(sFirst))
                            First = true;
                        else if (crossings[iC][iE].Equals(sLast))
                            First = false;
                        if (First != null)
                        {
                            conn.Add((sectionlist[iS], First.Value));
                            //crossings[iC].RemoveAt(iE--);//
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
                    // double a1 = Math.Atan2(endpos1.Y - mid.y, endpos1.X - mid.x);
                    double a1 = Math.Atan2(endpos1.Y - prevpos1.Y, endpos1.X - prevpos1.X);
                    for (int jP = iP + 1; jP < conn.Count; jP++)
                    {
                        //keresztezodesen atmeno ket szakasz szogei kozti kulonbseg
                        int prevDistj = Math.Min(3, conn[jP].Data.Count - 1);//TODO: ez fuggjon a resolutiontol
                        Point endpos2 = conn[jP].First ? conn[jP].Data[0] : conn[jP].Data[conn[jP].Data.Count - 1];
                        Point prevpos2 = conn[jP].First ? conn[jP].Data[0 + prevDistj] : conn[jP].Data[conn[jP].Data.Count - 1 - prevDistj];
                        //double a2 = Math.Atan2(mid.y - endpos2.Y, mid.x - endpos2.X);
                        double a2 = Math.Atan2(prevpos2.Y - endpos2.Y, prevpos2.X - endpos2.X);
                        double diff = Math.Abs(AngleDiff(a1, a2));
                        while (diffs.ContainsKey(diff))
                            diff += 0.00001;//TODO ez lehet baj
                        diffs.Add(diff, (iP, jP));
                    }
                }

                //eddig int koordinatak voltak, de most at kell ternunk, mert lehet hogy a szakasz osszekotesek pontja finomabb lesz
                List<(List<PointF> Data, bool First)> connF = new List<(List<PointF>, bool)>();
                foreach (var ci in conn)
                {// list<point> => list<pointF>
                    List<PointF> ps = new List<PointF>();
                    foreach (var pi in ci.Data)
                        ps.Add(pi);
                    connF.Add((ps, ci.First));
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
                    {//toroljuk a rosszabb ertekeit a megtalalt endpointoknak
                        (int deli, int delj) = diffs[diffs.Keys[jD]];
                        if (deli == iP || deli == jP || delj == iP || delj == jP)
                            diffs.RemoveAt(jD--);
                    }

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

            }

            return componentlist;
        }

        private double AngleDiff(double a1, double a2)
        {
            //https://stackoverflow.com/questions/1878907/the-smallest-difference-between-2-angles
            return Math.Atan2(Math.Sin(a2 - a1), Math.Cos(a2 - a1));

            //return Math.Min((2.0 * Math.PI) - abs(a2 - a1), abs(a2 - a1)); //gyorsabb de rondabb

            //gabor:
            //double diff = Math.abs(a2 - a1);
            //diff = diff > Math.PI ? Math.abs(Math.PI*2.0 - diff) : diff;
        }

    }
}
