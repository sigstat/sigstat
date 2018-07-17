using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class Configuration
    {

        public Lazy<Configuration> Default { get; set; } = new Lazy<Configuration>(()=>ConfigurationHelper.Load());

        public string DatabaseFolder { get; set; }

    }
}
