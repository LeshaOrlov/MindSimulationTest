using System;
using System.Linq;

namespace GraphTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(5);
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 1);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 2);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(1, 4, 2);
            graph.AddEdge(3, 4, 1);

            var result = Algorithm.IntersectVertexPath(graph, 0, 3, 1);

            Console.WriteLine("Вершины графа, которые являются общими для всех наименьших путей между выбранными вершинами: " + String.Join(',', result));
            Console.ReadKey();
        }
    }
}
