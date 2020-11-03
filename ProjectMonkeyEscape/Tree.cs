using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMonkey
{
    public class Tree
    {
        public int treeX { get; set; }
        public int treeY { get; set; }
        public string treeTag { get; set; }
        public bool visited { get; set; }

        public Tree(int x, int y)
        {
            this.treeX = x;
            this.treeY = y;
            this.treeTag = generateTreeId();
            this.visited = false;
        }

        public string generateTreeId()
        {
            string id = "T" + Guid.NewGuid().ToString();
            return id;
        }

    }
}
