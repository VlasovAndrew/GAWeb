using GeneticAlgorithm.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web.Hosting;
using GeneticAlgorithmWEB.BLL;

namespace GeneticAlgorithmWEB.Dao
{
    public class ContextInitializer : CreateDatabaseIfNotExists<GraphContextDB>
    {
        private class GraphDescription {
            public int RealR { get; set; }
            public string FileName { get; set; }
        }

        private readonly GraphDescription[] _graphDescriptions;

        public ContextInitializer() {
            List<GraphDescription> descriptions = new List<GraphDescription>();
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph1_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph2_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph3_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 4,
                FileName = "BarabasiAlbertGraph4_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph5_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph6_M2.txt",
            });
            descriptions.Add(new GraphDescription()
            {
                RealR = 5,
                FileName = "BarabasiAlbertGraph7_M2.txt",
            });
            _graphDescriptions = descriptions.ToArray();
        }

        protected override void Seed(GraphContextDB context)
        {
            string workPath = HostingEnvironment.MapPath("~");
            string folder = "Graphs";
            foreach (var description in _graphDescriptions)
            {
                string fullPath = Path.Combine(workPath, folder, description.FileName);
                string[] lines = File.ReadAllLines(fullPath);
                Graph graph = GraphParser.ParseSimpleTxtFormat(lines);
                context.Graphs.Add(graph);
                graph.Name = description.FileName;
                graph.R = description.RealR;
                context.Graphs.Add(graph);
            }
            context.SaveChanges();
        }
    }
}
