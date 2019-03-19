using System.Collections.Generic;

namespace SigStat.Common.Helpers.Excel
{
    public class HierarchyElement
    {
        public List<HierarchyElement> Children { get; }

        public string Name { get; set; }

        public HierarchyElement(string Name)
        {
            Children = new List<HierarchyElement>();
            this.Name = Name;
        }

        public void AddChild(HierarchyElement child)
        {
            Children.Add(child);
        }

        //Return the depth from this node
        public int getDepth()
        {

            int max = 0;

            foreach (var child in Children)
            {
                if (child.getDepth() > max)
                {
                    max = child.getDepth();
                }
            }

            return max + 1;
        }

        //Returns number of elements under this node and itself
        public int getCount()
        {
            int count = 1;

            foreach (var child in Children)
            {
                count += child.getCount();
            }

            return count;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
