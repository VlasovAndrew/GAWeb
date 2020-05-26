namespace GeneticAlgorithmWEB.Dao
{
    public class UserContext : DbContext
    {
        // Класс отвечает за предоставление 
        // достпа к коллекции объектов пользователей.
        static UserContext() {
            // Установка класса, за счет которого происходит 
            // начальная инициализация базы данных пользователями.
            Database.SetInitializer(new UserContextInitializer());
        }
        public UserContext() : base("DB") {}
        // Набор графов из базы данных.
        public DbSet<User> Users { get; set; }
    }
}
