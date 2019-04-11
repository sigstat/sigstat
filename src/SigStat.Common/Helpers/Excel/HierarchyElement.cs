using System;
using System.Collections;
using System.Collections.Generic;

namespace SigStat.Common.Helpers.Excel
{
    /// <summary>
    /// Hierarchical structure to store object
    /// </summary>
    public class HierarchyElement : IEnumerable<HierarchyElement>
    {
        public List<HierarchyElement> Children { get; }

        public Object Content { get; set; }

        /// <summary>
        /// Create an emty element
        /// </summary>
        public HierarchyElement()
        {
            Children = new List<HierarchyElement>();
        }

        /// <summary>
        /// Create a new element with content
        /// </summary>
        /// <param name="Content">Content of the new element</param>
        public HierarchyElement(Object Content)
        {
            Children = new List<HierarchyElement>();
            this.Content = Content;
        }

        public void Add(HierarchyElement child)
        {
            Children.Add(child);
        }


        /// <summary>
        /// Return the hierarchy's depth from this node
        /// </summary>
        /// <returns></returns>
        public int GetDepth()
        {

            int max = 0;

            foreach (var child in Children)
            {
                if (child.GetDepth() > max)
                {
                    max = child.GetDepth();
                }
            }

            return max + 1;
        }

        /// <summary>
        /// Returns number of elements under this node and itself
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            int count = 1;

            foreach (var child in Children)
            {
                count += child.GetCount();
            }

            return count;
        }

        public override string ToString()
        {
            return this.Content.ToString();
        }

        public IEnumerator<HierarchyElement> GetEnumerator()
        {
            return ((IEnumerable<HierarchyElement>)Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


}
