using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMonkey
{
    public class Monkey
    {
        public int monkeyX { get; set; } 
        public int monkeyY { get; set; }
        public string monkeyTag { get; set; }
        public string monkeyName { get; set; }
        public bool isEscaped { get; set; }
        public string treeId { get; set; }

        public Monkey(string treeId, int x, int y, string monkeyName)
        {
            this.monkeyX = x;
            this.monkeyY = y;
            this.monkeyTag = GenerateMonkeyId();
            this.monkeyName = monkeyName;
            this.treeId = treeId;
            this.isEscaped = false;
        }

        public string GenerateMonkeyId()
        {
            string id = "M" + Guid.NewGuid().ToString();
            return id;
        }

    }
}
