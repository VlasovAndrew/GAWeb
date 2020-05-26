namespace GeneticAlgorithmWEB.Dao
{
    public class GraphContextInitializer : CreateDatabaseIfNotExists<GraphContext>
    {
        // Класс для хранения информации о графе, прочитанной из файлов
        private class GraphDescription {
            public int RealR { get; set; }
            public string FileName { get; set; }
            public string GraphName { get; set; }
        }

        private readonly GraphDescription[] _graphDescriptions;

        public GraphContextInitializer() {
            List<GraphDescription> descriptions = new List<GraphDescription>();
            // Информация о графах хранящихся в файлах
            #region BA_GRAPHS
            
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph1_M2.txt",
                GraphName = "Граф Барабаши-Альберт",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph2_M2.txt",
                GraphName = "Граф Барабаши-Альберт",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph3_M2.txt",
                GraphName = "Граф Барабаши-Альберт",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph4_M2.txt",
                GraphName = "Граф Барабаши-Альберт",
            });
            
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph5_M2.txt",
                GraphName = "Граф Барабаши-Альберт",
            });
            #endregion
            #region ER_GRAPH
            descriptions.Add(new GraphDescription() { 
                RealR = 5, 
                FileName = "ErdosRenyi1_P001.txt",
                GraphName = "Граф Эрдоша-Реньи",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi2_P001.txt",
                GraphName = "Граф Эрдоша-Реньи",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi3_P001.txt",
                GraphName = "Граф Эрдоша-Реньи",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "ErdosRenyi4_P001.txt",
                GraphName = "Граф Эрдоша-Реньи",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 3,
                FileName = "ErdosRenyi5_P001.txt",
                GraphName = "Граф Эрдоша-Реньи",
            });
            #endregion
            #region GEOM_GRAPHS
            descriptions.Add(new GraphDescription()
            {
                RealR = 9,
                FileName = "GeometricGraph1_R01.txt",
                GraphName = "Геометрический граф",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 9,
                FileName = "GeometricGraph2_R01.txt",
                GraphName = "Геометрический граф",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph3_R01.txt",
                GraphName = "Геометрический граф",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph4_R01.txt",
                GraphName = "Геометрический граф",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 8,
                FileName = "GeometricGraph5_R01.txt",
                GraphName = "Геометрический граф",
            });
            #endregion
            _graphDescriptions = descriptions.ToArray();
        }
        // Метод, производящий начальную загрузку базы данных, за счет 
        // ранее считанных данных о графах
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
