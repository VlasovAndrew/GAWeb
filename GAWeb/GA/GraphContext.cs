using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class GraphContext
    {
        private int _n;
        private int _m;
        // Массив для хранения флагов, показывающих 
        // был ли запущен алгоритм поиска эксцентриситета из вершины.
        private bool[] _checked;
        // Двумерная матрица для хранения расстояний между вершинами.
        private int[,] _distance;
        // Список смежности для хранения графа.
        private List<int>[] _adjacency;
        // Двумераня матрица для хранения путей между вершинами.
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
                int u = edge.V1, v = edge.V2;
                _adjacency[u].Add(v);
                _adjacency[v].Add(u);
            }
        }

        public int N { get => _n; }
        public int M { get => _m; }

        // Метод получения эксцентриситета вершины.
        // Запускает алгоритм поиска в ширину, после чего 
        // находит максималную длину пути от заданной вершины до всех остальных.
        public int GetEccentricity(int v) {
            if (v < 0 || v >= _n) {
                throw new ArgumentException("Номер вершины должен быть положительным");
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
        // Проверка связности графа.
        // Запуск обхода из нулевой вершины, после чего проверяется 
        // длина пути до всех остальных вершин и если есть вершины, до которых не дошел алгоритм,
        // то это означает, что граф не связный, иначе связный.
        public bool CheckConnectivity() {
            BFS(0);
            for (int i = 0; i < _n; i++) {
                if (_path[0, i].Count == 0) {
                    return false;
                }
            }
            return true;
        }
        // Получение пути между вершинами,
        // если алгоритм поиска в ширину запускался из вершины не запускался, 
        // то запускается алгоритм поиска в ширину.
        public int[] GetPath(int u, int v) {
            if (!_checked[u]) {
                BFS(u);
            }
            return _path[u, v].ToArray();
        }
        // Получение расстояния между вершинами.
        public int Distance(int x, int y) {
            if (!_checked[x]) {
                BFS(x);
            }
            return _distance[x, y];
        }
        // Получение соседий вершины.
        public int[] GetNeighbors(int v)
        {
            return _adjacency[v].ToArray();
        }
        // Классический алгоритм обхода в ширину.
        // Сохраняет информацию о длинах найденных путей и сами пути.
        // Также отмечает флагом посещенные вершины.
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
        }
    }
}
