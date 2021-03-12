using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// A Sparse Matrix representation of a distance graph.
    /// </summary>
    /// <typeparam name="TRowKey">Type to represent the row indexes</typeparam>
    /// <typeparam name="TColumnKey">Type to represent the column indexes</typeparam>
    /// <typeparam name="TValue">Type to represent the distances</typeparam>
    public class DistanceMatrix<TRowKey, TColumnKey, TValue>
    {
        readonly Dictionary<KeyValuePair<TRowKey, TColumnKey>, TValue>  items = new Dictionary<KeyValuePair<TRowKey, TColumnKey>, TValue>();
        /// <summary>
        /// Gets or sets a distance for a given row and column
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="column">column</param>
        /// <returns></returns>
        public TValue GetDistance(TRowKey row, TColumnKey column)
        {
            return this[row, column];
        }

        /// <summary>
        /// Gets or sets a distance for a given row and column
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="column">column</param>
        /// <returns></returns>
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

        /// <summary>
        ///  Gets the value associated with the specified keys. 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns>true if the Matrix contains an element with the specified keys; otherwise, false.</returns>
        public bool TryGetValue(TRowKey row, TColumnKey column, out TValue value)
        {
            return items.TryGetValue(new KeyValuePair<TRowKey, TColumnKey>(row, column), out value);
        }


        /// <summary>
        /// Determines whether the Matrix contains the specified key pair
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>true if the Matrix contains an element with the specified keys; otherwise, false.</returns>
        public bool ContainsKey(TRowKey row, TColumnKey column)
        {
            return items.ContainsKey(new KeyValuePair<TRowKey, TColumnKey>(row, column));
        }

        /// <summary>
        /// Generates a two dimensional array representation of the Matrix
        /// </summary>
        /// <returns>a two dimensional array representation of the Matrix, where the first row and column contain the corresponding row and column indexes</returns>
        internal object[,] ToArray()
        {
            var rows = items.Keys.Select(key => key.Key).Distinct().OrderBy(i => i).ToList();
            var columns = items.Keys.Select(key => key.Value).Distinct().OrderBy(i => i).ToList();

            var result = new object[columns.Count + 1, rows.Count + 1];
            for (int i = 0; i < columns.Count; i++)
            {
                result[i+1, 0] = columns[i].ToString();
            }
            for (int j = 0; j < rows.Count; j++)
            {
                result[0, j+1] = rows[j].ToString();
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

        internal static DistanceMatrix<TRowKey, TColumnKey, TValue> FromArray(object[,] a)
        {
            var dm = new DistanceMatrix<TRowKey, TColumnKey, TValue>();
            int columnCnt = a.GetLength(0);
            int rowCnt = a.GetLength(1);

            for (int i = 1; i < columnCnt; i++)
            {
                for (int j = 1; j < rowCnt; j++)
                {
                    dm[(TRowKey)a[0, j], (TColumnKey)a[i, 0]] = (TValue)a[i, j];
                }
            }

            return dm;
        }

        /// <summary>
        /// Enumerates all values stored on the Matrix
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<TValue> GetValues()
        {
            return items.Values;
        }
    }
}
