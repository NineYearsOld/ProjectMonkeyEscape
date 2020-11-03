using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMonkey
{
    public class Grove
    {
        public int groveLength { get; set; }
        public int groveWidth { get; set; }
        public string GroveTag { get; set; }

        public Grove(int x, int y)
        {
            this.groveLength = y;
            this.groveWidth = x;
            this.GroveTag = generateGroveId();
        }

        public string generateGroveId()
        {
            string id = "G" + Guid.NewGuid().ToString();
            return id;
        }
    }
}
