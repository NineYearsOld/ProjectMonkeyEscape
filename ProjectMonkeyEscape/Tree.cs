using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMonkey
{
    public class Tree
    {
        public int treeX { get; set; }
        public int treeY { get; set; }
        public int treeTag { get; set; }
        public bool visited { get; set; }

        public Tree(int x, int y, int TreeTag)
        {
            this.treeTag = TreeTag;
            this.treeX = x;
            this.treeY = y;
            this.visited = false;
        }

    }
}
