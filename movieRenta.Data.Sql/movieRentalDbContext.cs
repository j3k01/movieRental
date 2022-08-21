using movieRental.Data.Sql.DAO;
using movieRental.Data.Sql.DAOConfigurations;
using Microsoft.EntityFrameworkCore;

namespace movieRental.Data.Sql
{
    //Klasa odpowiadająca za konfigurację Entity Framework Core
    //Przy pomocy instancji klasy FoodlyDbContext możliwe jest wykonywanie
    //wszystkich operacji na bazie danych od tworzenia bazy danych po zapytanie do bazy danych itd.
    public class movieRentalDbContext : DbContext
    {
        public movieRentalDbContext(DbContextOptions<movieRentalDbContext> options) : base(options) { }

        //Ustawienie klas z folderu DAO jako tabele bazy danych
        public virtual DbSet<DAO.Client> Client { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieOrder> MovieOrder { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }

        //Przykład konfiguracji modeli/encji poprzez klasy konfiguracyjne z folderu DAOConfigurations
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new MovieConfiguration());
            builder.ApplyConfiguration(new MovieOrderConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new RatingConfiguration());
        }
    }
}
