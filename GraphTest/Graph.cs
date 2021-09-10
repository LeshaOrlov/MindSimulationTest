using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTest
{
    public class Graph
    {
        readonly int INFINITY = int.MaxValue;
        public readonly int n;
        //матрица весов
        public int[,] w;

        public Graph(int n)
        {
            this.n = n;
            this.w = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.w[i, j] = INFINITY;
                }
            }
        }

        public void AddEdge(int i, int j, int w)
        {
            this.w[i, j] = w;
            this.w[j, i] = w;
        }

    }
}
