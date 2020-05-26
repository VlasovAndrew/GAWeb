namespace GeneticAlgorithmWEB.Dao
{
    // ����� �������� �� �������������� 
    // ������ � ��������� �������� ������.
    public class GraphContext : DbContext
    {
        static GraphContext() {
            // ��������� ������, �� ���� �������� ���������� 
            // ��������� ������������� ���� ������ �������.
            Database.SetInitializer(new GraphContextInitializer());
        }
        public GraphContext() : base("DB") { }
        // ����� ������ �� ���� ������.
        public DbSet<Graph> Graphs { get; set; }
    }
}