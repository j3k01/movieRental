using movieRental.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace movieRental.Data.Sql.DAOConfigurations
{
    //Klasa konfiguracyjna encji Category
    //należy zaimplementować (generyczny) interfejs IEntityTypeConfiguration i jako parametr przekazać 
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            //metoda IsRequired() wymusza w bazie ustawienie koleumny na NotNull
            builder.HasOne(x => x.Movie)
                .WithMany(x => x.Ratings)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.Client)
                .WithMany(x => x.Ratings)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.ClientId);
            //w EF Core domyślnie nazwy tabel są ustawiane w liczbie mnogiej,
            //co jest sprzeczne z dobrymi praktykami nazwania tabel w bazach danych
            //dlatego dzięki metodzie ToTable można ustawić własną nazwę bazy danych
            builder.ToTable("Rating");
        }
    }

}
