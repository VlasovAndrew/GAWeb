﻿namespace GeneticAlgorithmWEB.Dao
{
    public class GraphContextInitializer : CreateDatabaseIfNotExists<GraphContext>
    {
        // ����� ��� �������� ���������� � �����, ����������� �� ������
        private class GraphDescription {
            public int RealR { get; set; }
            public string FileName { get; set; }
            public string GraphName { get; set; }
        }

        private readonly GraphDescription[] _graphDescriptions;

        public GraphContextInitializer() {
            List<GraphDescription> descriptions = new List<GraphDescription>();
            // ���������� � ������ ���������� � ������
            #region BA_GRAPHS
            
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph1_M2.txt",
                GraphName = "���� ��������-�������",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph2_M2.txt",
                GraphName = "���� ��������-�������",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph3_M2.txt",
                GraphName = "���� ��������-�������",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph4_M2.txt",
                GraphName = "���� ��������-�������",
            });
            
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph5_M2.txt",
                GraphName = "���� ��������-�������",
            });
            #endregion
            #region ER_GRAPH
            descriptions.Add(new GraphDescription() { 
                RealR = 5, 
                FileName = "ErdosRenyi1_P001.txt",
                GraphName = "���� ������-�����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi2_P001.txt",
                GraphName = "���� ������-�����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi3_P001.txt",
                GraphName = "���� ������-�����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi4_P001.txt",
                GraphName = "���� ������-�����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 3,
                FileName = "ErdosRenyi5_P001.txt",
                GraphName = "���� ������-�����",
            });
            #endregion
            #region GEOM_GRAPHS
            descriptions.Add(new GraphDescription()
            {
                RealR = 9,
                FileName = "GeometricGraph1_R01.txt",
                GraphName = "�������������� ����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 9,
                FileName = "GeometricGraph2_R01.txt",
                GraphName = "�������������� ����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph3_R01.txt",
                GraphName = "�������������� ����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph4_R01.txt",
                GraphName = "�������������� ����",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph5_R01.txt",
                GraphName = "�������������� ����",
            });
            #endregion
            _graphDescriptions = descriptions.ToArray();
        }
        // �����, ������������ ��������� �������� ���� ������, �� ���� 
        // ����� ��������� ������ � ������
        protected override void Seed(GraphContext context)
        {
            string workPath = HostingEnvironment.MapPath("~");
            string folder = "Graphs";
            foreach (var description in _graphDescriptions)
            {
                string fullPath = Path.Combine(workPath, folder, description.FileName);
                string[] lines = File.ReadAllLines(fullPath);
                Graph graph = GraphParser.ParseTxtFormat(lines);
                context.Graphs.Add(graph);
                graph.Name = description.GraphName;
                graph.R = description.RealR;
                context.Graphs.Add(graph);
            }
            context.SaveChanges();
        }
    }
}
