using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMonkey
{
    public class Monkey
    {
        public int monkeyX { get; set; } 
        public int monkeyY { get; set; }
        public int monkeyTag { get; set; }
        public string monkeyName { get; set; }
        public bool isEscaped { get; set; }
        public int treeId { get; set; }

        public List<int> xHistory { get; set; }

        public List<int> yHistory { get; set; }

        public Monkey(int treeId, int x, int y, string monkeyName)
        {
            this.monkeyX = x;
            this.monkeyY = y;
            this.monkeyTag = 0;
            this.monkeyName = monkeyName;
            this.treeId = treeId;
            this.isEscaped = false;
        }

    }
}
