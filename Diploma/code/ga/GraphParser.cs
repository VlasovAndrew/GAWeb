﻿namespace GeneticAlgorithmWEB.BLL
{
    public static class GraphParser
    {
        // ������ ����� �� ���������� ������� �����.
        // ��������� ������������ ���������� ����� 
        // �� ������������ ������� �������.
        public static Graph ParseTxtFormat(string[] lines)
        {
            if (lines.Length == 0) {
                throw new FormatException("���� � ������ �������");
            }
            List<Edge> edges = new List<Edge>();
            foreach (var line in lines)
            {
                int[] vertices = line.Split().Select(v => int.Parse(v)).ToArray();
                if (vertices.Length != 2)
                {
                    throw new FormatException("����� ������ ���� �������� ��� ���� ������");
                }
                Edge edge = new Edge()
                {
                    V1 = vertices[0],
                    V2 = vertices[1]
                };
                if (edge.V1 < 0 || edge.V2 < 0)
                {
                    throw new FormatException("������ ������ ������ ���� �������������");
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
        // �������� �����, ��� ��������� ������ ���������� � 0 � ���� �� �������.
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
                throw new FormatException("���������� ������ ������ ���������� � ����");
            }
            int n = uniqueLabel.Max() + 1;
            if (n != uniqueLabel.Count) {
                throw new FormatException("���������� ������ ������ ���� �� �������");
            }
            return n;
        }
    }
}
