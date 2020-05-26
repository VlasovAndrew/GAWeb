namespace GeneticAlgorithmWEB.Dao
{
    // Класс отвечает за предоставление 
    // достпа к коллекции объектов графов.
    public class GraphContext : DbContext
    {
        static GraphContext() {
            // Установка класса, за счет которого происходит 
            // начальная инициализация базы данных графами.
            Database.SetInitializer(new GraphContextInitializer());
        }
        public GraphContext() : base("DB") { }
        // Набор графов из базы данных.
        public DbSet<Graph> Graphs { get; set; }
    }
}