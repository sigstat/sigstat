using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    public class DistanceMatrix<TRowKey, TColumnKey, TValue>
    {
        Dictionary<KeyValuePair<TRowKey, TColumnKey>, TValue> items = new Dictionary<KeyValuePair<TRowKey, TColumnKey>, TValue>();


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

        public bool TryGetValue(TRowKey row, TColumnKey column, out TValue value)
        {
            return items.TryGetValue(new KeyValuePair<TRowKey, TColumnKey>(row, column), out value);
        }

        internal object[,] ToArray()
        {
            var rows = items.Keys.Select(key => key.Key).Distinct().OrderBy(i => i).ToList();
            var columns = items.Keys.Select(key => key.Value).Distinct().OrderBy(i => i).ToList();

            var result = new object[columns.Count + 1, rows.Count + 1];
            for (int i = 0; i < columns.Count; i++)
            {
                result[i, 0] = columns[i].ToString();
            }
            for (int j = 0; j < rows.Count; j++)
            {
                result[0, j] = rows[j].ToString();
            }
            for (int i = 0; i < columns.Count; i++)
            {
                for (int j = 0; j < rows.Count; j++)
                {
                    if (TryGetValue(rows[j], columns[i], out var item))
                        result[i + 1, j + 1] = item;
                }
            }
            return result;

        }
    }
}
