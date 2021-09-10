using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTest
{
    public class Vertex
    {
        public int dist = int.MaxValue;
        public bool used = false;
        public List<int> path = new List<int>();
    }

    
}
