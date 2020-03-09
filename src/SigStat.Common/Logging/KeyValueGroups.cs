using SigStat.Common.Helpers.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// A group of key-value pairs
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(KeyValueGroupConverter))]
    public class KeyValueGroup

    {
        /// <summary>
        /// Name of the group
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Key-Value pairs in the group
        /// </summary>
        public List<KeyValuePair<string, object>> Items { get; set; }

        /// <summary>
        /// Creates an emty key-value group
        /// </summary>
        /// <param name="name">Name if the new group</param>
        public KeyValueGroup(string name)
        {
            this.Name = name;
            Items = new List<KeyValuePair<string, object>>();
        }
    }

}
