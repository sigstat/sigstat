using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class MyCollection<T>: Dictionary<int, T> where T : ObjectWithID 
    {
        public void Add(T p)
        {
            this.TryAdd(p.ID, p);
        }

        public void Add(IEnumerable<T> list)
        {
            foreach (var p in list)
            {
                Add(p);
            }
        }

        public T Get(int id)
        {
            T p;
            TryGetValue(id, out p);
            return p;
        }

        public void Remove(T p)
        {
            this.Remove(p.ID);
        }

        public bool Contains(T p)
        {
            return this.ContainsKey(p.ID);
        }
    }
}
