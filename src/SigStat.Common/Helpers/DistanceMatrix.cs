using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class DistanceMatrix<TRowKey, TColumnKey, TValue>
    {
        Dictionary<KeyValuePair<TRowKey, TColumnKey>, TValue> items;


        public TValue this[TRowKey row, TColumnKey column]
        {
            get
            {
                if (items.TryGetValue(new KeyValuePair<TRowKey, TColumnKey>(row, column), out var value))
                    return value;
                else
                    return default(TValue);

            }
            set
            {
                items[new KeyValuePair<TRowKey, TColumnKey>(row, column)] = value;
            }
        }
    }
}
