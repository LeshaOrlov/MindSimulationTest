using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTest
{
    static class Algorithm
    {
        static int INFINITY = int.MaxValue;
        static public List<Vertex> YenKSPMod(Graph graph, int start, int stop)
        {
            List<Vertex> CandidatePath = new List<Vertex>();
            var firstMinPath = DijkstraMod(graph, start, stop);
            var min = firstMinPath.dist;
            CandidatePath.Add(firstMinPath);
            for(int i = 0; i< firstMinPath.path.Count-1; i++)
            {
                var v = firstMinPath.path[i];
                var u = firstMinPath.path[i + 1];
                var node = graph.w[u,i];

                graph.w[u, v] = INFINITY;
                graph.w[v, u] = INFINITY;

                var path = DijkstraMod(graph, start, stop);
                if (path.dist == min) CandidatePath.Add(path);

                graph.w[u, i] = node;
                graph.w[i, u] = node;
            }

            return CandidatePath;

        }

        static public Vertex DijkstraMod(Graph graph, int start, int stop)
        {
            int n = graph.n;
            Vertex[] vertex = new Vertex[n];
            for (int i = 0; i < n; i++)
            {
                vertex[i] = new Vertex();
            }

            vertex[start].dist = 0;
            for (int i = 0; i < n; i++)
            {
                int min = INFINITY;
                int index = -1;

                // найдём вершину с минимальным расстоянием
                for (int j = 0; j < n; j++)
                    if (!vertex[j].used && (vertex[j].dist < min || index < 0))
                    {
                        min = vertex[j].dist;
                        index = j;
                    }

                int u = index;

                vertex[u].path.Add(u);
                vertex[u].used = true;

                for (int v = 0; v < n; v++)

                    if (!vertex[v].used &&
                        graph.w[u, v] != 0 &&
                         vertex[u].dist != INFINITY &&
                         graph.w[u, v] != INFINITY &&
                         vertex[u].dist + graph.w[u, v] < vertex[v].dist)
                    {
                        //найден новый путь
                        vertex[v].dist = vertex[u].dist + graph.w[u, v];
                        vertex[v].path = vertex[index].path.ToList();
                    }
            }

            return vertex[stop];
        }

        static public List<int> IntersectVertexPath(Graph graph, int first, int second, int third)
        {
            var p1 = IntersectVertexPath(graph, first, second);
            var p2 = IntersectVertexPath(graph, first, third);
            var p3 = IntersectVertexPath(graph, second, third);

            var result = p1.Intersect(p2).Intersect(p3).ToList();
            return result;
        }

        static public List<int> IntersectVertexPath(Graph graph, int first, int second)
        {
            List<Vertex> p1 = YenKSPMod(graph, first, second);
            List<int> cross = p1.First().path;
            for (int i = 1; i < p1.Count; i++)
            {
                cross = cross.Intersect(p1[i].path).ToList();
            }
            return cross;
        }
    }
}
