using movieRental.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace movieRental.Data.Sql.DAOConfigurations
{
    //Klasa konfiguracyjna encji Category
    //należy zaimplementować (generyczny) interfejs IEntityTypeConfiguration i jako parametr przekazać 
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            //metoda IsRequired() wymusza w bazie ustawienie koleumny na NotNull
            builder.Property(c => c.MovieId).IsRequired();
            //builder.Property(c => c.MovieTitle).IsRequired();
            builder.Property(c => c.MovieAvailability).IsRequired();
            builder.Property(c => c.MovieRating).IsRequired();
            builder.HasMany(x => x.Ratings)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.RatingId);
            //w EF Core domyślnie nazwy tabel są ustawiane w liczbie mnogiej,
            //co jest sprzeczne z dobrymi praktykami nazwania tabel w bazach danych
            //dlatego dzięki metodzie ToTable można ustawić własną nazwę bazy danych
            builder.ToTable("Movie");
        }
    }

}
