using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class GraphContext
    {
        private int _n;
        private int _m;
        private bool[] _checked;
        private int[,] _distance;
        private List<int>[] _adjacency;
        private List<int>[,] _path;
        
        public GraphContext(Graph graph)
        {
            _n = graph.N;
            _m = graph.M;

            _checked = new bool[_n];
            _distance = new int[_n, _n];
            _adjacency = new List<int>[_n];
            _path = new List<int>[_n, _n];

            for (int i = 0; i < _n; i++) {
                _adjacency[i] = new List<int>();
            }

            foreach (var edge in graph.Edges)
            {
                int u = edge.Item1, v = edge.Item2;
                _adjacency[u].Add(v);
                _adjacency[v].Add(u);
            }
        }

        public int N { get => _n; }
        public int M { get => _m; }

        public int GetEccentricity(int v) {
            if (v < 0 || v >= _n) {
                throw new ArgumentException();
            }
            if (!_checked[v]) {
                BFS(v);
            }
            int max = 0;
            for (int i = 0; i < _n; i++) {
                max = Math.Max(max, _distance[v, i]);
            }
            return max;
        }

        public int[] GetPath(int u, int v) {
            if (!_checked[u]) {
                BFS(u);
            }
            return _path[u, v].ToArray();
        }

        public int Distance(int x, int y) {
            if (!_checked[x]) {
                BFS(x);
            }
            return _distance[x, y];
        }

        public int[] GetNeighbors(int v)
        {
            return _adjacency[v].ToArray();
        }

        private void BFS(int x) {
            _checked[x] = true;
            bool[] visited = new bool[_n];
            
            _path[x, x] = new List<int>() { x };
            _distance[x, x] = 0;
            visited[x] = true;
            
            Queue<int> q = new Queue<int>();
            q.Enqueue(x);
            
            while (q.Count != 0) {
                int v = q.Dequeue();
                foreach (var u in _adjacency[v])
                {
                    if (!visited[u]) 
                    {
                        visited[u] = true;

                        List<int> path = _path[x, v];
                        path.Add(u);
                        _path[x, u] = path;
                        _path[u, x] = path;
                        
                        _distance[x, u] = _distance[x, v] + 1;
                        _distance[u, x] = _distance[x, v] + 1;
                        q.Enqueue(u);
                    }
                }
            }

            foreach (var flag in visited)
            {
                if (!flag) {
                    throw new FormatException("Graph should be connected");
                }
            }
        }
    }
}
