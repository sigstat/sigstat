using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class SerializationHelper
    {
        public static T Deserialize<T>(string s) where T:new()
        {
            return new T();
        }

        public static string Serialize<T>(T o)
        {
            return "";
        }
    }
}
