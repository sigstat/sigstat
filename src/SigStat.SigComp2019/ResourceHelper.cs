using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.SigComp2019
{
    public static class ResourceHelper
    {
        public static string ReadString(string name)
        {
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("SigStat.SigComp2019.Resources." + name + ".txt"))
            using (StreamReader sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
