namespace GeneticAlgorithmWEB.BLL
{
    public static class GraphParser
    {
        // Чтение графа из текстового формата файла.
        // Проверяет соответствие текстового файла 
        // на соответствие верному формату.
        public static Graph ParseTxtFormat(string[] lines)
        {
            if (lines.Length == 0) {
                throw new FormatException("Файл с графом пустрой");
            }
            List<Edge> edges = new List<Edge>();
            foreach (var line in lines)
            {
                int[] vertices = line.Split().Select(v => int.Parse(v)).ToArray();
                if (vertices.Length != 2)
                {
                    throw new FormatException("Ребра должны быть записаны как пара вершин");
                }
                Edge edge = new Edge()
                {
                    V1 = vertices[0],
                    V2 = vertices[1]
                };
                if (edge.V1 < 0 || edge.V2 < 0)
                {
                    throw new FormatException("Индекс вершин должен быть положительным");
                }
                edges.Add(edge);
            }
            int n = CountVerteces(edges);
            return new Graph
            {
                N = n,
                M = edges.Count,
                Edges = edges,
            };
        }
        // Проверка факта, что нумерация вершин начинается с 0 и идет по порядку.
        private static int CountVerteces(List<Edge> edges)
        {
            HashSet<int> uniqueLabel = new HashSet<int>();
            foreach (var edge in edges)
            {
                uniqueLabel.Add(edge.V1);
                uniqueLabel.Add(edge.V2);
            }
            int minValue = uniqueLabel.Min();
            if (minValue != 0) {
                throw new FormatException("Индексация вершин должна начинаться с нуля");
            }
            int n = uniqueLabel.Max() + 1;
            if (n != uniqueLabel.Count) {
                throw new FormatException("Индексация вершин должна идти по порядку");
            }
            return n;
        }
    }
}
